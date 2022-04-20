using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetlightController : MonoBehaviour
{
    private Light lite;
    //[SerializeField] private int numRays = 7;

    // Start is called before the first frame update
    void Start()
    {
        lite = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemies();
    }

    void FindEnemies()
    {
        HashSet<GameObject> enemies = ConeCast(transform.position, Vector3.down, 12, lite.spotAngle / 2);
        enemies.UnionWith(ConeCast(transform.position, Vector3.down, 6, lite.spotAngle / 4));
        enemies.UnionWith(ConeCast(transform.position, Vector3.down, 1, 0));
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }


    //change to concurrent circles inside each other
    /*void CastLight()
    {
        float angleInc = lite.spotAngle / numRays;
        for (int i = 0; i <= numRays; i++)
        {
            float angle = angleInc * i - lite.spotAngle / 2;
            Vector3 ray = Quaternion.Euler(0, 0, angle) * transform.TransformDirection(Vector3.forward);
            Vector3 ray2 = Quaternion.Euler(angle, 0, 0) * transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, ray * lite.range, Color.yellow);
            Debug.DrawRay(transform.position, ray2 * lite.range, Color.red);
            //Vector3 ray3 = Quaternion.Euler(lite.spotAngle, 0, angle) * transform.TransformDirection(Vector3.forward);
            //Debug.DrawRay(transform.position, ray3 * lite.range, Color.blue);
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
            if (Physics.Raycast(transform.position, ray2, out hit, lite.range))
            {
                if (hit.transform.tag == "Enemy")
                {
                    // Play enemy death animation/sound
                    // delay destroy by appropriate amount for animation/sound to play
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }*/

    HashSet<GameObject> ConeCast(Vector3 origin, Vector3 direction, int numberOfRays, float psi)
    {
        HashSet<GameObject> enemies = new HashSet<GameObject>();
        RaycastHit hit;
        Vector3 ray = Quaternion.Euler(0,0, psi) * direction;
        for (float i = 0; i < numberOfRays; i++)
        {
            ray = Quaternion.AngleAxis(360f / numberOfRays, direction) * ray;
            if(Physics.Raycast(origin, ray, out hit, lite.range))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    enemies.Add(hit.transform.gameObject);
                }
            }
            Debug.DrawRay(origin, ray * 5, Color.red);
        }
        return enemies;
    }

}
