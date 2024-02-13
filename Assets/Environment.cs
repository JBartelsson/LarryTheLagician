using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public SpriteRenderer day;
    public SpriteRenderer night;
    public float fadeDuration = 1.5f;
    public GameObject moonParent;
    public Transform outside;
    public GameObject moon;
    public GameObject sun;

    Vector3 ogPos;
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        ogPos = moonParent.transform.position;
        moon.SetActive(false);
        sun.SetActive(false);
    }

    private void Instance_OnGameStateChanged(object sender, GameState e)
    {
        float moonFade = .4f;
        if (e == GameState.King)
        {
            day.DOFade(0f, fadeDuration);
            night.DOFade(1f, fadeDuration);
            moonParent.transform.DOMove(outside.position, moonFade).OnComplete(() =>
            {
                moon.SetActive(false);
                sun.SetActive(true);
                moonParent.transform.DOMove(ogPos, moonFade);
            });
        } else
        {
            if (e == GameState.Free)
            {
                day.DOFade(1f, fadeDuration);
                night.DOFade(0f, fadeDuration);
                moonParent.transform.DOMove(outside.position, moonFade).OnComplete(() =>
                {
                    moon.SetActive(true);
                    sun.SetActive(false);
                    moonParent.transform.DOMove(ogPos, moonFade);
                });
            }
            
        }
    }
}
