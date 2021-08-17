using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class TriggerEnter : MonoBehaviour
{
    [SerializeField] private bool autoSetupRigidbody = true;
    private Rigidbody rb;
    [SerializeField] private BoxCollider col;
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool useRequireTarget = false;

    [SerializeField] private List<GameObject> targetObjs = new List<GameObject>();
    [SerializeField] private List<string> targetTags = new List<string>();
    [SerializeField] private UnityEvent OnTriggerEntered;
    [SerializeField] private UnityEvent OnTriggerExited;
    [SerializeField] private UnityEvent OnTarget;
    [SerializeField] private UnityEvent OnNoTarget;

    private int index = 0;
    private bool targetContained = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        index = 0;
        if (!autoSetupRigidbody) return;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetObjs.Contains(other.gameObject)) return;

        if (!targetTags.Contains(other.tag)) return;

        targetObjs.Add(other.gameObject);
        OnTriggerEntered?.Invoke();
        if (!targetContained) OnTarget?.Invoke();
        targetContained = true;
    }

    private void OnTriggerExit(Collider other)
    {

        if (targetObjs.Contains(other.gameObject))
        {

            targetObjs.Remove(other.gameObject);
            OnTriggerExited?.Invoke();
        }

        if (!useRequireTarget) return;

        if (!CheckCast(other))
        {
            targetContained = false;
            OnNoTarget?.Invoke();
            return;
        }


    }
    public bool CheckCast(Collider exception) 
    {
        Collider[] other = Physics.OverlapBox(transform.position,col.size, Quaternion.identity ,mask);

        foreach (Collider c in other) 
        {
            if (c == exception) continue;
            if (targetTags.Contains(c.tag)) return true;
        }

        return false;
    }

    private Vector3 MultiplyVector(Vector3 a, Vector3 b) 
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, col.size);
    }
}
