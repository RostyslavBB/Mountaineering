using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerClimbing _playerClimbing;

    [SerializeField] private LayerMask _groundLayer;

    private PlayerControls _controls;
    private PlayerModel _model;

    private float _horizontalInput;
    private float _verticalInput;
    private float _defaultGravityScale;

    private bool _isSprinting;
    private bool _jumpRequested;
    private bool _interactRequested;

    public float HorizontalInput => _horizontalInput;
    public float VerticalInput => _verticalInput;
    public bool IsSprinting => _isSprinting;
    public bool CanClimb => _playerClimbing.CanClimb;
    public float ClimbSpeed => 
        _playerClimbing.CurrentClimbable != null
        ? _model.ClimbSpeed
        : 0f;

    [Inject]
    public void Construct(PlayerControls controls, PlayerModel model)
    {
        _controls = controls;
        _model = model;
    }

    private void Awake()
    {
        _defaultGravityScale = _rigidbody.gravityScale;
    }

    private void Update()
    {
        _horizontalInput = _controls.Player.Move.ReadValue<Vector2>().x;
        _verticalInput = _controls.Player.Move.ReadValue<Vector2>().y;
        _isSprinting = _controls.Player.Sprint.IsPressed();

        if (_controls.Player.Jump.WasPressedThisFrame())
        {
            _jumpRequested = true;
        }

        if (_controls.Player.Interact.WasPressedThisFrame())
        {
            _interactRequested = true;
        }
    }

    public void SetClimbing(bool isClimbing)
    {
        _rigidbody.gravityScale = isClimbing
            ? 0f
            : _defaultGravityScale;

        StopMovement();
    }

    public void Climb(float speed)
    {
        _rigidbody.linearVelocity = new Vector2(
            0f,
            speed * _verticalInput
        );
    }

    public void StopMovement()
    {
        _rigidbody.linearVelocity = Vector2.zero;
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

    public bool ConsumeJumpRequest()
    {
        bool requested = _jumpRequested;
        _jumpRequested = false;

        return requested;
    }
    
    public bool ConsumeInteractRequest()
    {
        bool requested = _interactRequested;
        _interactRequested = false;

        return requested;
    }

    public bool IsGrounded =>
        Physics2D.OverlapCircle(
            _groundCheck.position,
            _model.GroundCheckRadius,
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
