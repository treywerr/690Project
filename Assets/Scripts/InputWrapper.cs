using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wrapper for the Input class that neutralizes controls based on the current control state of the game.
/// </summary>
public class InputWrapper
{
    /// <summary>
    /// The different states that define how much control the player has over the actions on screen.
    /// </summary>
    public enum InputStates
    {
        /// <summary>
        /// Player is moving, shooting, and solving puzzles.
        /// </summary>
        Gameplay, //default moving and puzzle-solving stuff

        /// <summary>
        /// Player is looking at a menu, such as the main menu or a death screen.
        /// </summary>
        Menus, // only UI stuff: pause menu, main menu, death screen, etc 

        /// <summary>
        /// Player is watching a scripted event, no control allowed.
        /// </summary>
        Cutscene //no player controls at all, input disabled (maybe allow pausing still?)
    }

    /// <summary>
    /// The current control state that defines what inputs the player can use.
    /// </summary>
    public static InputStates currentState = InputStates.Gameplay;

    static InputWrapper()
    {

    }

    /// <summary>
    /// Change the current control state of the player.
    /// </summary>
    /// <param name="newState">The new control state of the player.</param>
    public static void ChangeState(InputStates newState)
    {
        currentState = newState;
    }

    /// <summary>
    /// Gets the input as modified by the current control state.
    /// </summary>
    /// <param name="key">The key code of the desired keyboard key.</param>
    /// <returns>Returns true while the user holds the key identified by key, as long as the game is in a play state.</returns>
    public static bool GetKey(KeyCode key)
    {
        if (currentState == InputStates.Gameplay)
        {
            return Input.GetKey(key);
        }
        return false;
    }

    /// <summary>
    /// Gets the input as modified by the current control state.
    /// </summary>
    /// <param name="axisName">The name of the axis for the desired input.</param>
    /// <returns>Float depicting value of the given input axis.</returns>
    public static float GetMovement(string axisName)
    {
        if (currentState == InputStates.Gameplay)
        {
            return Input.GetAxisRaw(axisName);
        }
        return 0;
    }

    /// <summary>
    /// Gets whether or not the player can move, as defined by the current control state.
    /// </summary>
    /// <returns>Bool: True if player can move, False otherwise</returns>
    public static bool GetCanMove()
    {
        return currentState == InputStates.Gameplay;
    }

    /// <summary>
    /// Gets the value of the "Pause" input axis, as modified by the control state.
    /// </summary>
    /// <returns></returns>
    public static float GetPause()
    {
        if (currentState == InputStates.Gameplay || currentState == InputStates.Menus)
        {
            return Input.GetAxisRaw("Pause");
        }
        return 0;
    }

    /// <summary>
    /// Gets the value of the "Fire1" input axis, as modified by the control state.
    /// </summary>
    /// <returns></returns>
    public static float GetShoot()
    {
        if (currentState == InputStates.Gameplay)
        {
            return Input.GetAxisRaw("Fire1");
        }
        return 0;
    }

    /// <summary>
    /// Gets whether or not the player should be able to aim in the current control state.
    /// </summary>
    /// <returns></returns>
    public static bool GetCanAim()
    {
        return currentState == InputStates.Gameplay;
    }

    /// <summary>
    /// Gets the value of the "Submit" axis for menuing based on current control state.
    /// </summary>
    /// <returns></returns>
    public static float GetMenuSubmit()
    {
        if (currentState == InputStates.Menus)
        {
            return Input.GetAxisRaw("Submit");
        }
        return 0;
    }

    /// <summary>
    /// Gets the value of the "Cancel" axis for menuing based on current control state.
    /// </summary>
    /// <returns></returns>
    public static float GetMenuCancel()
    {
        if (currentState == InputStates.Menus)
        {
            return Input.GetAxisRaw("Cancel");
        }
        return 0;
    }

    /// <summary>
    /// Gets the value of the "Interact" axis based on current control state.
    /// </summary>
    /// <returns></returns>
    public static float GetInteract()
    {
        if (currentState == InputStates.Gameplay)
        {
            return Input.GetAxisRaw("Interact");
        }
        return 0;
    }
}
