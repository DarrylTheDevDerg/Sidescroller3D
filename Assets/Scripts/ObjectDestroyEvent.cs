using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectDestroyEvent : MonoBehaviour
{
    // Define an event delegate that takes no arguments
    public event Action OnDestroyed;

    // This method is called when the object is destroyed
    private void OnDestroy()
    {
        // Check if there are any subscribers to the OnDestroyed event
        if (OnDestroyed != null)
        {
            // Trigger the event, notifying all subscribers
            OnDestroyed.Invoke();
        }
    }
}
