using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourSidedSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer rend;
    public Sprite front;
    public Sprite left;
    public Sprite right;
    public Sprite back;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pointToPlayer = player.position - transform.position;
        float angle = Vector3.SignedAngle(transform.TransformDirection(Vector3.forward), pointToPlayer, Vector3.up);

        if (Mathf.Abs(angle) < 45)
        {
            rend.sprite = front;
        }
        else if (angle >= 45 && angle < 135)
        {
            rend.sprite = right;
        }
        else if (angle <= -45 && angle > -135)
        {
            rend.sprite = left;
        }
        else if (Mathf.Abs(angle) >= 135)
        {
            rend.sprite = back;
        }
    }
}
