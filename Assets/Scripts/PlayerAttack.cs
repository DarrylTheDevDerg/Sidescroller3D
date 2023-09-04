using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab1;  // Prefab for first attack
    public GameObject attackPrefab2;  // Prefab for second attack
    public GameObject attackPrefab3;  // Prefab for third attack
    public GameObject superAttack;
    public Vector3 attackOffset = new Vector3(1f, 0f, 0f);  // Offset for attack instantiation
    public float attackCooldown = 1.5f;  // Cooldown time between attacks
    public float xAttackCooldown = 1.5f;  // Cooldown time between attacks
    public float cAttackCooldown = 2.2f;  // Cooldown time between attacks
    public float charge;
    public float chargecooldown;
    private bool canAttack = true;  // Flag to track attack cooldown
    public DamageableObject hpControl;

    public Transform playerTransform; // Reference to the player's transform


    private void Update()
    {
        if (canAttack)
        {
            // Attack with Z key
            if (Input.GetKey(KeyCode.Z))
            {
                hpControl.damageAmount = 1.3f;
                PerformAttack(attackPrefab1);
                StartCoroutine(StartCooldown(0f));
            }

            // Attack with X key
            if (Input.GetKey(KeyCode.X))
            {
                hpControl.damageAmount = 2.5f;
                PerformAttack(attackPrefab2);
                StartCoroutine(StartCooldown(xAttackCooldown));
            }

            // Attack with C key
            if (Input.GetKey(KeyCode.C))
            {
                hpControl.damageAmount = 3.4f;
                PerformAttack(attackPrefab3);
                StartCoroutine(StartCooldown(cAttackCooldown));
            }
            
            if (Input.GetKey(KeyCode.V))
            {   
                charge += 0.09f;
            }
        }

        if (canAttack && charge >= 100)
        {
            hpControl.damageAmount = 20f;
            PerformSUPERAttack(superAttack);
            charge = -5f;
        }

        if (charge > 0 && charge < 100)
        {
            chargecooldown += 0.15f;
        }

        if (chargecooldown >= 11.5f)
        {
            charge -= 1.3f;
            chargecooldown = 0f;
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

    private void PerformSUPERAttack(GameObject prefab)
    {
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * (attackOffset.x * 4.6f);
        Quaternion spawnRotation = playerTransform.rotation;

        GameObject attackInstance = Instantiate(prefab, spawnPosition, spawnRotation);
    }

    private IEnumerator StartCooldown(float addedscs)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown + addedscs);
        canAttack = true;
    }
}
