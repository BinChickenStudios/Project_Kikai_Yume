using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class V2SoulSwapper : MonoBehaviour
{
    //events
    [SerializeField] private UnityEvent OnSoulSwap;
    public UnityAction OnSoulSwapAction;

    //references to the soul
    public V2Soul Soul { get; private set; }


    //swap souls (gets called by the soul)
    public void SoulSwap(V2Soul soul)
    {
        if (Soul != null) Soul.UnSwapSoul();

        Soul = soul;
        OnSoulSwap?.Invoke();
        OnSoulSwapAction?.Invoke();
    }

    //references for the soul
    public Transform Left => left;
    public Transform Right => right;

    //the positions of the hand
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

}
