using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawner
{
    static Transform spawnLocation;

    static Respawner()
    {

    }

    public static void RespawnPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject player = GameObject.Find("Player");
        player.transform.position = spawnLocation.position;
        player.transform.rotation = spawnLocation.rotation;
    }

    public static void setLocalSpawn(Transform newSpawn)
    {
        spawnLocation = newSpawn;
    }

    public static Transform getLocalSpawn()
    {
        return spawnLocation;
    }

    public static void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
        GameObject spawn = GameObject.Find("SpawnPoint");
        spawnLocation = spawn.transform;
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
    }

    public static void SaveImportantInfo()
    {
        //number of flares
        //most recent checkpoint/spawnpoint
        //current scene
        //info on what mementos have been grabbed
    }

    public static void LoadImportantInfo()
    {

    }
}
