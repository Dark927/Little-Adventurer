﻿using UnityEngine;

public class Enemy : Character
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private Transform _targetPlayer;
    private EnemyVFXManager _enemyVFX;

    #endregion


    // ----------------------------------------------------------------------------------
    // Protected Methods
    // ----------------------------------------------------------------------------------

    #region Protected Methods


    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _enemyVFX = GetComponent<EnemyVFXManager>();
        _targetPlayer = FindObjectOfType<Player>().transform;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public override void ConfigureMovement()
    {
        bool farFromPlayer = Vector3.Distance(_targetPlayer.position, transform.position) >= _navMeshAgent.stoppingDistance;

        if (farFromPlayer)
        {
            _navMeshAgent.SetDestination(_targetPlayer.position);
            _navMeshAgent.speed = _movementSpeed;
            _animator.SetFloat("Speed", _movementSpeed);
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            _animator.SetFloat("Speed", 0f);

            Quaternion newRotation = Quaternion.LookRotation(_targetPlayer.position - transform.position);
            transform.rotation = newRotation;
            SetState(StateType.State_attack);
        }
    }

    public override void Move()
    {
        // Enemy movement controlled by CharacterController and NavMesh Agent
    }

    public override void TakeDamage(int damage, Vector3 attackerPosition = new Vector3(), float attackForce = 1f)
    {
        if (!_isDead && !_isInvincible)
        {
            base.TakeDamage(damage);
            _enemyVFX.BeingHitVFX(attackerPosition);
        }
    }

    #endregion
}

