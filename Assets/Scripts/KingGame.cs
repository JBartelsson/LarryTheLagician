using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KingGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup minigameObject;
    public CanvasGroup perfectText;
    public CanvasGroup goodText;
    public CanvasGroup failureText;
    public CanvasGroup actionsGroup;
    public TextMeshProUGUI actionsText;
    public CanvasGroup lifesGroup;
    public GameObject otherParts;
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
        perfectText.gameObject.SetActive(false);
        goodText.gameObject.SetActive(false);
        failureText.gameObject.SetActive(false);
        actionsGroup.gameObject.SetActive(false);
        lifesGroup.gameObject.SetActive(false);
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
        float angle = Vector2.SignedAngle(-rotationObject.transform.up, Vector2.down);
        if (angle < 0) angle += 180;
        float percent = angle / 360 * 100;
        float delay = 2f;
        float durationWait = .8f;
        Invoke(nameof(EndKingGame), delay);
        if (percent < goodPercent)
        {
            if (percent < perfectPercent)
            {
                Debug.Log("perfect");
                GameManager.Instance.UIAnimateObject(perfectText);
                actionsText.text = "+2";
                GameManager.Instance.UIAnimateObject(actionsGroup, durationWait);

                GameManager.Instance.GiveActions(2);

            }
            else
            {
                GameManager.Instance.GiveActions(1);
                actionsText.text = "+1";
                GameManager.Instance.UIAnimateObject(goodText);
                GameManager.Instance.UIAnimateObject(actionsGroup, durationWait);

                Debug.Log("Good");

            }
        } else
        {
            GameManager.Instance.UIAnimateObject(failureText);
            GameManager.Instance.UIAnimateObject(lifesGroup, durationWait);
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
        otherParts.SetActive(true);
    }

    public void StartKingGame()
    {
        active = true;
        minigameActive = true;
        otherParts.SetActive(true);

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
