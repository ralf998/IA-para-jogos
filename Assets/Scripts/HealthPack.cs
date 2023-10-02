using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthPack : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BaseInteract()
    {
        Interact();
    }

    protected override void Interact()
    {
        
    }
}
