using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    private V2MovementSystem player;

    [SerializeField] private UnityEvent OnCheckpointReached;


    private void Start()
    {
        player = FindObjectOfType<V2MovementSystem>();
    }

    public void ReachedCheckPoint()
    {
        player.SetRespawnPoint(transform);
        player.Respawn();
        OnCheckpointReached?.Invoke();
    }

}
