using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    public float Gravity = -9.8f;

    [SerializeField] private float _movementSpeed;

    private CharacterController _characterController;
    private PlayerInput _playerInput;

    private Vector3 _movementVelocity;
    private float _verticalVelocity;
    private Quaternion _cameraRotationEuler = Quaternion.Euler(0, -45, 0);

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        CalculateMovement();


        _characterController.Move(_movementVelocity);
    }

    private void CalculateMovement()
    {
        // Horizontal Movement

        _movementVelocity.Set(_playerInput.HorizontalInput, 0, _playerInput.VerticalInput);
        _movementVelocity.Normalize();
        _movementVelocity = _cameraRotationEuler * _movementVelocity;
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

    #endregion
}
