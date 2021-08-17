using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Transform bottom;
    [SerializeField] private Transform top;
    [SerializeField] private float timer = 1f;

    [SerializeField] private UnityEvent OnBottom;
    [SerializeField] private UnityEvent OnTop;

    float t = 0;
    private bool isStarted;
    private bool downwards;
    private Transform start;
    private Transform end;




    private void Update()
    {
        if (!isStarted) return;
        t += Time.deltaTime;
        transform.position = Vector3.Lerp(start.position, end.position, t / timer);

        if (t > timer) isStarted = false;
        if (downwards) OnBottom?.Invoke();
        else OnTop?.Invoke();
    }

    public void MoveTowards(bool down) 
    {
        t = 0;
        downwards = down;
        isStarted = true;

        if (down)
        {
            start = top;
            end = bottom;
        }
        else
        {
            start = bottom;
            end = top;
        }
    }


}
