using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomListener : MonoBehaviour
{
    // Start is called before the first frame update
    public Room roomToListen;
    [SerializeField] private List<GameObject> gameObjectToShow;
    void Start()
    {
        GameManager.Instance.OnRoomChanged += Instance_OnRoomChanged;
    }

    private void Instance_OnRoomChanged(object sender, GameManager.RoomChange e)
    {
        if ( e.newRoom == roomToListen)
        {
            Debug.Log($"{name} set Active");
            gameObjectToShow.ForEach((x) => x.SetActive(true));
        } else
        {
            gameObjectToShow.ForEach((x) => x.SetActive(false));


        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
