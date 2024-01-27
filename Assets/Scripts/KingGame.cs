using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingGame : MonoBehaviour
{
    [SerializeField] private GameObject kingGameObject;
    bool active = false;
    [SerializeField] float goodPercent;
    [SerializeField] float perfectPercent;
    [SerializeField] Image goodImage;
    [SerializeField] Image perfectImage;

    private void Start()
    {
        GameManager.Instance.OnPerform += Instance_OnPerform;
        EndKingGame();
        goodImage.fillAmount = goodPercent / 100;
        perfectImage.fillAmount = perfectPercent / 100;
    }

    private void Instance_OnPerform(object sender, ActionToListen e)
    {
        if (e == ActionToListen.performed1)
        {
            if (active)
            HitSkillCheck();
        }
    }

    private void HitSkillCheck()
    {
        EndKingGame();
    }

    private void EndKingGame()
    {
        Debug.Log("End King Game");
        active = false;
        GameManager.Instance.ChangeGameState(GameState.Free);
        GameManager.Instance.GiveActions(2);
        kingGameObject.SetActive(false);
    }

    public void StartKingGame()
    {
        active = true;
        kingGameObject.SetActive(true);
    }

    private void Update()
    {
        
    }
}
