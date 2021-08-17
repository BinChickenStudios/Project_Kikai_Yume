using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3SimulatedFollow : MonoBehaviour
{
    [SerializeField] private Transform inputPoint;
    [SerializeField] private MovementType startMovementType;
    private MovementType movementType;

    private void Start()
    {
        SetMovementType();
    }

    private void FixedUpdate()
    {
        if (inputPoint == null) return;

        if (movementType == MovementType.Kinematic)
        {
            transform.position = inputPoint.position;
            transform.rotation = inputPoint.rotation;
        }
    }

    public void SetMovementType()
    {
        SetMovementType(startMovementType);
    }
    public void SetMovementType(MovementType type) 
    {
        movementType = type;
    }


}

public enum MovementType {Kinematic, Physics }
