using UnityEngine;

public class MovementTest : MonoBehaviour
{
    [Header("Lerp:")]
    public AnimationCurve acceleration;
    public MovementHelper movementHelper;
    public Vector3 target;
    public float moveDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 调用 MovementHelper 的公开方法来移动自己 (this.transform)
            movementHelper.MoveToBySpeed(this.transform, target, moveDuration, acceleration);
        }
    }
}
