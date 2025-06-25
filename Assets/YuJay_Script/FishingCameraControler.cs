using UnityEngine;

public class FishingCameraControler : MonoBehaviour
{
    [Header("Camera Settings")]
    [Range(1, 10)] public float cameraSpeed = 5f;
    public Transform seaTop;
    public GameObject targetPositionUp;
    public GameObject targetPositiontDown;

    [Header("Movement Profiles")]
    public MovementHelper movementHelper;
    public AnimationCurve goDownCurve;
    public AnimationCurve goUpCurve;

    // --- 私有变量 ---
    private Camera _camera;
    private CameraMotion _currentMotion = CameraMotion.stationary;
     public CameraMotion CurrentMotion
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

        // 确保所有必需的引用都已设置
        if (targetPositionUp == null || targetPositiontDown == null || movementHelper == null)
        {
            
            this.enabled = false; // 禁用此脚本以防出错
        }
    }

    void Update()
    {
        // 触发条件：只有在静止状态下才能开始下移
        if (_currentMotion == CameraMotion.stationary)
        {
            // 当鼠标进入水下时
            if (_camera.ScreenToWorldPoint(Input.mousePosition).y < seaTop.position.y && _canStartDescent)
            {
                StartMovingDown();
            }
            // 当鼠标回到水上时，重置触发条件
            else if (_camera.ScreenToWorldPoint(Input.mousePosition).y > seaTop.position.y)
            {
                _canStartDescent = true;
            }
        }

        // 持续检查移动状态（是否到达目的地）
        UpdateMovementStatus();
    }

    // --- 公开的“遥控”方法 ---
    public void ForceMoveUp()
    {
        // 已经在上移或静止时忽略命令
        if (_currentMotion == CameraMotion.goUp || _currentMotion == CameraMotion.stationary) return;

        
        _currentMotion = CameraMotion.goUp;
        movementHelper.StopMoving();
        movementHelper.MoveToBySpeed(_camera.transform, targetPositionUp.transform.position, cameraSpeed, goUpCurve);
    }

    // --- 内部逻辑方法 ---
    private void StartMovingDown()
    {
        
        _canStartDescent = false;
        _currentMotion = CameraMotion.goDown;
        movementHelper.StopMoving();
        movementHelper.MoveToBySpeed(_camera.transform, targetPositiontDown.transform.position, cameraSpeed, goDownCurve);
    }

    private void UpdateMovementStatus()
    {
        // 如果不在移动，就无需检查
        if (_currentMotion == CameraMotion.stationary) return;

        // 如果正在下移，检查是否触底
        if (_currentMotion == CameraMotion.goDown)
        {
            if (Vector3.Distance(_camera.transform.position, targetPositiontDown.transform.position) < 0.1f)
            {
                // 到达底部，立刻切换到上移状态
                
                ForceMoveUp(); // 直接调用我们即将创建的遥控方法，代码复用！
            }
        }
        // 如果正在上移，检查是否到达顶部
        else if (_currentMotion == CameraMotion.goUp)
        {
            if (Vector3.Distance(_camera.transform.position, targetPositionUp.transform.position) < 0.1f)
            {
                
                _currentMotion = CameraMotion.stationary;
                // 确保移动完全停止
                movementHelper.StopMoving();
                // 让相机精确停在目标点
                _camera.transform.position = targetPositionUp.transform.position;
            }
        }
    }
}