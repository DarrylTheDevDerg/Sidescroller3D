using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KamikazeObject : MonoBehaviour
{
    public MethodEvent[] eventsToExecute;
    public string targetTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        { 
            for (int i = 0; i < eventsToExecute.Length; i++)
            {
                eventsToExecute[i].Invoke();
            }

            Destroy(gameObject);
        }
        
    }
}
