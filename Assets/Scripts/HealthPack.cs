using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Interact(GameObject npc)
    {
        npc.GetComponent<NPCAllySM>().life += 30;
        Destroy(this.gameObject);
    }
}
