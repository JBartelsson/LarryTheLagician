using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomScriptDisabler : MonoBehaviour
{
    [SerializeField] List<RoomObject> roomObjectsToManage;
    [SerializeField] Room room;

    private void Start()
    {
        GameManager.Instance.OnRoomChanged += Instance_OnRoomChanged;
    }

    private void Instance_OnRoomChanged(object sender, GameManager.RoomChange e)
    {
        if (room != e.newRoom)
        {
            roomObjectsToManage.ForEach((x) =>
            {
                x.HoverObject(false);
                x.ChangeVisibility(false);
            });
            
        } else
        {
            roomObjectsToManage.ForEach((x) =>
            {
                x.ChangeVisibility(true);
                x.HoverObject(false);

            });

        }
    }
}
