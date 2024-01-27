using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KingGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup minigameObject;
    bool active = false;
    bool minigameActive = true;
    [SerializeField] float goodPercent;
    [SerializeField] float perfectPercent;
    [SerializeField] Image goodImage;
    [SerializeField] Image perfectImage;
    [SerializeField] Image rotationObject;
    [SerializeField] private float speed = 2f;
    float fadeDuration = 0.5f;

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
        minigameActive = false;
        float angle = Vector2.Angle(-rotationObject.transform.up, Vector2.down);
        float percent = angle / 360 * 100;
        EndKingGame();
        if (percent < goodPercent)
        {
            if (percent < perfectPercent)
            {
                Debug.Log("perfect");
                GameManager.Instance.GiveActions(2);

            }
            else
            {
                GameManager.Instance.GiveActions(1);

                Debug.Log("Good");

            }
        } else
        {
            GameManager.Instance.LivesLeft--;
            GameManager.Instance.GiveActions(1);

        }
    }

    private void EndKingGame()
    {
        Debug.Log("End King Game");
        active = false;
        GameManager.Instance.ChangeGameState(GameState.Free);
        minigameObject.DOFade(0f, fadeDuration);
    }

    public void StartKingGame()
    {
        active = true;
        minigameActive = true;

        minigameObject.DOFade(1f, fadeDuration);

    }

    private void Update()
    {
        if (minigameActive)
        {
            rotationObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
        }
    }
}
