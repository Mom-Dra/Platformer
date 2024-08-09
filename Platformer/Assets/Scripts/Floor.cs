using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Floor : MonoBehaviour
{        
    private static LinkedList<Floor> floors = new LinkedList<Floor>();

    [SerializeField]
    private bool hasPlayer;

    public UnityAction<bool> hasPlayerChanged;

    private void Awake()
    {
        floors.AddLast(this);
    }

    private void SetHasPlayer(bool b)
    {
        if (hasPlayer != b) NotifyHasPlayerChanged(b);

        hasPlayer = b;
    }

    private void NotifyHasPlayerChanged(bool changed)
    {
        hasPlayerChanged?.Invoke(changed);
    }

    public float GetLeft()
    {
        return transform.position.x - transform.lossyScale.x * 0.5f;
    }

    public float GetRight()
    {
        return transform.position.x + transform.lossyScale.x * 0.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach (Floor floor in floors)
            {
                if(floor.hasPlayer && floor != this)
                {
                    floor.SetHasPlayer(false);
                }
            }

            SetHasPlayer(true);
        }
    }
}
