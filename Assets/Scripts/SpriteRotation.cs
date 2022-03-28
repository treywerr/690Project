using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").transform;
            Debug.Log("Attach player to Rotation script of " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
