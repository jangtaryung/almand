using UnityEngine;

/// <summary>
/// 카메라가 플레이어를 부드럽게 추적합니다.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 8f;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}