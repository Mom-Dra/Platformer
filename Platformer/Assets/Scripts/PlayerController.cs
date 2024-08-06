using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 addPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //rb.Move()

        rb.MovePosition(rb.position + addPos);
    }

    private void OnArrow(InputValue value)
    {
        Vector2 val = value.Get<Vector2>();

        Debug.Log(val);

        addPos = new Vector3(val.x, 0f, val.y);
    }

    private void OnJump(InputValue value)
    {
        Debug.Log("OnJump");
    }
}
