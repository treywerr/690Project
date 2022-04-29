using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPickup : LorePickup
{
    protected override void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<LoreDialogue>().BeginLore((int)loreIndex);
        FindObjectOfType<FlashlightController>().enabled = true;
        Destroy(gameObject);
    }
}
