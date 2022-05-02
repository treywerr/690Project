using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour
{
    public SpriteRenderer face;
    public SpriteRenderer body;
    private float opacity = 1f;
    [SerializeField] private float duration = 2f; // Set this to be length of death sound or death animation, whichever is longer

    public AudioSource source;
    public AudioClip deathSound;

    public void KillEnemy()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        //AIcontroller.enabled = false;
        Destroy(GetComponent<It4Enemy>());
        Destroy(GetComponent<NavMeshAgent>());

        // Play death sound
        source.clip = deathSound;
        source.Play();

        // Play death animation
        // Lacking an animation currently, instead fading enemy out.
        while (opacity > 0)
        {
            opacity -= (1f / duration) * Time.deltaTime;
            if (opacity < 0)
                opacity = 0; // catch edge case of negative opacity
            Color color = Color.white;
            color.a = opacity;
            face.color = color;
            body.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}
