using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light lite;
    public float chargeTimeMax = 5;
    private float chargeTime = 0;
    private float litTime = 2;
    private float intens;
    [SerializeField] private int numRays = 7;
    //private bool cooldown = false;

    private enum LightState
    {
        Ready,
        Charging,
        Lit,
        Uncharging
    }
    LightState currentState;

    // Audio
    public AudioSource source;
    public AudioClip chargeSound;
    public AudioClip runningSound;
    public AudioClip windDownSound;

    // Start is called before the first frame update
    void Start()
    {
        lite.enabled = false;
        intens = lite.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == LightState.Ready)
        {
            if(InputWrapper.GetAxis("Flashlight", InputWrapper.InputStates.Gameplay) == 1f)
            {
                currentState = LightState.Charging;
                chargeTime += Time.deltaTime;
                source.clip = chargeSound;
                source.Play();
                source.time = 0f;
            }
        }
        else if(currentState == LightState.Charging)
        {
            if(InputWrapper.GetAxis("Flashlight", InputWrapper.InputStates.Gameplay) == 1f)
            {
                chargeTime = Mathf.Clamp(chargeTime + Time.deltaTime, 0, chargeTimeMax);
                if(chargeTime == chargeTimeMax)
                {
                    currentState = LightState.Lit;
                    source.clip = runningSound;
                    source.Play();
                    source.time = 0;
                    chargeTime += litTime;
                    lite.intensity = intens;
                    lite.enabled = true;
                    CastLight();
                }
                else
                {
                    lite.intensity = 1;
                    lite.enabled = Flicker(chargeTimeMax - chargeTime);
                }
            }
            else
            {
                chargeTime = Mathf.Clamp(chargeTime - Time.deltaTime, 0, chargeTimeMax);
                currentState = LightState.Uncharging;
                source.clip = windDownSound;
                source.Play();
                source.time = 5 - chargeTime;
                lite.enabled = false;
            }
        }
        else if(currentState == LightState.Lit)
        {
            chargeTime = Mathf.Clamp(chargeTime - Time.deltaTime, chargeTimeMax, chargeTimeMax + litTime);
            if (chargeTime == chargeTimeMax)
            {
                currentState = LightState.Uncharging;
                source.clip = windDownSound;
                source.Play();
                source.time = 0f;
                lite.enabled = false;
            }
            else
            {
                CastLight();
            }
        }
        else
        {
            chargeTime = Mathf.Clamp(chargeTime - Time.deltaTime, 0, chargeTimeMax);
            if(chargeTime == 0)
            {
                currentState = LightState.Ready;
            }
        }
        if(currentState != LightState.Ready)
        {
            StealthController.Request(3f, 1);
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
                    hit.transform.gameObject.GetComponent<EnemyDeath>().KillEnemy();
                }
            }
        }
    }
    /*
    IEnumerator startCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(5f);
        cooldown = false;
    }*/

}
