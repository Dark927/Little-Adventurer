using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    public float Gravity = -9.8f;
    [SerializeField] protected float _movementSpeed;

    protected CharacterController _characterController;
    protected Animator _animator;

    protected Vector3 _movementVelocity;


    #endregion


    // ----------------------------------------------------------------------------------
    // Protected Methods
    // ----------------------------------------------------------------------------------

    #region Protected Methods
    protected abstract void ConfigureMovement();


    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        ConfigureMovement();
    }

    #endregion
}
