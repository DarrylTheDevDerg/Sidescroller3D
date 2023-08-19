using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private Rigidbody rb;
    public GameObject childObject; // Objeto hijo cuyo MeshRenderer o SkinnedMeshRenderer se modificará (en caso de usar Skinned Mesh)
    public float health = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * moveSpeed;
        transform.Translate(movement, Space.World);

        // Rotación del objeto hijo (cambio de dirección)
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            childObject.transform.rotation = Quaternion.Slerp(childObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Salto
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
    }
}
