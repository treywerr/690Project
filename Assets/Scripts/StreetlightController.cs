using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetlightController : MonoBehaviour
{
    private Light lite;
    private bool isFlickering = false;
    [SerializeField] private float flickerDelayMax = 0.5f;
    private SpriteRenderer liteRend;
    public Sprite unlitLamp;
    public Sprite litLamp;

    // Start is called before the first frame update
    void Start()
    {
        lite = gameObject.GetComponent<Light>();
        liteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(Flicker());
        }

        if (lite.enabled)
        {
            FindEnemies();
        }
    }

    IEnumerator Flicker()
    {
        isFlickering = true;

        lite.enabled = false;
        liteRend.sprite = unlitLamp;
        float delay = Random.Range(0.01f, flickerDelayMax);
        yield return new WaitForSeconds(delay);

        lite.enabled = true;
        liteRend.sprite = litLamp;
        delay = Random.Range(0.01f, flickerDelayMax);
        yield return new WaitForSeconds(delay);

        isFlickering = false;
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
