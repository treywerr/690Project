using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorePickup : MonoBehaviour
{
    protected enum Pickups
    {
        Necklace,
        StuffedSnake,
        FlashLight,
        Glasses
    }
    /// <summary>
    /// Which item is this? If correct item is not seen, add it to Pickups enum and make sure theres a corresponding dialogue node in the LorePickups dialogue.
    /// </summary>
    [Tooltip("If correct item is not seen, add it to Pickups enum and make sure theres a corresponding dialogue node in the LorePickups dialogue.")]
    [SerializeField] protected Pickups loreIndex;
    // Start is called before the first frame update
    protected virtual void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<LoreDialogue>().BeginLore((int)loreIndex);
        Destroy(gameObject);
    }
}
