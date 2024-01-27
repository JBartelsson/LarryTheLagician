using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateDisabler : MonoBehaviour
{
    [SerializeField] List<RoomObject> roomObjectsToManage;
    [SerializeField] GameObject room;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged; ;
    }

    private void Instance_OnGameStateChanged(object sender, GameState e)
    {

    }
}
