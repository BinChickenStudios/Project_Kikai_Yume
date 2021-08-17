using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class V2TeleportationArea : TeleportationArea
{
    [SerializeField] private string teleporterTag = "Teleporter";
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (interactor.CompareTag(teleporterTag)) return true;
        else return false;
    }
}
