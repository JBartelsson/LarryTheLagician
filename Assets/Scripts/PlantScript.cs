using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : RoomObject
{
   
    [SerializeField] ActionToListen actionToListen;
    [SerializeField] SpriteRenderer plantSprite;
    [SerializeField] GameObject activeMarker;
    [SerializeField] List<PlantSO> plantSOList;
    PlantSO currentPlant;
    bool clickedThisFrame = false;

    private void Start()
    {
        //plantUI.SetActive(false);
        SetPlant(false);
        GameManager.Instance.OnPerform += OnPerformchoose;
        GameManager.Instance.OnPerform += Perform1;
        GameManager.Instance.OnDayEnd += Instance_OnDayEnd;


    }

    private void Instance_OnDayEnd(object sender, System.EventArgs e)
    {
        Harvest();
    }

    private void Perform1(object sender, ActionToListen e)
    {
        Debug.Log($"{name} plant listened to Perform");
        if (!objectEnabled) return;
        if (e == actionToListen)
        ChoosePlant();
    }

    

    public void ChoosePlant()
    {
        if (!GameManager.Instance.canDoAction) return;
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
            Debug.Log("Planted Happy");
            SetPlant(true, plantSOList[0]);

        }
        if (e == ActionToListen.performed2)
        {
            Debug.Log("Planted Dead");

            SetPlant(true, plantSOList[1]);

        }
    }

    void SetPlant(bool active, PlantSO plantToPlant = null)
    {
        plantSprite.gameObject.SetActive(active);
        if (plantToPlant == null) return;
        currentPlant = plantToPlant;
        GameManager.Instance.RegisterAction();
        if (active)
        plantSprite.sprite = plantToPlant.PlantSprite;
    }

    void Harvest()
    {
        SetPlant(false);
        currentPlant = null;
    }

    public override void HoverObject(bool active)
    {
        base.HoverObject(active);
        activeMarker.SetActive(active);
    }
}