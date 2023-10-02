using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
            if(other.GetComponent<Interactable>() != null)
            {
                other.GetComponent<Interactable>().BaseInteract();
            }
        }
}
