using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MethodEvent : UnityEvent { }

public class ExecuteMethodOnContact : MonoBehaviour
{
    public string targetTag = "Player"; // The tag of the object to make contact with
    public MonoBehaviour[] scriptsToExecute; // Array of scripts to execute methods from
    public MethodEvent methodToExecute; // UnityEvent to execute the method

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            foreach (MonoBehaviour script in scriptsToExecute)
            {
                if (script != null)
                {
                    methodToExecute.Invoke();
                }
            }
        }
    }
}
