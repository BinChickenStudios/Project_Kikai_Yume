using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class V1ClimberInteractor : XRSocketInteractor 
{
    [SerializeField] InputActionProperty velocityInput;
    [SerializeField] private CharacterController player;

    private  void Start()
    {
        player = FindObjectOfType<V1SoulSwapper>().GetComponent<CharacterController>();
    }

    private bool isClimbing = false;
    private void FixedUpdate()
    {
        if (isClimbing) 
        {
            print("climbing");
            Climb();
        }
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        base.OnSelectEntered(interactable);

        isClimbing = true;
    }

    protected override void OnSelectExiting(XRBaseInteractable interactable)
    {
        base.OnSelectExiting(interactable);

        isClimbing = false;
    }



    private void Climb() 
    {
        Vector3 vel = velocityInput.action.ReadValue<Vector3>();

        player.Move(-vel * Time.fixedDeltaTime);
    }
}

