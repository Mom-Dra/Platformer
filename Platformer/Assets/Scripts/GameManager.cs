using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public GameObject player;

    protected override void Awake()
    {
        base.Awake();
    }
}
