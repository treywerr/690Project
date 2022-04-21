using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public float speed = 2000;
    public float amount = 2000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var xs = 449 + Mathf.Sin(Time.time * speed) * amount;
        var xy = 4 + Mathf.Cos(Time.time * speed) * amount;
        var xz = 14 + Mathf.Sin(Time.time * speed) * amount;
        gameObject.transform.position = new Vector3(xs, xy, xz);
    }
}
