using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Interactable
{
    void Start()
    {
        
    }

    protected override void Interact(GameObject npc)
    {
        npc.GetComponent<NPCAllySM>().life += 30;
        Destroy(this.gameObject);
    }
}
