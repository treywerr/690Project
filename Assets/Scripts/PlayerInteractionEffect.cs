using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that defines a player interaction's effect. Basically just here for polymorphism purposes.
/// </summary>
public abstract class PlayerInteractionEffect : MonoBehaviour
{
    public abstract void ActivateEffect();
}
