using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.GetComponent<Interactable>() != null)
            {
                other.gameObject.GetComponent<Interactable>().BaseInteract(this.gameObject);
            }
        }
}
