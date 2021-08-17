using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class V1TargetRayInteractor : XRRayInteractor
{
    public string TargetTag = string.Empty;

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && IsTargetTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && IsTargetTag(interactable);
    }

    protected bool IsTargetTag(XRBaseInteractable interactable) 
    {
        return interactable.CompareTag(TargetTag);
    }
}
