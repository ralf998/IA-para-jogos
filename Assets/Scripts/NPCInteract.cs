using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.GetComponent<Interactable>() != null)
            {
                other.gameObject.GetComponent<Interactable>().BaseInteract(this.gameObject);
            }
        }
}
