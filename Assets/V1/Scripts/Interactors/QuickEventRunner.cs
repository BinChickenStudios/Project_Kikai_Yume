using UnityEngine;
using UnityEngine.Events;

public class QuickEventRunner : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStart;

    private void Start()
    {
        OnStart?.Invoke();
    }
}

