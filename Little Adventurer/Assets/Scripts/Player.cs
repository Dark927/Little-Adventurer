using UnityEngine;

public class Player : Character
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    protected PlayerInput _playerInput;
    protected float _verticalVelocity;
    protected Quaternion _cameraRotationEuler = Quaternion.Euler(0, -45, 0);

    protected int _cointsAmount = 0;

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

        _characterController.Move(_movementVelocity);
        _movementVelocity = Vector3.zero;
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

        // Set Fall State

        _animator.SetBool("Falling", !_characterController.isGrounded);
    }

    public override void SetState(StateType newStateType)
    {        
        base.SetState(newStateType);

        if (newStateType == StateType.State_normal)
        {
            _playerInput.enabled = true;
        }
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

    #endregion
}

