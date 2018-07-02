using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour {

    #region public variables

    /// <summary>
    /// Индекс мира к которому идет телепорт
    /// </summary>
    public int To;
    /// <summary>
    /// Название конечного телепорта
    /// </summary>
    public string EndPoint;

    /// <summary>
    /// Точка спавна игрока
    /// </summary>
    [HideInInspector]
    public Transform SpawnPoint;

    #endregion

	void Start () {
        SpawnPoint = transform.Find("SpawnPoint");
	}

    /// <summary>
    /// Телепортация
    /// </summary>
    /// <param name="toWorld">Индекс мира в который нужно телепортироватся</param>
    /// <param name="teleportName">Название телепорта к которому нужно телепортироватся</param>
    public static void Teleport(int toWorld, string teleportName)
    {
        _teleportName = teleportName;

        GameSceneManager.Load(toWorld.ToString(), SpawnAtEndPoint);
    }

    /// <summary>
    /// Тулупортация
    /// </summary>
    /// <param name="teleport">Телепорт от которого произошел вызов</param>
    public void Teleport(TeleportSystem teleport)
    {
        Teleport(teleport.To, teleport.name);
    }

    /// <summary>
    /// Спавн игрока в указаном месте и настройка миссии
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
    /// True - если телепортация активировалась, нужно что бы не сработала несколько раз колизия 
    /// </summary>
    private bool _teleportStart = false;

    /// <summary>
    /// Название последнего используемого телепорта
    /// </summary>
    private static string _teleportName;

    #endregion
}
