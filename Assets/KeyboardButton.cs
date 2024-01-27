using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButton : MonoBehaviour
{
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
    }

    private void Instance_OnGameStateChanged(object sender, GameState e)
    {
        anim.Rebind();
        anim.Update(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
