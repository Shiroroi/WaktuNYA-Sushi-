
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

    [Range(.1f,10f)] public float goUpAdjustSpeed = 1f;
    [Range(.1f,10f)] public float goDownAdjustSpeed = 1f;
    [Range(.1f,100f)] public float stationaryAdjustSpeed = 1f;
 

    public List<Collider2D> _myFishesCollider = new List<Collider2D>();
    public List<SpriteRenderer> _myFishesSprite = new List<SpriteRenderer>();
    public List<Collider2D> allFishesCollider = new List<Collider2D>();
    
    private int fishCount = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Collider2D col in allFishesCollider)
        {
            col.isTrigger = true;
        }
        fishCount = 0;
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
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;
        
        switch (fisihingCameraController.CurrentMotion)
        {
            case FishingCameraControler.CameraMotion.stationary:
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                if (Vector2.Distance(transform.position, mousePosition) > 1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, mousePosition, stationaryAdjustSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = mousePosition;
                }
                    
                
                break;

            case FishingCameraControler.CameraMotion.goDown:
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePosition.x, Camera.main.ViewportToWorldPoint(new Vector3(0f, Mathf.MoveTowards(Camera.main.WorldToViewportPoint(new Vector3(0f, transform.position.y, 0f)).y,
                            cameraGoDownY, goDownAdjustSpeed * Time.deltaTime), 0f)).y);
                break;

            case FishingCameraControler.CameraMotion.goUp:
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePosition.x,
                    Camera.main.ViewportToWorldPoint(new Vector3(0f, Mathf.MoveTowards(Camera.main.WorldToViewportPoint(new Vector3(0f, transform.position.y, 0f)).y, cameraGoUpY, goUpAdjustSpeed * Time.deltaTime), 0f)).y);
                break;
        }

        

        // transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D fishCollider)
    {
        if (fishCollider.gameObject.layer == LayerMask.NameToLayer("Water"))
            return;
        
        ++fishCount;
        
        if (fishCount <= 5)
        {
            AudioManager.Instance.PlaySfx("Fishing_When catching fish");
            _myFishesCollider.Add(fishCollider);
            _myFishesSprite.Add(fishCollider.GetComponentInChildren<SpriteRenderer>());

            if (fishCount == 5)
            {
                Debug.Log("LLL");
                foreach (Collider2D col in allFishesCollider)
                {
                    col.isTrigger = false;
                }
                
            }
        }
        
        

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
