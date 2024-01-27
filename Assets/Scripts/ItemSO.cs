using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    DeadlyPotion
}
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [SerializeField] public Item itemtype;
    [SerializeField] public Sprite itemSprite;

}
