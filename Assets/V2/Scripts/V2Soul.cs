using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class V2Soul : MonoBehaviour
{
    [Header("Data for the player")]

    [SerializeField] private V2SoulSwapper player;    
    private void Start()
    {
        if (player == null) player = FindObjectOfType<V2SoulSwapper>();
    }
    public XRBaseInteractor Left => leftInteractor;
    public XRBaseInteractor Right => rightInteractor;
    
    [SerializeField] private XRBaseInteractor leftInteractor;
    [SerializeField] private XRBaseInteractor rightInteractor;


    [Header("Events")]
    [SerializeField] private UnityEvent OnSwapSouls;
    public UnityAction OnSwapSoulsAction;
    
    [SerializeField] private UnityEvent OnUnSwapSouls;
    public UnityAction OnUnSwapSoulsAction;

    [Header("References")]
    [SerializeField] private GameObject interactors;
    [SerializeField] private GameObject interactable;

    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [SerializeField] private Transform StartPoint;

    //this will reposition the interactors onto the player hands via the soul swapper
    //it also calls the souls to swap (when its interacted with)

    public void SwapSoul()
    {
        //tell the player to swap souls with us
        player.SoulSwap(this);

        //teleport player to us
        player.transform.position = transform.position;
        //parent us to the player
        transform.parent = player.transform;

        //teleport our hands to the player hands
        leftHand.position = player.Left.transform.position;
        rightHand.position = player.Right.transform.position;

        //parent the hands to the player 
        leftHand.parent = player.Left.transform;
        rightHand.parent = player.Right.transform;


        //disable the model and enable the hands
        interactable.SetActive(false);
        interactors.SetActive(true);

        OnSwapSoulsAction?.Invoke();
        OnSwapSouls?.Invoke();
    }

    public void UnSwapSoul()
    {
        //unparent the soul from the player
        transform.parent = null;

        //parent the hands back to the soul
        leftHand.parent = interactors.transform;
        rightHand.parent = interactors.transform;

        //disable the hands and enable the model
        interactors.SetActive(false);
        interactable.SetActive(true);

        OnUnSwapSoulsAction?.Invoke();
        OnUnSwapSouls?.Invoke();
    }
}
