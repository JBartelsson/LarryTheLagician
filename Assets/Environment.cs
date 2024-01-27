using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public SpriteRenderer day;
    public SpriteRenderer night;
    public float fadeDuration = 1.5f;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
    }

    private void Instance_OnGameStateChanged(object sender, GameState e)
    {
        if (e == GameState.King)
        {
            day.DOFade(0f, fadeDuration);
            night.DOFade(1f, fadeDuration);
        } else
        {
            if (e == GameState.Free)
            {
                day.DOFade(1f, fadeDuration);
                night.DOFade(0f, fadeDuration);
            }
            
        }
    }
}
