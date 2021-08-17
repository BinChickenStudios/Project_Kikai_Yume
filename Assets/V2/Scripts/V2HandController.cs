using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class V2HandController : MonoBehaviour 
{
    //swap button input
    [SerializeField] private InputActionProperty SwapInput;
    [Space(10)]
    [SerializeField] private UnityEvent OnSwap;
    public UnityAction OnSwapAction;

    //swap button input
    [SerializeField] private InputActionProperty MenuInput;
    [Space(10)]
    [SerializeField] private UnityEvent OnMenu;
    public UnityAction OnMenuAction;
    private GameManager manager;

    private bool isAlternate = false;

    [Header("References")]
    [SerializeField] private bool isLeft = true;
    [SerializeField] private V2SoulSwapper soulSwapper;   
    
    //useful if i need specific info for the interactors
    private XRBaseInteractor soulInteractor;
    private XRBaseInteractor alternateInteractor;
    private XRBaseInteractor activeInteractor;

    [SerializeField] private GameObject soulSocket;
    [SerializeField] private GameObject alternateSocket;
    private GameObject activeSocket;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        //what to do when we are swapping souls
        soulSwapper.OnSoulSwapAction += UpdateAlternateInteractor;

        //what to do when the swap button is pressed
        SwapInput.action.started += SwapButtonPressed;

        MenuInput.action.started += MenuButtonPressed;
    }

    private void OnDisable()
    {
        //what to do when we are swapping souls
        soulSwapper.OnSoulSwapAction -= UpdateAlternateInteractor;

        //what to do when the swap button is pressed
        SwapInput.action.started -= SwapButtonPressed;

        MenuInput.action.started -= MenuButtonPressed;
    }

    //gets called when the swap button is pressed
    private void SwapButtonPressed(InputAction.CallbackContext obj)
    {
        Swap();
    }

    private void MenuButtonPressed(InputAction.CallbackContext obj) 
    {
        manager.ToggleMenu();
        OnMenu?.Invoke();
        OnMenuAction?.Invoke();
    }

    public void Swap()
    {
        Swap(!isAlternate);
    }

    public void Swap(bool alternate)
    {
        if(manager != null) if (manager.toggleValue) return;

        //which interactor do we want?
        isAlternate = alternate;

        //swap the interactor
        SwapInteractor();

        //call events
        OnSwap?.Invoke();
        OnSwapAction?.Invoke();
    }

    public void SwapInteractor()
    {
        //disable the previous interactor
        if (activeSocket != null) activeSocket.SetActive(false);

        if (!isAlternate) activeSocket = soulSocket;
        else activeSocket = alternateSocket;

        //enable the current interactor
        activeSocket.SetActive(true);
    }

    
    public void UpdateAlternateInteractor()
    {
        XRBaseInteractor interactor = null;

        //the soul swapper contains information on the current soul we inherit, so we just update it
        if (isLeft) interactor = soulSwapper.Soul.Left;
        else interactor = soulSwapper.Soul.Right;
        
        
        SetAlternateInteractor(interactor);
    }


    public void SetAlternateInteractor(XRBaseInteractor interactor)
    {
        //update to the new interactor
        alternateInteractor = interactor;

        //swap to the new interactor 
        Swap(true);
    }

}

