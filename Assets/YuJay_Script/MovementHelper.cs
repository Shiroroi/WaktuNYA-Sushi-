using UnityEngine;
using System.Collections;

public class MovementHelper : MonoBehaviour
{
    // --- 版本 1: 按指定【时间】移动 ---
    public void MoveToByTime(Transform objectToMove, Vector3 destination, float duration, AnimationCurve curve)
    {
        if (curve == null || curve.keys.Length < 2)
        {
            Debug.LogWarning("MoveToByTime was called with an invalid curve. Using default EaseInOut.");
            curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        }
        StartCoroutine(MoveCoroutine(objectToMove, destination, duration, curve));
    }

    // --- 版本 2: 按指定【速度】移动 ---
    public void MoveToBySpeed(Transform objectToMove, Vector3 destination, float speed, AnimationCurve curve)
    {
        if (speed <= 0)
        {
            Debug.LogError("Speed must be greater than zero.");
            return;
        }

        if (curve == null || curve.keys.Length < 2)
        {
            Debug.LogWarning("MoveToBySpeed was called with an invalid curve. Using default EaseInOut.");
            curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        }

        float totalDistance = Vector3.Distance(objectToMove.position, destination);
        if (totalDistance < 0.001f) return;

        // 核心区别：根据速度和距离计算出一个动态的时长
        float duration = totalDistance / speed;

        StartCoroutine(MoveCoroutine(objectToMove, destination, duration, curve));
    }


    // --- 统一的、唯一的协程 ---
    // 两个公开方法最终都调用这个协程来完成工作
    private IEnumerator MoveCoroutine(Transform objectToMove, Vector3 destination, float duration, AnimationCurve curve)
    {
        Vector3 startPosition = objectToMove.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (objectToMove == null) yield break;

            elapsedTime += Time.deltaTime;
            float timeProgress = Mathf.Clamp01(elapsedTime / duration);
            float distanceProgress = curve.Evaluate(timeProgress);
            objectToMove.position = Vector3.LerpUnclamped(startPosition, destination, distanceProgress);

            yield return null;
        }

        if (objectToMove != null)
        {
            objectToMove.position = destination;
        }
    }

    public void StopMoving()
    {
        // StopAllCoroutines() 是 Unity 自带的功能,
        // 它会立即停止这个脚本上所有正在运行的协程。
        StopAllCoroutines();
    }
}