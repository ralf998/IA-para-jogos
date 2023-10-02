using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BaseInteract(GameObject npc)
    {
        Interact(npc);
    }

    protected virtual void Interact(GameObject npc) {}
}
