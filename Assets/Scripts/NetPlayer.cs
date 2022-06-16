using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetPlayer : NetworkBehaviour
{
    public float playerSpeed = 1.0f;

    //public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    //public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

    private Rigidbody rb;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody>();
        //Position.Value = transform.position;
        //Rotation.Value = transform.rotation;

        if (IsOwner)
        {

        }
    }

    [ServerRpc]
    void SubmitMovementRequestServerRpc(Vector3 direction, Vector3 rawDirection)
    {
        rb.velocity = new Vector3(direction.x * playerSpeed, rb.velocity.y, direction.z * playerSpeed);
        //transform.position = Vector3.Lerp(transform.position, rb.position, Time.deltaTime * 20f);

        if (rawDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 20f);
        }
    }

    void Update()
    {
        //transform.position = Position.Value;
        //transform.rotation = Rotation.Value;
    }

    void FixedUpdate()
    {
        if (IsClient && IsOwner)
        {
            Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 rawVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            rb.velocity = new Vector3(moveVec.x * playerSpeed, rb.velocity.y, moveVec.z * playerSpeed);

            SubmitMovementRequestServerRpc(moveVec, rawVec);
        }
    }
}
