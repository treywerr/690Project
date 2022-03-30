using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    public float speed = 1.0f; //how fast it shakes
    public float amount = 1.0f; //how much it shakes
    // Start is called before the first frame update
    void Start()
    {
        
    }

 
void Update()
{
    var xs = 449 + Mathf.Sin(Time.time * speed) * amount;
    var xy = 4 + Mathf.Cos(Time.time * speed) * amount;
    var xz = 14 + Mathf.Sin(Time.time * speed) * amount;
    gameObject.transform.position = new Vector3(xs, xy, xz);
}

}
 
