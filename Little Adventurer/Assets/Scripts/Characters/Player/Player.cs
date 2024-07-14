using UnityEngine;

public class Player : Character
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private PlayerInput _playerInput;
    private float _verticalVelocity;
    private Quaternion _cameraRotationEuler = Quaternion.Euler(0, -45, 0);


    [Space]
    [Header("Falling Settings")]
    [Space]

    [SerializeField] private string _groundLayerName = "ground";
    [SerializeField] private string _cursorAreaLayerName = "cursorarea";
    [SerializeField] private float _minDistanceToFall = 0.2f;
    [SerializeField] private float _maxDistanceToFall = 10f;
    [Space]
    [SerializeField] private IState.TYPE[] _fallingIgnoreStates = { IState.TYPE.Dash, IState.TYPE.Spawn };


    #endregion


    // ----------------------------------------------------------------------------------
    // Protected Methods
    // ----------------------------------------------------------------------------------

    #region Protected Methods


    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        HandleAirbone();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void HandleAirbone()
    {
        // Check all falling conditions before setting animator value

        bool isFalling = !IsGrounded();

        _animator.SetBool("Falling", isFalling);
        _playerInput.enabled = !isFalling;
    }

    private void OnDrawGizmos()
    {
        // Distance to the ground

        Gizmos.color = Color.yellow;

        RaycastHit hitGroundInfo;
        Vector3 rayStartPosition = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayStartPosition, Vector3.down, out hitGroundInfo, _maxDistanceToFall, LayerMask.NameToLayer(_groundLayerName)))
        {
            if (hitGroundInfo.distance > _minDistanceToFall)
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawLine(rayStartPosition, hitGroundInfo.point);
        }


        // Cursor position

        Ray rayFromCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cursorHitInfo;

        if (Physics.Raycast(rayFromCursor, out cursorHitInfo, 1000, 1 << LayerMask.NameToLayer(_cursorAreaLayerName)))
        {
            Gizmos.DrawWireSphere(cursorHitInfo.point, 1f);
        }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public override void ConfigureMovement()
    {
        // Horizontal Movement

        _movementVelocity.Set(_playerInput.HorizontalInput, 0, _playerInput.VerticalInput);
        _movementVelocity.Normalize();
        _movementVelocity = _cameraRotationEuler * _movementVelocity;
        _animator.SetFloat("Speed", _movementVelocity.magnitude);
        _movementVelocity *= _movementSpeed * Time.deltaTime;

        // Vertical Movement

        _verticalVelocity = (_characterController.isGrounded) ? Gravity * 0.3f : Gravity;
        _movementVelocity += _verticalVelocity * Vector3.up * Time.deltaTime;


        // Look Rotation

        Vector3 lookRotation = new Vector3(_movementVelocity.x, 0, _movementVelocity.z);

        if (lookRotation != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookRotation);
        }
    }

    public override void SetState(IState.TYPE newStateType)
    {
        base.SetState(newStateType);
    }

    public override void TakeDamage(int damage, Vector3 attackerPosition = new Vector3(), float attackForce = 1f)
    {
        if (!_isDead && !_isInvincible)
        {
            base.TakeDamage(damage, attackerPosition, attackForce);
        }
    }

    public bool IsGrounded()
    {
        bool isFalling = true;

        foreach (IState.TYPE ignoreState in _fallingIgnoreStates)
        {
            if (_currentState.Type == ignoreState)
            {
                isFalling = false;
                break;
            }
        }

        if (isFalling)
        {
            RaycastHit hit;
            Vector3 rayStartPosition = transform.position + Vector3.up * 0.1f;

            if (Physics.Raycast(rayStartPosition, Vector3.down, out hit, _maxDistanceToFall, LayerMask.NameToLayer(_groundLayerName)))
            {
                isFalling = hit.distance > _minDistanceToFall;
            }
        }

        return !isFalling;
    }

    public void LookAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer(_cursorAreaLayerName)))
        {
            Vector3 lookDirection = hit.point - transform.position;
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }

    #endregion
}

