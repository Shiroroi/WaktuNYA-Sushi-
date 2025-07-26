using Unity.Cinemachine;
using UnityEngine;

public class CameraOffsetZone : MonoBehaviour
{
    public CinemachineCamera vcam;
    public Vector3 targetOffset = new Vector3(0, 6, -10);
    public float transitionSpeed = 2f;

    private CinemachineFollow followComponent;
    private bool transitioning = false;

    private void Start()
    {
        followComponent = vcam.GetComponent<CinemachineFollow>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && followComponent != null)
        {
            StopAllCoroutines(); // stop previous transition if still running
            StartCoroutine(SmoothOffsetChange(targetOffset));
        }
    }

    private System.Collections.IEnumerator SmoothOffsetChange(Vector3 newOffset)
    {
        Vector3 startOffset = followComponent.FollowOffset;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            followComponent.FollowOffset = Vector3.Lerp(startOffset, newOffset, t);
            yield return null;
        }

        followComponent.FollowOffset = newOffset;
    }
}
