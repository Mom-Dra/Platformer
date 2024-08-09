using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum EnemyState
{ 
    Move,
    Detect,
    Perceive,
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
    private float attackDistance;

    [SerializeField]
    private Floor floor;

    private Vector3 dir;

    private EnemyState state;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        dir = Vector3.right;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Detect:
                Detect();
                break;

            case EnemyState.Perceive:
                Perceive();
                break;

            case EnemyState.Attack:
                Attack();
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

    private void ChangeState(EnemyState nextState)
    {
        state = nextState;
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

    private void Perceive()
    {
        Debug.Log("Perceive");
        if (Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) < detectDistance)
            ChangeState(EnemyState.Detect);

        float pos = transform.position.x - GameManager.Instance.player.transform.position.x;

        Vector3 lDir;

        if(pos < 0f)
        {
            lDir = Vector3.right;
        }
        else
        {
            lDir = Vector3.left;
        }

        Vector3 nextPos = rigid.position + lDir  * Time.fixedDeltaTime * speed;
        nextPos.x = Mathf.Clamp(nextPos.x, floor.GetLeft(), floor.GetRight());

        rigid.MovePosition(nextPos);
    }

    private void Detect()
    {
        Debug.Log("Detect");

        float dis = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position);

        if (dis < attackDistance)
            ChangeState(EnemyState.Attack);
        else if(dis > detectDistance)
            ChangeState(EnemyState.Perceive);
    }

    private void Attack()
    {
        Debug.Log("Attack");

        if(Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) > attackDistance)
            ChangeState(EnemyState.Detect);
    }

    private void FloorHasPlayer(bool b)
    {
        if(b)
        {
            Debug.Log("Perceive·Î");
            ChangeState(EnemyState.Perceive);
        }
        else
        {
            Debug.Log("Move·Î");
            ChangeState(EnemyState.Move);
        }
    }
}
