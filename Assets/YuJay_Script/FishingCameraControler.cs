using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FishingCameraControler : MonoBehaviour
{
    [Header("Camera Settings")]
    [Range(1, 10)] public float cameraSpeed = 5f;
    public Transform seaTop;
    public GameObject targetPositionUp;
    public GameObject targetPositiontDown;

    [Header("Movement Profiles")]
    public MovementHelper_original movementHelper;
    public AnimationCurve goDownCurve;
    public AnimationCurve forceGoUpCurve;
    public AnimationCurve goUpCurve;

    public bool normalMode = false;
    
    private bool canNormalBgm = false;

    public GameObject waterSpalshFrefab;

    public float delayStartTime  = 1.1f;
    private float delayCounter;

    // private variable
    private Camera _camera;
    private CameraMotion _currentMotion = CameraMotion.stationary;
    public CameraMotion CurrentMotion // public properties, let other script the access our _currentMotion
    {
        get { return _currentMotion; }
    }
    private bool _canStartDescent = true;

    public enum CameraMotion
    {
        stationary,
        goDown,
        goUp
    }

    void Start()
    {
        _camera = Camera.main;

        // Defensive programming
        if (targetPositionUp == null || targetPositiontDown == null || movementHelper == null)
        {
            Debug.Log(gameObject.name + " missing something");
            this.enabled = false; // disable this script
        }

        delayCounter = 0f;

    }


    void Update()
    {
        if (delayCounter < delayStartTime)
        {
            Debug.Log(delayCounter);
            delayCounter += Time.deltaTime;
            return;
        }
        
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        // Only when camera motion is stationary, will check if
        if (_currentMotion == CameraMotion.stationary)
        {
            // the mouse pointer is under the sea top
            if (_camera.ScreenToWorldPoint(Input.mousePosition).y < seaTop.position.y && _canStartDescent)
            {
                
                StartMovingDown();
                
                
            }
            // the mouse pointer is above the sea top
            else if (_camera.ScreenToWorldPoint(Input.mousePosition).y > seaTop.position.y)
            {
                
                _canStartDescent = true;
                
                if (canNormalBgm == true)
                {
                    canNormalBgm = false;
                    
                    AudioManager.Instance.PlayMusic("Fishing_Bgm when the game haven't start");
                    AudioManager.Instance.PlaySfx("Fishing_When the hook is come up from water");
                    Instantiate(waterSpalshFrefab, new Vector3(_camera.ScreenToWorldPoint(Input.mousePosition).x, _camera.ScreenToWorldPoint(Input.mousePosition).y, 0),  Quaternion.identity);
                    
                }
                
            }
        }

        // continous check whther reach the target position
        UpdateMovementStatus();
    }

    // can force camera move up
    public void ForceMoveUp()
    {
        
        
        // if is going up or is stationary, return
        if (_currentMotion == CameraMotion.goUp || _currentMotion == CameraMotion.stationary) return;
        
        AudioManager.Instance.PlaySfx("Fishing_When the rope get stretch");
        _currentMotion = CameraMotion.goUp;
        movementHelper.StopMoving(); // stop thr going down coroutine
        movementHelper.MoveToBySpeed(_camera.transform, targetPositionUp.transform.position, cameraSpeed, forceGoUpCurve);
    }

    public void MoveUp()
    {
        
        
        // if is going up or is stationary, return
        if (_currentMotion == CameraMotion.goUp || _currentMotion == CameraMotion.stationary) return;

        AudioManager.Instance.PlaySfx("Fishing_When the rope get stretch");
        _currentMotion = CameraMotion.goUp;
        movementHelper.StopMoving(); // stop thr going down coroutine
        movementHelper.MoveToBySpeed(_camera.transform, targetPositionUp.transform.position, cameraSpeed, goUpCurve);
    }


    private void StartMovingDown()
    {
        if (normalMode == true)
        {
            
            AudioManager.Instance.PlayMusic("Fishing_Bgm when the game start");
            AudioManager.Instance.PlaySfx("Fishing_When the hook fall into water and the game start");
            canNormalBgm = true;
        }
        
        _canStartDescent = false; // if already go down, then next time mouse pointer is over the sea top only can descent again
        _currentMotion = CameraMotion.goDown;
        movementHelper.StopMoving(); // stop all coroutine first if got
        movementHelper.MoveToBySpeed(_camera.transform, targetPositiontDown.transform.position, cameraSpeed, goDownCurve); // move the camera
    }

    private void UpdateMovementStatus()
    {
        // if is not moving, no need to check
        if (_currentMotion == CameraMotion.stationary) return;

        // if is going down, check got reach the bottom ?
        if (_currentMotion == CameraMotion.goDown)
        {
            // is reach the bottom, force move up
            if (Vector3.Distance(_camera.transform.position, targetPositiontDown.transform.position) < 0.1f)
            {
                MoveUp();
            }
        }
        // if camera is going up
        else if (_currentMotion == CameraMotion.goUp)
        {
            // if also reach the position, then sop moving
            if (Vector3.Distance(_camera.transform.position, targetPositionUp.transform.position) < 0.1f)
            {

                _currentMotion = CameraMotion.stationary;

                movementHelper.StopMoving(); // stop moving coroutine

                _camera.transform.position = targetPositionUp.transform.position; // mave the posiiton euqavilent
            }
        }
    }
}