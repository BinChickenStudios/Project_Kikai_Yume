using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class V1SoulSwapper : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSoulSwap;
    public UnityAction OnSoulSwapAction;

    public V1Soul Soul { get; private set; }

    public Transform Left => left;
    public Transform Right => right;

    [SerializeField] private Transform left;
    [SerializeField] private Transform right;


    public void SoulSwap(V1Soul soul) 
    {
        if(Soul != null) Soul.UnSwapSoul();

        Soul = soul;
        OnSoulSwap?.Invoke();
        OnSoulSwapAction?.Invoke();
    }
}
