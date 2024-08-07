using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    private Rigidbody rb;
    private Vector3 addPos;

    private bool canJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + addPos * Time.fixedDeltaTime * speed);

        Debug.DrawRay(rb.position, Vector3.down * (transform.lossyScale.y + 0.01f), Color.red);
        Debug.DrawRay(rb.position, transform.forward, Color.red);

        if (Physics.Raycast(rb.position, Vector3.down, transform.lossyScale.y + 0.01f, LayerMask.GetMask("Floor")))
        {
            Debug.Log("true·Î!");
            canJump = true;
        }
        else canJump = false;
    }

    private void OnArrow(InputValue value)
    {
        Vector2 val = value.Get<Vector2>();

        Debug.Log(val);

        addPos = new Vector3(val.x, 0f, 0f).normalized;
    }

    private void OnJump(InputValue value)
    {
        if (!canJump) return;

        Debug.Log($"false·Î");

        canJump = false;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
