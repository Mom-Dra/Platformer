using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Floor : MonoBehaviour
{
    static List<Floor> floorList = new List<Floor>();
    LinkedList<Floor> l;
        
    private GameObject game;

    private bool hasPlayer;

    public UnityAction<bool> hasPlayerChanged;

    private void Awake()
    {
        floorList.Add(this);
    }

    private void NotifyHasPlayerChanged(bool changed)
    {
        hasPlayerChanged.Invoke(changed);
    }
}
