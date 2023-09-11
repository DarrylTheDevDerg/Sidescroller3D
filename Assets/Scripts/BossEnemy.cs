using UnityEngine;
using System;
using System.Collections;

public class BossEnemy : MonoBehaviour
{
    public Transform leftFist;        // Reference to the left fist cube
    public Transform rightFist;       // Reference to the right fist cube
    public float attackSpeed = 5f;    // Speed of the fist attacks
    public float attackCooldown = 2f; // Cooldown between attacks
    public float fistDamage = 10f;    // Damage inflicted by the fists
    public GameObject player;

    // Event to be triggered when the player is hit
    public event Action<float> OnPlayerHit;

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

        // Move the fists towards the player's position
        Vector3 playerPosition = player.transform.position;
        StartCoroutine(MoveFists(leftFist, playerPosition));
        StartCoroutine(MoveFists(rightFist, playerPosition));
    }

    private IEnumerator MoveFists(Transform fist, Vector3 targetPosition)
    {
        float journeyLength = Vector3.Distance(fist.position, targetPosition);
        float startTime = Time.time;

        while (fist.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * attackSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            fist.position = Vector3.Lerp(fist.position, targetPosition, fractionOfJourney);

            yield return null;
        }

        // Check if the player was hit
        if (Vector3.Distance(fist.position, targetPosition) < 0.1f)
        {
            // Trigger the player hit event with the fist's damage
            OnPlayerHit?.Invoke(fistDamage);
        }

        // Reset the fist's position
        fist.position = transform.position + fist.localPosition;

        isAttacking = false;
    }
}
