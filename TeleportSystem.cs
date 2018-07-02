using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour {

    #region public variables

    /// <summary>
    /// World Index for teleport
    /// </summary>
    public int To;
    /// <summary>
    /// Name of end location
    /// </summary>
    public string EndPoint;

    /// <summary>
    /// Player Spawn point
    /// </summary>
    [HideInInspector]
    public Transform SpawnPoint;

    #endregion

	void Start () {
        SpawnPoint = transform.Find("SpawnPoint");
	}

    /// <summary>
    /// Teleportation
    /// </summary>
    /// <param name="toWorld">World Index for teleport</param>
    /// <param name="teleportName">Name of end location</param>
    public static void Teleport(int toWorld, string teleportName)
    {
        _teleportName = teleportName;

        GameSceneManager.Load(toWorld.ToString(), SpawnAtEndPoint);
    }

    /// <summary>
    /// Teleport location
    /// </summary>
    /// <param name="teleport">Teleport origin</param>
    public void Teleport(TeleportSystem teleport)
    {
        Teleport(teleport.To, teleport.name);
    }

    /// <summary>
    /// Player spawn in specified location & mission setup
    /// </summary>
    private static void SpawnAtEndPoint()
    {
        TeleportSystem teleportSystem = FindObjectsOfType<TeleportSystem>().GetTeleport(_teleportName);

        VehiclesContainer.SpawnCar(teleportSystem.SpawnPoint.position, teleportSystem.SpawnPoint.rotation);

        MissionController.Setup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && !_teleportStart)
        {
            _teleportStart = true;
            Teleport(this);
        }
    }

    #region private variables

    /// <summary>
    /// True - if teleportation is activated, avoidance for multiple collisions 
    /// </summary>
    private bool _teleportStart = false;

    /// <summary>
    /// Name of teleport origin
    /// </summary>
    private static string _teleportName;

    #endregion
}
