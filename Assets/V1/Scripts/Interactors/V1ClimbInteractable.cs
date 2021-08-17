using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class V1ClimbInteractable : V1Interactable
{

    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        print("climbing");
        base.OnSelectEntering(interactor);
    }
}
