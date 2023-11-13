using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePackage : Interactable
{
    void Start()
    {
        
    }

    protected override void Interact(GameObject npc)
    {
        npc.GetComponent<NPCAllySM>().resources += 50;
        Destroy(this.gameObject);
    }
}
