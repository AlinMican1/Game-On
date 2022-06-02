using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    float ZInput;
    float XInput;

    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform orientation;

    [Header("Test")]
    [SerializeField] Vector3 moveDir;

    private void Start()
    {
        GetReferences();
        MovePlayer();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void GetReferences()
    {
        rb = GetComponent<Rigidbody>();
        orientation = transform.GetChild(1).GetComponent<Transform>();
    }

    void HandleInput()
    {
        ZInput = Input.GetAxisRaw("Vertical");
        XInput = Input.GetAxisRaw("Horizontal");
    }

    void MovePlayer()
    {
        moveDir = orientation.forward * ZInput + orientation.right * XInput;

        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Acceleration);
    }
}
