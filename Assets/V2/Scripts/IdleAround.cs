using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAround : MonoBehaviour
{
    [SerializeField] private bool onStart;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 range = Vector3.one;
    [SerializeField] GameObject empty;
    private Transform target;
    private bool move;

    private void Start()
    {
        target = Instantiate(empty, transform.position, Quaternion.identity).transform;
        if (onStart) Move(true);
    }
    private void Update()
    {
        if (!move) return;
        UpdateMove();
    }

    public void Move() 
    {
        Move(!move);
    }

    public void Move(bool value) 
    {
        move = value;
    }


    void UpdateMove() 
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < .1f) target.position = range * Random.Range(0f,1f);
        transform.LookAt(target);
    }

}
