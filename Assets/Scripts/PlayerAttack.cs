using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab1;  // Prefab for first attack
    public GameObject attackPrefab2;  // Prefab for second attack
    public GameObject attackPrefab3;  // Prefab for third attack
    public Vector3 attackOffset = new Vector3(1f, 0f, 0f);  // Offset for attack instantiation
    public float attackCooldown = 1.5f;  // Cooldown time between attacks
    public float xAttackCooldown = 1.5f;  // Cooldown time between attacks
    public float cAttackCooldown = 2.2f;  // Cooldown time between attacks
    private bool canAttack = true;  // Flag to track attack cooldown

    public Transform playerTransform; // Reference to the player's transform


    private void Update()
    {
        if (canAttack)
        {
            // Attack with Z key
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PerformAttack(attackPrefab1);
                StartCoroutine(StartCooldown(0f));
            }

            // Attack with X key
            if (Input.GetKeyDown(KeyCode.X))
            {
                PerformAttack(attackPrefab2);
                StartCoroutine(StartCooldown(xAttackCooldown));
            }

            // Attack with C key
            if (Input.GetKeyDown(KeyCode.C))
            {
                PerformAttack(attackPrefab3);
                StartCoroutine(StartCooldown(cAttackCooldown));
            }
        }
    }

    private void PerformAttack(GameObject prefab)
    {
        // Instantiate the attack prefab with the specified offset
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * attackOffset.x;
        Quaternion spawnRotation = playerTransform.rotation;

        // Instantiate the attack prefab with calculated position and rotation
        GameObject attackInstance = Instantiate(prefab, spawnPosition, spawnRotation);

        // Assuming the prefab has a script for attack behavior, you can access and set parameters here
        //AttackBehavior attackBehavior = attackInstance.GetComponent<AttackBehavior>();
        //if (attackBehavior != null)
        //{
        //attackBehavior.InitiateAttack();
        //}
    }

    private IEnumerator StartCooldown(float addedscs)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown + addedscs);
        canAttack = true;
    }
}
