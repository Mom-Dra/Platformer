using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{ 
    Move,
    Detect,
    Attack
}


public class EnemyController : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float detectDistance;

    [SerializeField]
    private Floor floor;

    private Vector3 dir;

    private EnemyState state;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        dir = Vector3.right;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Detect:
                break;
            case EnemyState.Attack:
                break;
        }
    }

    private void OnEnable()
    {
        floor.hasPlayerChanged += FloorHasPlayer;
    }

    private void OnDisable()
    {
        floor.hasPlayerChanged -= FloorHasPlayer;
    }

    private void Move()
    {
        Vector3 nextPos = rigid.position + dir * Time.fixedDeltaTime * speed;
        nextPos.x = Mathf.Clamp(nextPos.x, floor.GetLeft(), floor.GetRight());  
        
        if(nextPos.x >= floor.GetRight())
        {
            dir = Vector3.left;
        }
        else if (nextPos.x <= floor.GetLeft())
        {
            dir = Vector3.right;
        }

        rigid.MovePosition(nextPos);
    }

    private void Detect()
    {
        Debug.Log("Detect");
    }

    private void FloorHasPlayer(bool b)
    {
        
    }
}
