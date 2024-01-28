using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : RoomObject
{

    public KingGame kingGame;
    public GameObject kingkillableGO;

    public void Init()
    {
        GameManager.Instance.OnPerform += Instance_OnPerform;
    }

    private void Update()
    {
        kingkillableGO.SetActive(GameManager.Instance.kingKillable);
    }

    private void Instance_OnPerform(object sender, ActionToListen e)
    {
        if (!objectEnabled) return;
        if (GameManager.Instance.currentGameState != GameState.King)
        GameManager.Instance.ChangeGameState(GameState.King);
        

    }
}
