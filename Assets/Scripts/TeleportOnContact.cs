using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnContact : MonoBehaviour
{
    public Vector3 teleportPosition = new Vector3(0f, 5f, 0f); // The position to teleport to

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport the player
            other.transform.position = teleportPosition;
        }
    }
}
