using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    private void Start()
    {
        // Get a reference to the ObjectDestroyEvent script on the target object
        ObjectDestroyEvent objectDestroyEvent = GetComponent<ObjectDestroyEvent>();

        // Check if the reference is not null
        if (objectDestroyEvent != null)
        {
            // Subscribe to the OnDestroyed event and specify the action to perform
            objectDestroyEvent.OnDestroyed += HandleObjectDestroyed;
        }
    }

    // This method is called when the OnDestroyed event is triggered
    private void HandleObjectDestroyed()
    {
        // Perform your custom action here when the object is destroyed
        Debug.Log("Object has been destroyed!");
    }
}
