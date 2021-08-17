using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class V1Soul : MonoBehaviour
{
    [SerializeField] private V1SoulSwapper player;

    private void Start()
    {
        if (player == null) player = FindObjectOfType<V1SoulSwapper>();
    }

    public XRBaseInteractor Left => leftInteractor;
    public XRBaseInteractor Right => rightInteractor;

    [SerializeField] private GameObject interactors;
    [SerializeField] private GameObject interactable;
    
    [SerializeField] private XRBaseInteractor leftInteractor;
    [SerializeField] private XRBaseInteractor rightInteractor;

    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform RightHand;


    [SerializeField] private UnityEvent OnSwapSouls;
    public UnityAction OnSwapSoulsAction;

    [SerializeField] private UnityEvent OnUnSwapSouls;
    public UnityAction OnUnSwapSoulsAction;

    public void SwapSouls() 
    {
        player.transform.position = transform.position;
        
        leftHand.position = player.Left.transform.position;
        RightHand.position = player.Right.transform.position;

        leftHand.parent = player.Left.transform;
        RightHand.parent = player.Right.transform;

        player.SoulSwap(this);

        interactable.SetActive(false);
        interactors.SetActive(true);

        OnSwapSoulsAction?.Invoke();
        OnSwapSouls?.Invoke();
    }

    public void UnSwapSoul() 
    {
        leftInteractor.transform.parent = transform;
        rightInteractor.transform.parent = transform;

        interactors.SetActive(false);
        interactable.SetActive(true);

        OnUnSwapSoulsAction?.Invoke();
        OnUnSwapSouls?.Invoke();
    }
}
