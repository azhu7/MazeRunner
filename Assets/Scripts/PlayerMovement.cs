using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed;
    private float maxSpeed = 10f;

    private Vector3 input;
    private Vector3 spawn;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        rb.AddForce(input * moveSpeed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Respawn if player jumps off
        if (transform.position.y < -2)
        {
            RpcRespawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Goal")
        {
            GameManager.CompleteLevel();
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = spawn;  // Respawn
            rb.velocity = new Vector3();  // No initial velocity
        }
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}