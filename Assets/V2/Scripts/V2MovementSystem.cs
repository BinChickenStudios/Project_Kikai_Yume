using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class V2MovementSystem : MonoBehaviour
{

    float minHeight = -10f;

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private UnityEvent OnRespawn;
    public UnityAction OnRespawnAction;

    public void SetRespawnPoint(Transform point) 
    {
        respawnPoint = point;
    }

    public void Respawn() 
    {
        OnRespawnAction?.Invoke();
        OnRespawn?.Invoke();
    }

    public void Death() 
    {
        if (respawnPoint == null) respawnPoint = startPoint;
        transform.position = respawnPoint.position;
        Respawn();
    }

    private void Update()
    {
        if (transform.position.y > minHeight) return;
        Death();
    }


}
