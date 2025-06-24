using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FishingCameraControler : MonoBehaviour
{

    [Header("Camera Settings: ")]
    public float targetCameraSize;

    public Vector3 cameraPositionUp;
    public GameObject targetPositionUp;
    public Vector3 cameraPositionDown;
    public GameObject targetPositiontDown;
    public bool touched;
    public float waitToTouchSecond;

    



    [Range(1, 10)] public float cameraSpeed;

    private Camera _camera;
    private float _initialCameraSize = 5;
    private Vector3 _initialCameraPosition;
    private Coroutine _waitToTouchCoroutine;
    private bool _timeUp;


    public enum CameraMotion
    {
        stationary,
        goDown,
        goUp
    }

    public CameraMotion cameraMotion = CameraMotion.stationary;

    void Start()
    {
        _camera = Camera.main;

        _camera.orthographicSize = _initialCameraSize;

        _initialCameraPosition = _camera.transform.position;

        touched = false;

        cameraPositionUp = targetPositionUp.transform.position;
        cameraPositionDown = targetPositiontDown.transform.position;

        _timeUp = false;
    }

    void Update()
    {
        CheckTouched();

        if (Input.GetKeyDown(KeyCode.O))
        {
            cameraMotion = CameraMotion.goDown;
        }

        switch (cameraMotion)
        {
            case CameraMotion.stationary:
                break;
            case CameraMotion.goDown:
                GoDown();
                break;
            case CameraMotion.goUp:
                GoUp();
                break;
        }
    }


    void Stationary()
    {
        _camera.transform.position = _initialCameraPosition;
    }
    void GoUp()
    {
        if(touched == true && _timeUp == true)
        {
            cameraPositionUp = targetPositionUp.transform.position;
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, cameraPositionUp, cameraSpeed * Time.deltaTime);
        }
        
   
    }

    void GoDown()
    {
        if (touched == false)
        {
            cameraPositionDown = targetPositiontDown.transform.position;
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, cameraPositionDown, cameraSpeed * Time.deltaTime);
        }
        
        
    }


    void CheckTouched()
    {
        if (Vector2.Distance(_camera.transform.position, cameraPositionDown) < 0.01f)
        {
            touched = true;
            _waitToTouchCoroutine = StartCoroutine(WaitToTouchCoroutine());
            
        }


        if (touched == true)
        {
            cameraMotion = CameraMotion.goUp;
        }

    }

    public IEnumerator WaitToTouchCoroutine()
    {
        yield return new WaitForSeconds(waitToTouchSecond);
        _timeUp = true;
        
    }

}
