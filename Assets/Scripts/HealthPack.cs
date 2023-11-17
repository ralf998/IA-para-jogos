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
        if(npc.GetComponent<NPCAllySM>() != null)
        {
            npc.GetComponent<NPCAllySM>().life += 30;
        
        }
        else if(npc.GetComponent<AllyBT>() != null)
        {
            npc.GetComponent<AllyBT>().IncreaseLifePoints(30);
        
        }
        Destroy(this.gameObject);
    }
}
