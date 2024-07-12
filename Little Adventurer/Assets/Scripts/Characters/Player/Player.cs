﻿using UnityEngine;

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

    [Space]
    [Header("Falling Settings")]
    [Space]

    [SerializeField] protected string _groundLayerName = "ground";
    [SerializeField] protected float _minDistanceToFall = 0.2f;
    [SerializeField] protected float _maxDistanceToFall = 10f;
    [SerializeField] protected StateType[] _fallingIgnoreStates = { StateType.State_dash, StateType.State_spawn };


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

    private void HandleAirbone()
    {
        // Check all falling conditions before setting animator value

        bool isFalling = true;

        foreach (StateType stateType in _fallingIgnoreStates)
        {
            if (_currentStateType == stateType)
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

    public override void SetState(StateType newStateType)
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

