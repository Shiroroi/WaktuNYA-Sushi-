
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FishingHookBehaviour : MonoBehaviour
{
    public float FishingHookZ;

    public FishingCameraControler fisihingCameraController;

    private Coroutine _waitToTouchedCoroutine;

    public float fishPositionGap;

    private Vector2 mousePosition;

    [Range(0.5f,0.9f)] public float cameraGoDownY = 0.7f;
    [Range(0.5f, 0f)] public float cameraGoUpY = 0.3f;




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


    // only if 什么状态检查什么，一检查到了立马调用什么函数并且更改状态不要继续检查 用 movemebnt helper
    void FollowingMouse()
    {
        switch (fisihingCameraController.CurrentMotion)
        {
            case FishingCameraControler.CameraMotion.stationary:
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePosition;
                break;

            case FishingCameraControler.CameraMotion.goDown:
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePosition.x, Camera.main.ViewportToWorldPoint(new Vector3(0f, cameraGoDownY, 0f)).y);
                break;

            case FishingCameraControler.CameraMotion.goUp:
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePosition.x, Camera.main.ViewportToWorldPoint(new Vector3(0f, cameraGoUpY, 0f)).y);
                break;
        }

        

        // transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D fishCollider)
    {

        _myFishesCollider.Add(fishCollider);
        _myFishesSprite.Add(fishCollider.GetComponentInChildren<SpriteRenderer>());


        fisihingCameraController.ForceMoveUp();

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
