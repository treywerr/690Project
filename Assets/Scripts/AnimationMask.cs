using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMask : MonoBehaviour
{
    private SpriteMask mask;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<SpriteMask>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mask.sprite = rend.sprite;
    }
}
