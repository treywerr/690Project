using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light lite;
    public float chargeTimeMax = 5;
    public float litTimeMax = 2;
    [SerializeField] private float chargeTime = 5;
    [SerializeField] private float litTime = 2;


    // Start is called before the first frame update
    void Start()
    {
        lite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lite.enabled) {
            /* Flashlight lit */
            // OPERATION SOUND EMITTED
            // Kill enemies, or have enemies be killed (kill them here or in a script on the enemies?)
            litTime -= Time.deltaTime;
            if (litTime <= 0)
            {
                lite.enabled = false;
                litTime = litTimeMax;
            }
        }
        else if (InputWrapper.GetKey(KeyCode.F))
        {
            /* Flashlight charging */
            // WIND UP SOUND EMITTED
            chargeTime -= Time.deltaTime;
            if (chargeTime <= 0) {
                lite.enabled = true;
                chargeTime = chargeTimeMax;
            }
        }
        else
        {
            chargeTime = chargeTimeMax;
        }
    }
}
