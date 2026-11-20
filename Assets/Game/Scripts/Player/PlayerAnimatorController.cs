using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    [SerializeField] private string _moveSpeed;
    [SerializeField] private string _climbY;
    [SerializeField] private string _isRunning;
    [SerializeField] private string _isGrounded;
    [SerializeField] private string _isJumping;
    [SerializeField] private string _isClimbing;

    public void SetMoveSpeed(float value) =>
        _animator.SetFloat(_moveSpeed, value);

    public void SetRunning(bool value) =>
        _animator.SetBool(_isRunning, value);
    
    public void SetJumping(bool value) =>
        _animator.SetBool(_isJumping, value);
    
    public void SetGrounded(bool value) =>
        _animator.SetBool(_isGrounded, value);
    
    public void SetClimbing(bool value) =>
        _animator.SetBool(_isClimbing, value);
    
    public void SetClimbY(float value) =>
        _animator.SetFloat(_climbY, value);
    
    public void SetDirection(float direction)
    {
        if (direction != 0)
            _spriteRenderer.flipX = direction < 0;
    } 
}
