using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Pause,
    Gamover
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameInput gameInput;
    [SerializeField] private List<RoomType> rooms;
    [SerializeField] private KingGame kingGame;
    public TextMeshProUGUI actionsUIText;
    public TextMeshProUGUI lifesUIText;

    private Room currentRoom = Room.None;
    public GameState currentGameState = GameState.King;
    private int actionsLeft = 0;
    private float dayEndDelay = 2f;
    private int livesLeft = 2;

    //King stats
    public bool kingKillable = false;

    private List<ItemSO> inventory = new();

    public bool canDoAction = true;
    public bool canDoMove = true;

    public List<ItemSO> Inventory { get => inventory; set => inventory = value; }
    public int LivesLeft { get => livesLeft; set {
            livesLeft = value;
            if (livesLeft < 0)
            {
                ChangeGameState(GameState.Gamover);
            } else
            {
                lifesUIText.text = livesLeft.ToString();

            }
        }
    }

   

    [Serializable]
    public class RoomType
    {
        public Room roomType;
        public Room upRoom;
        public Room rightRoom;
        public Room LeftRoom;
        public Room bottomRoom;
    }

    public class RoomChange
    {
        public Room newRoom;
        public Room oldRoom;
    }

    

    public event EventHandler<RoomChange> OnRoomChanged;
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
        ChangeRoom(Room.Throne);
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
        if (!canDoMove) return;
        if (currentGameState == GameState.King) return;
        Room newRoom = rooms.First((x) => x.roomType == currentRoom).bottomRoom;
        if (newRoom != Room.None)
        {
            ChangeRoom(newRoom);
        }
    }

    private void GameInput_OnUp(object sender, System.EventArgs e)
    {
        if (!canDoMove) return;
        if (currentGameState == GameState.King) return;
        Room newRoom = rooms.First((x) => x.roomType == currentRoom).upRoom;
        if (newRoom != Room.None)
        {
            ChangeRoom(newRoom);
        }
    }

    public void ChangeRoom(Room newRoom)
    {
        OnRoomChanged?.Invoke(this, new RoomChange { newRoom = newRoom, oldRoom = currentRoom });
        currentRoom = newRoom;

    }

    public void RegisterAction()
    {
        if (actionsLeft > 0)
        actionsLeft--;
        actionsUIText.text = actionsLeft.ToString();
        if (actionsLeft == 0)
        {
            canDoAction = false;
        }

    }

    public void EnableActions()
    {
        canDoAction = false;

    }

    public void DisableActions()
    {
        canDoAction = true;

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
                AudioManager.Instance.PlaySFX("Drinking");
                SceneManager.LoadScene("Win");
                Debug.Log("WIIN");

                return;
            }
            foreach (var item in GameObject.FindObjectsByType<RoomObject>(FindObjectsSortMode.None).ToList())
            {
                item.HoverObject(false);
                item.ChangeVisibility(false);
            }
            canDoMove = false;
            kingGame.StartKingGame();
            ChangeRoom(Room.Throne);

            AudioManager.Instance.PlayMusic("ThroneMusic");
        }

        if (currentGameState == GameState.Free)
        {
            canDoAction = true;
            canDoMove = true;

            OnDayEnd?.Invoke(this, EventArgs.Empty);
            ChangeRoom(Room.Throne);
            AudioManager.Instance.PlayMusic("PlottingMusic");

        }

        if (currentGameState == GameState.Gamover)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");

            foreach (var item in GameObject.FindObjectsByType<RoomObject>(FindObjectsSortMode.None).ToList())
            {
                item.HoverObject(false);
                item.ChangeVisibility(false);
            }
        }
    }

    public void UIAnimateObject(CanvasGroup go, float delay = 0f, bool noMovement = false)
    {
        Vector3 ogPos = go.transform.position;
        Sequence sequence = DOTween.Sequence();
        go.alpha = 0f;
        if (!noMovement)
        {
            sequence.AppendInterval(delay).AppendCallback(() => go.gameObject.SetActive(true)).Append(go.transform.DOMove(go.transform.position + Vector3.up * .3f, .3f)).Append(go.DOFade(1f, .3f)).AppendInterval(.2f).Append(go.DOFade(0f, .15f)).AppendCallback(() =>
            {
                go.gameObject.SetActive(false);
                go.transform.position = ogPos;
            });
        } else
        {
            sequence.AppendInterval(delay).AppendCallback(() => go.gameObject.SetActive(true)).Append(go.DOFade(1f, .3f)).AppendInterval(.2f).Append(go.DOFade(0f, .15f)).AppendCallback(() =>
            {
                go.gameObject.SetActive(false);
            });
        }
        
        sequence.Play();
            
    }

    public void GiveActions(int actions)
    {
        canDoAction = true;
        actionsLeft = actions;
        actionsUIText.text = actionsLeft.ToString();

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
