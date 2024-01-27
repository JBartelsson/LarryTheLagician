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
public enum ActionToListen
{
    performed1,
    performed2,
    performed3
}
public enum GameState
{
    King,
    Free,
    Pause
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameInput gameInput;
    [SerializeField] private List<RoomType> rooms;
    [SerializeField] private KingGame kingGame;

    private Room currentRoom = Room.Sleep;
    private GameState currentGameState = GameState.King;
    private int actionsLeft = 0;
    private float dayEndDelay = 0.5f;

    //King stats
    public bool kingKillable = false;

    private List<ItemSO> inventory = new();

    public bool CanDoAction { get
        {
            return actionsLeft > 0;
        } }

    public List<ItemSO> Inventory { get => inventory; set => inventory = value; }

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
    public event EventHandler<GameState> OnGameStateChanged;
    public event EventHandler<ActionToListen> OnPerform;
    public event EventHandler OnDayEnd;
    public event EventHandler<List<ItemSO>> OnInventoryUpdated;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnUp += GameInput_OnUp;
        gameInput.OnDown += GameInput_OnDown;
        gameInput.OnInteract1 += GameInput_OnInteract1;
        gameInput.OnInteract2 += GameInput_OnInteract2;
        gameInput.OnInteract3 += GameInput_OnInteract3;

        ChangeRoom(Room.Sleep);
        ChangeGameState(GameState.King);
        OnInventoryUpdated?.Invoke(this, inventory);
    }

    private void GameInput_OnInteract3(object sender, EventArgs e)
    {
        OnPerform?.Invoke(this, ActionToListen.performed3);
    }

    private void GameInput_OnInteract2(object sender, EventArgs e)
    {
        OnPerform?.Invoke(this, ActionToListen.performed2);

    }

    private void GameInput_OnInteract1(object sender, EventArgs e)
    {
        OnPerform?.Invoke(this, ActionToListen.performed1);
    }

    private void GameInput_OnDown(object sender, EventArgs e)
    {
        if (currentGameState == GameState.King) return;
        Room newRoom = rooms.First((x) => x.roomType == currentRoom).bottomRoom;
        if (newRoom != Room.None)
        {
            ChangeRoom(newRoom);
        }
    }

    private void GameInput_OnUp(object sender, System.EventArgs e)
    {
        if (currentGameState == GameState.King) return;
        Room newRoom = rooms.First((x) => x.roomType == currentRoom).upRoom;
        if (newRoom != Room.None)
        {
            ChangeRoom(newRoom);
        }
    }

    public void ChangeRoom(Room newRoom)
    {
        currentRoom = newRoom;
        OnRoomChanged?.Invoke(this, currentRoom);

    }

    public void RegisterAction()
    {
        if (actionsLeft > 0)
        actionsLeft--;
        if (actionsLeft == 0)
        {
            Invoke(nameof(EndDay), dayEndDelay);
        }

    }

    public void EndDay()
    {
        ChangeGameState(GameState.King);
    }

    public void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        OnGameStateChanged?.Invoke(this, currentGameState);

        if (currentGameState == GameState.King)
        {
            if (kingKillable)
            {
                Debug.Log("You win");
                return;
            }
            foreach (var item in GameObject.FindObjectsByType<RoomObject>(FindObjectsSortMode.None).ToList())
            {
                item.HoverObject(false);
                item.ChangeVisibility(false);
            }
            kingGame.StartKingGame();
            currentRoom = Room.Throne;
            AudioManager.Instance.PlayMusic("ThroneMusic");
        }

        if (currentGameState == GameState.Free)
        {
            OnDayEnd?.Invoke(this, EventArgs.Empty);
            ChangeRoom(Room.Sleep);
            AudioManager.Instance.PlayMusic("PlottingMusic");

        }
    }

    public void GiveActions(int actions)
    {
        actionsLeft = actions;
    }

    public void AddItemToInventory(ItemSO item)
    {
        inventory.Add(item);
        OnInventoryUpdated?.Invoke(this, inventory);
        Debug.Log("Added item");
    }

    public void RemoveItemFromInventory(ItemSO item)
    {
        inventory.RemoveAll((x) => x.itemtype == item.itemtype);
        OnInventoryUpdated?.Invoke(this, inventory);

    }

}
