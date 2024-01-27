using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LorryRoomAnimation : MonoBehaviour
{
    public Transform lorry;

    public Transform throneStairsPoint;
    public Transform throneStandingPoint;
    public Transform kitchenStairsPoint;
    public Transform kitchenStandingPoint;

    public Transform sleepStairsPoint;
    public Transform sleepStairsPointLeft;
    public Transform sleepStandingPoint;
    public Transform plantStairsPoint;
    public Transform plantStandingPoint;

    public Animator animator;


    float speed = .7f;
    float lorryFade = .1f;
    SpriteRenderer lorryImage;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnRoomChanged += Instance_OnRoomChanged;
        lorryImage = lorry.GetComponent<SpriteRenderer>();
        
        
    }

    private void Instance_OnRoomChanged(object sender, GameManager.RoomChange e)
    {
        if (e.oldRoom == Room.None)
        {
            lorry.position = throneStandingPoint.position;
            return;
        }
        if (e.oldRoom == Room.Throne && e.newRoom == Room.Kitchen)
        {
            MovePlayer(throneStairsPoint, kitchenStairsPoint, kitchenStandingPoint, false, true);
        }
        if (e.oldRoom == Room.Kitchen && e.newRoom == Room.Throne)
        {
            MovePlayer(kitchenStairsPoint, throneStairsPoint, throneStandingPoint, false, true);
        }
        if (e.oldRoom != Room.Kitchen && e.newRoom == Room.Throne)
        {
            FadePlayerToThrone();
        }
        if (e.oldRoom == Room.Kitchen && e.newRoom == Room.Sleep)
        {
            MovePlayer(kitchenStairsPoint, sleepStairsPointLeft, sleepStandingPoint, false, true);
        }
        if (e.oldRoom == Room.Sleep && e.newRoom == Room.Plant)
        {
            MovePlayer(sleepStairsPoint, plantStairsPoint, plantStandingPoint, true, false);
        }
        if (e.oldRoom == Room.Plant && e.newRoom == Room.Sleep)
        {
            MovePlayer(plantStairsPoint, sleepStairsPoint, sleepStandingPoint, true, false);
        }
        if (e.oldRoom == Room.Sleep && e.newRoom == Room.Kitchen)
        {
            MovePlayer(sleepStairsPointLeft, kitchenStairsPoint, kitchenStandingPoint, false, true);
        }
    }

    public void MovePlayer(Transform stairPoint1, Transform stairPoint2, Transform standingPoint1, bool facing1, bool facing2)
    {
        GameManager.Instance.canDoAction = false;
        animator.SetBool("facingLeft", !facing1);
        animator.SetBool("walking", true);
        AudioManager.Instance.PlaySFX("LoryWalk");
        lorry.DOMove(stairPoint1.position, speed).OnComplete(() =>
        {
            lorryImage.DOFade(0f, lorryFade).OnComplete(() =>
            {
                lorry.position = stairPoint2.position;
                animator.SetBool("facingLeft", !facing2);
                lorryImage.DOFade(1f, lorryFade).OnComplete(() =>
                {
                    lorry.DOMove(standingPoint1.position, speed).OnComplete(() =>{
                        GameManager.Instance.canDoAction = true;
                        AudioManager.Instance.StopSFX();

                        animator.SetBool("walking", false);

                    });
                });
            });
        });
    }

    public void FadePlayerToThrone()
    {
        lorryImage.DOFade(0f, lorryFade).OnComplete(() =>
        {
            lorry.position = throneStandingPoint.position;
            lorryImage.DOFade(1f, lorryFade);
            animator.SetBool("facingLeft", false);
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
