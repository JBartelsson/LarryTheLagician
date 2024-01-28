using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cook : RoomObject
{
    public Transform point1;
    public Transform point2;

    SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        transform.position = point1.position;
        GameManager.Instance.OnPerform += Instance_OnPerform;
        //Sequence s2 = DOTween.Sequence();
        //Sequence s1 = DOTween.Sequence().Append(transform.DOMove(point2.position, .8f)).AppendCallback(() => s2.Play());
        //s2 = DOTween.Sequence().Append(transform.DOMove(point1.position, .8f)).AppendCallback(() => s1.Play());
        //s1.Play();
        float duration = 2f;
        DOTween.Sequence().Append(transform.DOMove(point2.position, duration)).AppendCallback(() =>
        {
            sprite.flipX = false;
        }).AppendInterval(0f).Append(transform.DOMove(point1.position, duration)).AppendCallback(() =>
        {
            sprite.flipX = true;
        }).SetLoops(-1, LoopType.Restart).Play();
    }

    private void Instance_OnPerform(object sender, ActionToListen e)
    {
        if (!GameManager.Instance.canDoAction) return;
        if (!objectEnabled) return;
        if (GameManager.Instance.Inventory.Any((x) => x.itemtype == Item.DeadlyPotion))
        {
            Debug.Log("King is killable");
            GameManager.Instance.kingKillable = true;
            GameManager.Instance.RegisterAction();
        }

    }
}
