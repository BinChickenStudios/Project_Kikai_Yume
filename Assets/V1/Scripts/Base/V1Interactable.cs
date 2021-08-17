using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class V1Interactable : XRSimpleInteractable
{
    [SerializeField] private bool allSoulInteractors;
    public List<XRBaseInteractor> interactors = new List<XRBaseInteractor>();
    private void updateInteractors() 
    {
        if (!allSoulInteractors) return;
        XRBaseInteractor[] _sInteractors = FindObjectsOfType<V1SoulInteractor>();

        foreach (XRBaseInteractor interactor in _sInteractors)
        {
            if (!interactors.Contains(interactor)) interactors.Add(interactor);
        }
    }

    private void Start()
    {
        updateInteractors();
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return interactors.Contains(interactor) && base.IsSelectableBy(interactor);
    }

    public override bool IsHoverableBy(XRBaseInteractor interactor)
    {
        return  interactors.Contains(interactor) && base.IsHoverableBy(interactor);
    }

    //what can interact with this?
    //gripper -> gripSoul 

    //when is it considered interacting?
    //this is decided by the interaction but we check if its a grip soul

    //what happens when its being interacted with?
    //displace the player (another script)



}
