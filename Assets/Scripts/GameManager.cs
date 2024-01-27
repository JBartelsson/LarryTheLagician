using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum Room
{
    Kitchen,
    Sleep,
    Plant,
    Throne,
    None
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameInput gameInput;
    [SerializeField] private List<RoomType> rooms;

    private Room currentRoom = Room.Sleep;

    [Serializable]
    public class RoomType
    {
        public Room roomType;
        public Room upRoom;
        public Room rightRoom;
        public Room LeftRoom;
        public Room bottomRoom;
    }

    

    public event EventHandler<Room> OnRoomChanged;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnUp += GameInput_OnUp;
        gameInput.OnDown += GameInput_OnDown;

        OnRoomChanged?.Invoke(this, currentRoom);
    }

    private void GameInput_OnDown(object sender, EventArgs e)
    {
        Room newRoom = rooms.First((x) => x.roomType == currentRoom).bottomRoom;
        if (newRoom != Room.None)
        {
            currentRoom = newRoom;
            OnRoomChanged?.Invoke(this, currentRoom);
        }
    }

    private void GameInput_OnUp(object sender, System.EventArgs e)
    {
        Room newRoom = rooms.First((x) => x.roomType == currentRoom).upRoom;
        if (newRoom != Room.None)
        {
            currentRoom = newRoom;
            OnRoomChanged?.Invoke(this, currentRoom);
        }
    }

    public void ChangeRoom(Room room)
    {
        currentRoom = room;
    }

}
