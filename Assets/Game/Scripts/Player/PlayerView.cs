using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private float _groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask _groundLayer;

    private PlayerControls _controls;

    private float _horizontalInput;
    private bool _isSprinting;
    private bool _jumpRequested;

    public float HorizontalInput => _horizontalInput;
    public bool IsSprinting => _isSprinting;

    [Inject]
    public void Construct(PlayerControls controls)
    {
        _controls = controls;
    }

    private void Update()
    {
        _horizontalInput = _controls.Player.Move.ReadValue<Vector2>().x;
        _isSprinting = _controls.Player.Sprint.IsPressed();

        if (_controls.Player.Jump.WasPressedThisFrame())
        {
            _jumpRequested = true;
        }
    }

    public void Move(float speed)
    {
        _rigidbody.linearVelocity = new Vector2(
            HorizontalInput * speed,
            _rigidbody.linearVelocity.y
        );
    }

    public void Jump(float force)
    {
        _rigidbody.linearVelocity = new Vector2(
            _rigidbody.linearVelocity.x,
            force
        );
    }

    public bool ConsumeJumpRequst()
    {
        bool requested = _jumpRequested;
        _jumpRequested = false;

        return requested;
    }

    public bool IsGrounded =>
        Physics2D.OverlapCircle(
            _groundCheck.position,
            _groundCheckRadius,
            _groundLayer
        );

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
