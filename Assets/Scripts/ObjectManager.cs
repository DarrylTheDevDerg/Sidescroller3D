using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public string targetTag = "Player";
    public bool activate = true;
    public bool deactivate = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (activate)
            { 
                for (int i = 0; i < objectsToActivate.Length; i++)
                {
                    objectsToActivate[i].SetActive(true);
                }
            }

            if (deactivate)
            { 
                for (int i = 0; i < objectsToDeactivate.Length; i++)
                {
                    objectsToDeactivate[i].SetActive(false);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (activate)
            {
                for (int i = 0; i < objectsToActivate.Length; i++)
                {
                    objectsToActivate[i].SetActive(false);
                }
            }

            if (deactivate)
            {
                for (int i = 0; i < objectsToDeactivate.Length; i++)
                {
                    objectsToDeactivate[i].SetActive(true);
                }
            }
        }
    }
}
