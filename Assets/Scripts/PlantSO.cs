using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlantSO : ScriptableObject
{
    public enum PlantTypes
    {
        happy,
        deadly
    }

    [SerializeField] private PlantTypes plantType;
    [SerializeField] private Sprite plantSprite;

    public PlantTypes PlantType { get => plantType; set => plantType = value; }
    public Sprite PlantSprite { get => plantSprite; set => plantSprite = value; }
}
