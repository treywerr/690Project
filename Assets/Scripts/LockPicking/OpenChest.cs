using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : UnlockEffect
{
    [SerializeField] GameObject itemToSpawn;

    public override void UnlockAction()
    {
        Instantiate(itemToSpawn, transform.position + Vector3.up, transform.rotation);
        Destroy(gameObject);
    }
}
