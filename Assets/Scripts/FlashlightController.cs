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
                // Play enemy death animation/sound
                // delay destroy by appropriate amount for animation/sound to play
                Destroy(hit.transform.gameObject);
            }
        }
    }
    void CastLight2()
    {
        // Kill enemies via raycast
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * lite.range, Color.yellow, 10);
        Vector3 leftbound = Quaternion.Euler(0, -lite.spotAngle/2, 0) * transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, leftbound * lite.range, Color.red, 10);
        Vector3 rightbound = Quaternion.Euler(0, lite.spotAngle / 2, 0) * transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, rightbound * lite.range, Color.blue, 10);

        RaycastHit hit1;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit1, lite.range))
        {
            if (hit1.transform.tag == "Enemy")
            {
                // Play enemy death animation/sound
                // delay destroy by appropriate amount for animation/sound to play
                Destroy(hit1.transform.gameObject);
            }
        }
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, leftbound, out hit2, lite.range))
        {
            if (hit2.transform.tag == "Enemy" && hit2.transform.gameObject != hit1.transform.gameObject)
            {
                Destroy(hit2.transform.gameObject);
            }
        }
        RaycastHit hit3;
        if (Physics.Raycast(transform.position, rightbound, out hit3, lite.range))
        {
            if (hit3.transform.tag == "Enemy" && hit3.transform.gameObject != hit1.transform.gameObject && hit3.transform.gameObject != hit2.transform.gameObject)
            {
                Destroy(hit3.transform.gameObject);
            }
        }

    }
}
