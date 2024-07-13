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

    private int _cointsAmount = 0;

    [Space]
    [Header("Falling Settings")]
    [Space]

    [SerializeField] private string _groundLayerName = "ground";
    [SerializeField] private float _minDistanceToFall = 0.2f;
    [SerializeField] private float _maxDistanceToFall = 10f;
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

        _animator.SetBool("Falling", isFalling);
        _playerInput.enabled = !isFalling;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        RaycastHit hit;
        Vector3 rayStartPosition = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayStartPosition, Vector3.down, out hit, _maxDistanceToFall, LayerMask.NameToLayer(_groundLayerName)))
        {
            if (hit.distance > _minDistanceToFall)
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawLine(rayStartPosition, hit.point);
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

    public void TakeCoins(int coins)
    {
        _cointsAmount += coins;
    }

    public override void Move()
    {
        _characterController.Move(_movementVelocity);
        _movementVelocity = Vector3.zero;
    }

    #endregion
}

