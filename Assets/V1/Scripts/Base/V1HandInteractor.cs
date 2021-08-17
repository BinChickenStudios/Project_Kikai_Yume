using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class V1HandInteractor : MonoBehaviour
{

    [SerializeField] private InputActionProperty SwapInput;

    [SerializeField] private V1SoulSwapper soulSwapper = null;
    [SerializeField] private bool isLeft = true;
    [SerializeField] private XRBaseInteractor soulSwapInteractor;
    [SerializeField] private UnityEvent OnSwap;
    public UnityAction OnSwapAction;

    private bool isSoulMode = true;
    private XRBaseInteractor currentSoulInteractor;
    private XRBaseInteractor currentInteractor;

    [SerializeField] private UnityEvent OnHover;
    public UnityAction OnHoverAction;
    [SerializeField] private UnityEvent OnUnHover;
    public UnityAction OnUnHoverAction;
    [SerializeField] private UnityEvent OnSelect;
    public UnityAction OnSelectAction;
    [SerializeField] private UnityEvent OnUnSelect;
    public UnityAction OnUnSelectAction;


    private void OnEnable()
    {
        SwapInput.action.started += swapButtonPressed; 
        soulSwapper.OnSoulSwapAction += Swap;
    }


    private void OnDisable()
    {
        SwapInput.action.started -= swapButtonPressed;
        soulSwapper.OnSoulSwapAction -= Swap;
    }

    private void Start()
    {
        SwapMode(true);
    }

    private void swapButtonPressed(InputAction.CallbackContext obj)
    {
        SwapMode();
    }


    public void Swap() 
    {
        //move the previous interactors back
        XRBaseInteractor soulInteractor = null;
        if (isLeft) soulInteractor = soulSwapper.Soul.Left;
        else soulInteractor = soulSwapper.Soul.Right;
        SetCurrentSoulInteractor(soulInteractor);
    }


    public void SetCurrentSoulInteractor(XRBaseInteractor interactor)
    {
        UnSubscribeTo(currentSoulInteractor);
        currentSoulInteractor = interactor;
        SwapMode(false);
        SwapInteractor();
    }

    public void SwapMode()
    {
        SwapMode(!isSoulMode);
    }

    public void SwapMode(bool mode)
    {
        isSoulMode = mode;
        SwapInteractor();
        OnSwap?.Invoke();
        OnSwapAction?.Invoke();
    }

    public void SwapInteractor()
    {
        UnSubscribeTo(currentInteractor);
        if(currentInteractor != null) currentInteractor.gameObject.SetActive(false);

        if (isSoulMode || currentSoulInteractor == null) currentInteractor = soulSwapInteractor;
        else currentInteractor = currentSoulInteractor;


        currentInteractor.gameObject.SetActive(true);
        SubscribeTo(currentInteractor);
    }


    public void SubscribeTo(XRBaseInteractor interactor)
    {
        if (interactor == null) return;
        interactor.onHoverEntered.AddListener(Hover);
        interactor.onHoverExited.AddListener(UnHover);
        interactor.onSelectEntered.AddListener(Select);
        interactor.onSelectExited.AddListener(UnSelect);
    }


    public void UnSubscribeTo(XRBaseInteractor interactor)
    {
        if (interactor == null) return;
        interactor.onHoverEntered.RemoveListener(Hover);
        interactor.onHoverExited.RemoveListener(UnHover);
        interactor.onSelectEntered.RemoveListener(Select);
        interactor.onSelectExited.RemoveListener(UnSelect);
    }

    public void Hover(XRBaseInteractable arg0)
    {
        OnHover?.Invoke();
        OnHoverAction?.Invoke();
    }
    public void UnHover(XRBaseInteractable arg0) 
    {
        OnUnHover?.Invoke();
        OnUnHoverAction?.Invoke();
    }
    public void Select(XRBaseInteractable arg0) 
    {
        OnSelect?.Invoke();
        OnSelectAction?.Invoke();
    }
    public void UnSelect(XRBaseInteractable arg0) 
    {
        OnUnSelect?.Invoke();
        OnSelectAction?.Invoke();
    }


}
