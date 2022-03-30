using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light lite;
    public float chargeTimeMax = 5;
    public float litTimeMax = 2;
    private float chargeTime = 5;
    private float litTime = 2;
    [SerializeField] private int numRays = 7;

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
            // OPERATION SOUND
            CastLight();
            litTime -= Time.deltaTime;
            if (litTime <= 0)
            {
                lite.enabled = false;
                litTime = litTimeMax;
            }
        }
        else if (InputWrapper.GetAxis("Flashlight", InputWrapper.InputStates.Gameplay) == 1f)
        {
            /* Flashlight charging */
            // WIND UP SOUND
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

    void CastLight()
    {
        float angleInc = lite.spotAngle / numRays;
        for (int i = 1; i <= numRays; i++)
        {
            Vector3 ray = Quaternion.Euler(0, angleInc * i - lite.spotAngle/2, 0) * transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, ray * lite.range, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, ray, out hit, lite.range))
            {
                if (hit.transform.tag == "Enemy")
                {
                    // Play enemy death animation/sound
                    // delay destroy by appropriate amount for animation/sound to play
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

}
