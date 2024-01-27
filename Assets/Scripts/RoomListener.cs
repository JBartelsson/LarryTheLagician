using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomListener : MonoBehaviour
{
    // Start is called before the first frame update
    public Room roomToListen;
    [SerializeField] private GameObject gameObjectToShow;
    void Start()
    {
        GameManager.Instance.OnRoomChanged += Instance_OnRoomChanged;
    }

    private void Instance_OnRoomChanged(object sender, Room e)
    {
        if ( e == roomToListen)
        {
            Debug.Log($"{name} set Active");
            gameObjectToShow.SetActive(true);
        } else
        {
            gameObjectToShow.SetActive(false);

        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
