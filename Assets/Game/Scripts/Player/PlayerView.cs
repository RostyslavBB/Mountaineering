using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerClimbing _playerClimbing;
    [SerializeField] private PlayerClimbLimit _playerClimbLimit;

    [SerializeField] private LayerMask _groundLayer;

    [Header("Fall")]
    [SerializeField] private float _fallGravityMultiplier = 2f;
    [SerializeField] private float _maxFallSpeed = 20f;

    [Header("Ledge climb")]
    [SerializeField] private float _wallProbeDistance = 0.6f;
    [SerializeField] private float _ledgeForwardOffset = 0.5f;
    [SerializeField] private float _ledgeProbeHeight = 1.5f;
    [SerializeField] private float _ledgeProbeDepth = 3f;

    private PlayerControls _controls;
    private PlayerModel _model;

    private float _horizontalInput;
    private float _verticalInput;
    private float _defaultGravityScale;

    private bool _isRunning;
    private bool _jumpRequested;
    private float _facing = 1f;
    private bool _isClimbing;

    private readonly ContactPoint2D[] _contacts = new ContactPoint2D[8];

    public float HorizontalInput => _horizontalInput;

    public float VerticalInput => _verticalInput;

    public bool IsRunning => _isRunning;

    public bool JumpRequested => _jumpRequested;

    public bool CanClimb => _playerClimbing.CanClimb;

    public float ClimbUpSpeed => 
        _playerClimbLimit.CanClimb
        ? _model.ClimbSpeed
        : 0f;

    public float ClimbDownSpeed => 
        _playerClimbing.CanClimb
        ? _model.ClimbSpeed
        : 0f;

    [Inject]
    public void Construct(
        PlayerControls controls, 
        PlayerModel model,
        PlayerAnimatorController playerAnimator)
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
        _isRunning = _controls.Player.Sprint.IsPressed();

        if (Mathf.Abs(_horizontalInput) > 0.01f)
            _facing = Mathf.Sign(_horizontalInput);

        if (_controls.Player.Jump.WasPressedThisFrame()
            && (IsGrounded || CanClimb))
        {
            _jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isClimbing) return;

        Vector2 velocity = _rigidbody.linearVelocity;

        // Падаємо швидше, ніж летимо вгору, — стрибок стає «важким», без левітації.
        _rigidbody.gravityScale = velocity.y < 0f
            ? _defaultGravityScale * _fallGravityMultiplier
            : _defaultGravityScale;

        if (velocity.y < -_maxFallSpeed)
            _rigidbody.linearVelocity = new Vector2(velocity.x, -_maxFallSpeed);
    }

    public void SetClimbing(bool isClimbing)
    {
        _isClimbing = isClimbing;

        _rigidbody.gravityScale = isClimbing
            ? 0f
            : _defaultGravityScale;

        StopMovement();
    }

    public void Climb(float speed)
    {
        _rigidbody.linearVelocity = new Vector2(
            0f,
            speed
        );
    }

    public void StopMovement()
    {
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void Move(float speed)
    {
        float horizontalVelocity = _horizontalInput * speed;

        // Не тиснемо в стіну: інакше тертя тримає гравця на ній і він не падає.
        if (IsTouchingWall(_horizontalInput))
            horizontalVelocity = 0f;

        _rigidbody.linearVelocity = new Vector2(
            horizontalVelocity,
            _rigidbody.linearVelocity.y
        );
    }

    private bool IsTouchingWall(float direction)
    {
        if (Mathf.Abs(direction) < 0.01f) return false;

        ContactFilter2D filter = new();
        filter.SetLayerMask(_groundLayer);
        filter.useTriggers = false;

        int count = _rigidbody.GetContacts(filter, _contacts);

        for (int i = 0; i < count; i++)
        {
            ContactPoint2D contact = _contacts[i];

            bool isWall = Mathf.Abs(contact.normal.x) > 0.7f;
            bool isInMoveDirection = (contact.point.x - _rigidbody.position.x) * direction > 0f;

            if (isWall && isInMoveDirection) return true;
        }

        return false;
    }

    public void Jump(float force)
    {
        _rigidbody.linearVelocity = new Vector2(
            _rigidbody.linearVelocity.x,
            force
        );
    }

    // Переносить гравця на верх блоку, якщо він доліз до краю стіни.
    public bool TryClimbOntoLedge()
    {
        float direction = GetWallDirection();

        Vector2 origin = new(
            _rigidbody.position.x + direction * _ledgeForwardOffset,
            _playerClimbLimit.transform.position.y + _ledgeProbeHeight
        );

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            _ledgeProbeDepth,
            _groundLayer
        );

        // distance == 0 означає, що промінь стартував усередині блоку — там не стати
        if (!hit || hit.distance <= 0f) return false;

        float feetOffset = _rigidbody.position.y - _groundCheck.position.y;

        Vector2 target = new(origin.x, hit.point.y + feetOffset + 0.02f);

        _rigidbody.position = target;
        transform.position = target;

        StopMovement();

        return true;
    }

    private float GetWallDirection()
    {
        Vector2 center = _playerClimbing.transform.position;

        bool wallRight = Physics2D.OverlapPoint(center + Vector2.right * _wallProbeDistance, _groundLayer);
        bool wallLeft = Physics2D.OverlapPoint(center + Vector2.left * _wallProbeDistance, _groundLayer);

        if (wallRight && !wallLeft) return 1f;
        if (wallLeft && !wallRight) return -1f;

        return _facing;
    }

    public bool ConsumeJumpRequest()
    {
        bool requested = _jumpRequested;
        _jumpRequested = false;

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
