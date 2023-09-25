using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour
{
    public Transform leftFist;        // Reference to the left fist cube
    public Transform rightFist;       // Reference to the right fist cube
    public float attackSpeed = 5f;    // Speed of the fist attacks
    public float attackCooldown = 2f; // Cooldown between attacks
    public GameObject player;

    private bool isAttacking = false;
    private float attackTimer = 0f;

    private void Update()
    {
        if (!isAttacking)
        {
            // Check if it's time to attack again based on the cooldown
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                Attack();
                attackTimer = 0f;
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;

        // Calculate the target position based on the player's current position
        Vector3 playerPosition = player.transform.position;

        // Move the fists towards the player's position
        StartCoroutine(MoveFists(leftFist, playerPosition));
        StartCoroutine(MoveFists(rightFist, playerPosition));
    }


    private IEnumerator MoveFists(Transform fist, Vector3 targetPosition)
    {
        while (fist.position != targetPosition)
        {
            // Calculate the direction towards the target position
            Vector3 moveDirection = (targetPosition - fist.position).normalized;

            // Calculate the new position based on the attackSpeed
            Vector3 newPosition = fist.position + moveDirection * attackSpeed * Time.deltaTime;

            // Move the fist towards the player's position
            fist.position = newPosition;

            yield return null;
        }

        // Reset the fist's position to the boss's position
        fist.position = transform.position;

        isAttacking = false;
    }
}
