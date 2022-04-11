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
    private float intens;
    [SerializeField] private int numRays = 7;

    // Start is called before the first frame update
    void Start()
    {
        lite.enabled = false;
        intens = lite.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargeTime <= 0)
        {
            /* Flashlight lit */
            // OPERATION SOUND
            lite.intensity = intens;
            lite.enabled = true;
            CastLight();
            litTime -= Time.deltaTime;
            if (litTime <= 0)
            {
                lite.enabled = false;
                litTime = litTimeMax;
                chargeTime = chargeTimeMax;
            }
        }
        else if (InputWrapper.GetAxis("Flashlight", InputWrapper.InputStates.Gameplay) == 1f)
        {
            /* Flashlight charging */
            // WIND UP SOUND
            chargeTime -= Time.deltaTime;
            // Flicker
            lite.intensity = 1;
            lite.enabled = Flicker(chargeTime);
        }
        else
        {
            chargeTime = chargeTimeMax;
        }
    }

    bool Flicker(float x)
    {
        return (Mathf.Cos(chargeTimeMax/ (x*x)) < 0);
    }

    void CastLight()
    {
        float angleInc = lite.spotAngle / numRays;
        for (int i = 0; i <= numRays; i++)
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
