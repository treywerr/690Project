using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorePickup : MonoBehaviour
{
    enum Pickups
    {
        Default,
        Default2
    }
    /// <summary>
    /// Which item is this? If correct item is not seen, add it to Pickups enum and make sure theres a corresponding dialogue node in the LorePickups dialogue.
    /// </summary>
    [Tooltip("If correct item is not seen, add it to Pickups enum and make sure theres a corresponding dialogue node in the LorePickups dialogue.")]
    [SerializeField] Pickups loreIndex;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<LoreDialogue>().BeginLore((int)loreIndex);
        Destroy(gameObject);
    }
}
