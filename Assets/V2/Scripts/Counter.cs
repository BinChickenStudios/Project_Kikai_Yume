using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    [SerializeField] private UnityEvent OnComplete;
    [SerializeField] private UnityEvent OnCheck;
    [SerializeField] private List<GameObject> checklist = new List<GameObject>();

    public void Check() 
    {
        OnCheck?.Invoke();
        foreach (GameObject check in checklist) 
        {
            if (!check.activeSelf)
            {
                return;
            }
        }

        OnComplete?.Invoke();
    }
}
