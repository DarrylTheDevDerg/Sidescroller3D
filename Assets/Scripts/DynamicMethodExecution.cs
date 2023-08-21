using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.Events;

public class DynamicMethodExecution : MonoBehaviour
{
    public string targetTag; // Reference to the script containing the method
    public MethodEvent[] voidsToExecute; // Name of the void method to execute
    public float cooldown = 1f;
    public float cooldownStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (voidsToExecute != null && cooldown <= 0f)
            {
                for (int i = 0; i < voidsToExecute.Length; i++)
                {
                    voidsToExecute[i].Invoke();
                    cooldown = cooldownStart;
                }
            }
        }
    }

    private void Update()
    {
        if (cooldown > 0)
        {
        cooldown =- Time.deltaTime;
        }
    }
}
