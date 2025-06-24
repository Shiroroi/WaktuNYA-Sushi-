using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FishingHookBehaviour : MonoBehaviour
{
    public float FishingHookZ;
    public FishingCameraControler fisihingCameraController;

    private Coroutine _waitToTouchedCoroutine;

    public float fishPositionGap;

    

    public List<Collider2D> _myFishesCollider = new List<Collider2D>();
    public List<SpriteRenderer> _myFishesSprite = new List<SpriteRenderer>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowingMouse();

        TryCatchFish();
    }

    void FollowingMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D fishCollider)
    {

        _myFishesCollider.Add(fishCollider);
        _myFishesSprite.Add(fishCollider.GetComponentInChildren<SpriteRenderer>());


        fisihingCameraController.touched = true;
        _waitToTouchedCoroutine = StartCoroutine(fisihingCameraController.WaitToTouchCoroutine());
    }

    void TryCatchFish()
    {
        if (_myFishesCollider != null)
        {
            for (int i = 0; i < _myFishesCollider.Count; ++i)
            {
                _myFishesCollider[i].transform.position = transform.position;
                _myFishesCollider[i].transform.position = new Vector2(transform.position.x, transform.position.y - i * fishPositionGap);



            }    
        }
        if (_myFishesSprite != null)
        {
            for (int i = 0; i < _myFishesSprite.Count; ++i)
            {
                _myFishesSprite[i].sortingOrder = i;



            }
        }

    }
        
}
