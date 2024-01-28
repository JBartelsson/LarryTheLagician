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
    public BossTrigger boss;
    bool active = false;
    bool minigameActive = true;
    [SerializeField] float goodPercent;
    [SerializeField] float perfectPercent;
    [SerializeField] Image goodImage;
    [SerializeField] Image perfectImage;
    [SerializeField] Image rotationObject;
    [SerializeField] private float speed = 2f;

    public Sprite kingnormal;
    public Sprite kinghappy;
    public Sprite kingextrahappy;
    public SpriteRenderer king;
    float fadeDuration = 0.5f;

    public void Start()
    {
        otherParts.SetActive(false);
        perfectText.gameObject.SetActive(false);
        goodText.gameObject.SetActive(false);
        failureText.gameObject.SetActive(false);
        actionsGroup.gameObject.SetActive(false);
        lifesGroup.gameObject.SetActive(false);
        minigameObject.DOFade(0f, 0f);

        GameManager.Instance.OnPerform += Instance_OnPerform;
        EndKingGame();
        goodImage.fillAmount = goodPercent / 100;
        perfectImage.fillAmount = perfectPercent / 100;
        boss.Init();
    }

    private void Instance_OnPerform(object sender, ActionToListen e)
    {
        if (e == ActionToListen.performed1)
        {
            HitSkillCheck();
        }
    }

    

    private void HitSkillCheck()
    {
        if (!minigameActive) return;
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
                king.sprite = kingextrahappy;
                AudioManager.Instance.PlaySFX("KingLaughingHard");


            }
            else
            {
                GameManager.Instance.GiveActions(1);
                king.sprite = kinghappy;

                actionsText.text = "+1";
                GameManager.Instance.UIAnimateObject(goodText);
                GameManager.Instance.UIAnimateObject(actionsGroup, durationWait);
                AudioManager.Instance.PlaySFX("KingLaughingMedium");

                Debug.Log("Good");

            }
        } else
        {
            GameManager.Instance.UIAnimateObject(failureText);
            GameManager.Instance.UIAnimateObject(lifesGroup, durationWait);
            GameManager.Instance.LivesLeft--;
            GameManager.Instance.GiveActions(1);
            AudioManager.Instance.PlaySFX("KingNotAmused");



        }
        Invoke(nameof(sprite), 2f);
    }

    private void sprite()
    {
        king.sprite = kingnormal;
    }

    private void EndKingGame()
    {
        Debug.Log("End King Game");
        active = false;
        minigameActive = false;

        GameManager.Instance.ChangeGameState(GameState.Free);
        minigameObject.DOFade(0f, fadeDuration);
        otherParts.SetActive(false);
    }

    public void StartKingGame()
    {
        king.sprite = kingnormal;
        active = true;
        minigameActive = true;
        otherParts.SetActive(true);
        rotationObject.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
        minigameObject.DOFade(1f, fadeDuration);

    }

    private void Update()
    {
        if (minigameActive)
        {
            Debug.Log(Time.deltaTime * speed);
            rotationObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
        }
    }
}
