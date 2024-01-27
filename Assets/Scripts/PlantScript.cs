using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : RoomObject
{
   
    [SerializeField] ActionToListen actionToListen;
    [SerializeField] SpriteRenderer plantSprite;
    [SerializeField] GameObject plantUI;
    [SerializeField] List<PlantSO> plantSOList;
    PlantSO currentPlant;
    bool clickedThisFrame = false;

    private void Start()
    {
        //plantUI.SetActive(false);
        SetPlant(false);
        GameManager.Instance.OnPerform += OnPerformchoose;
        GameManager.Instance.OnPerform += Perform1;


    }

    private void Perform1(object sender, ActionToListen e)
    {
        if (e == actionToListen)
        ChoosePlant();
    }

    

    public void ChoosePlant()
    {
        if (!GameManager.Instance.CanDoAction) return;
        //plantUI.SetActive(false);
        if (objectEnabled && !activeObject)
        {
            HoverObject(true);
            clickedThisFrame = true;
        }
        
    }

    //Plant the plant
    private void OnPerformchoose(object sender, ActionToListen e)
    {
        
        if (!activeObject) return;
        if (e == ActionToListen.performed1)
        {
            SetPlant(true, plantSOList[0]);

        }
        if (e == ActionToListen.performed2)
        {
            SetPlant(true, plantSOList[1]);

        }
    }

    void SetPlant(bool active, PlantSO plantToPlant = null)
    {
        if (currentPlant != null) return;
        plantSprite.gameObject.SetActive(active);
        if (plantToPlant == null) return;
        currentPlant = plantToPlant;
        GameManager.Instance.RegisterAction();
        if (active)
        plantSprite.sprite = plantToPlant.PlantSprite;
    }

}