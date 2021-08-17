using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class V2Interactable : XRBaseInteractable
{
    [SerializeField] private List<XRBaseInteractor> interactors = new List<XRBaseInteractor>();
    [SerializeField] private List<string> interactorTags = new List<string>();
    [SerializeField] private UnityEvent OnStart;

    private void Start()
    {
        OnStart?.Invoke();
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isSelectable = base.IsSelectableBy(interactor);
        if (!isSelectable) return false;

        if (!interactorTags.Contains(interactor.tag) && !interactors.Contains(interactor)) return false;
        else return true;

    }

    public override bool IsHoverableBy(XRBaseInteractor interactor)
    {
        bool isHoverable = base.IsHoverableBy(interactor);
        if (!isHoverable) return false;

        if (!interactorTags.Contains(interactor.tag) && !interactors.Contains(interactor)) return false;
        return true;
    }
}
