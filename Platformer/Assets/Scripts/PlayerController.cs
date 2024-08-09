using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    private Rigidbody rb;
    private Vector3 moveAddPos;
    private Vector3 rotVec;

    private bool canJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
    }

    private void FixedUpdate()
    {
        Move();
        RealTimeDownRay();
    }

    private void Move()
    {
        Vector3 nextPos = rb.position + moveAddPos * Time.fixedDeltaTime * speed;
        Quaternion lookRot = Quaternion.LookRotation(rotVec);

        rb.Move(nextPos, lookRot);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void RealTimeDownRay()
    {
        Debug.DrawRay(rb.position, Vector3.down * (transform.lossyScale.y + 0.01f), Color.red);
        Debug.DrawRay(rb.position, transform.forward, Color.red);

        RaycastHit hitInfo;

        if (Physics.Raycast(rb.position, Vector3.down, out hitInfo, transform.lossyScale.y + 0.01f))
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                canJump = true;
            }
        }
        else canJump = false;
    }

    private void CollisionDownRay()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(rb.position, Vector3.down, out hitInfo, transform.lossyScale.y + 0.01f))
        {
            if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                //hitInfo.transform.GetComponent<Floor>().
            }
            else if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Jump();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionDownRay();
    }

    private void OnArrow(InputValue value)
    {
        Vector2 val = value.Get<Vector2>();

        moveAddPos = new Vector3(val.x, 0f, 0f).normalized;
        if (Mathf.Abs(val.x) > float.Epsilon) rotVec = moveAddPos;
    }

    private void OnJump(InputValue value)
    {
        if (!canJump) return;
        canJump = false;

        Jump();
    }
}
