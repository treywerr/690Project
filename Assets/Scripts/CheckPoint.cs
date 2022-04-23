using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool cooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player") && !cooldown)
        {
            StartCoroutine(Cooldown());
            Respawner.setLocalSpawn(transform);
            Respawner.SaveImportantInfo();
        }
    }

    IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(10f);
        cooldown = false;
    }
}
