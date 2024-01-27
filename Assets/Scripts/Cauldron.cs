using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : RoomObject
{
    [SerializeField] private ItemSO itemToGive;
    private void Start()
    {
        GameManager.Instance.OnPerform += Instance_OnPerform;
    }

    private void Instance_OnPerform(object sender, ActionToListen e)
    {
        if (!GameManager.Instance.canDoAction) return;
        if (!objectEnabled) return;
        AudioManager.Instance.PlaySFX("PotionProduce");

        GameManager.Instance.AddItemToInventory(itemToGive);
        GameManager.Instance.RegisterAction();

    }
}
