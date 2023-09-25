using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFilter : MonoBehaviour
{
    public string[] allowedTags; // Tags of objects allowed to collide with this object

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a tag that's not in the allowedTags array
        if (!IsTagAllowed(collision.gameObject.tag))
        {
            // Disable the collision by setting the collider to isTrigger
            collision.collider.isTrigger = true;
        }
    }

    private bool IsTagAllowed(string tag)
    {
        // Check if the provided tag is in the allowedTags array
        foreach (string allowedTag in allowedTags)
        {
            if (tag == allowedTag)
            {
                return true; // The tag is allowed
            }
        }
        return false; // The tag is not allowed
    }
}
