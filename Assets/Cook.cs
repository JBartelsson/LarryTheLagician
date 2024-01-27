using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cook : RoomObject
{
    private void Start()
    {
        GameManager.Instance.OnPerform += Instance_OnPerform;
    }

    private void Instance_OnPerform(object sender, ActionToListen e)
    {
        if (!GameManager.Instance.CanDoAction) return;
        if (!objectEnabled) return;
        if (GameManager.Instance.Inventory.Any((x) => x.itemtype == Item.DeadlyPotion))
        {
            Debug.Log("King is killable");
            GameManager.Instance.kingKillable = true;
        }
        GameManager.Instance.RegisterAction();

    }
}
