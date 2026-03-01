using UnityEngine;

/// <summary>
/// 플레이어 이동 및 방향 처리.
/// 뱀서라이크 특성상 공격은 자동이므로 이동만 담당합니다.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("참조")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector2 _moveInput;
    private bool _facingRight = true;

    private void Update()
    {
        // 입력 받기
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        // 대각선 이동 시 속도 정규화
        if (_moveInput.sqrMagnitude > 1f)
        {
            _moveInput.Normalize();
        }

        // 좌우 반전
        UpdateFacing();
    }

    private void FixedUpdate()
    {
        // 물리 기반 이동 (FixedUpdate에서 처리)
        //_rb.linearVelocity = _moveInput * _moveSpeed;
        _rb.velocity = _moveInput * _moveSpeed;

    }

    private void UpdateFacing()
    {
        if (_moveInput.x > 0 && !_facingRight)
        {
            Flip();
        }
        else if (_moveInput.x < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        _spriteRenderer.flipX = !_facingRight;
    }

    /// <summary>
    /// 외부에서 이동속도를 변경할 때 사용 (레벨업 버프 등)
    /// </summary>
    public void SetMoveSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    public float GetMoveSpeed() => _moveSpeed;
    public Vector2 GetMoveDirection() => _moveInput;
    public bool IsMoving() => _moveInput.sqrMagnitude > 0.01f;
}