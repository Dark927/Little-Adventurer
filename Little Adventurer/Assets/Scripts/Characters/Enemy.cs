using System.Collections;
using UnityEngine;

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

    protected IEnumerator DeathRoutine(float deathDuration)
    {
        yield return new WaitForSeconds(deathDuration);

        DropItem();
        Destroy(gameObject);
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
            SetState(IState.TYPE.Attack);
        }
    }

    public override void Move()
    {
        // Enemy's normal movement controlled by CharacterController and NavMesh Agent

        if(_currentState.Type == IState.TYPE.Hurted)
        {
            base.Move();
        }
    }

    public override void TakeDamage(int damage, Vector3 attackerPosition = new Vector3(), float attackForce = 1f)
    {
        if (!_isDead && !_isInvincible)
        {
            base.TakeDamage(damage, attackerPosition, attackForce);
            _enemyVFX.BeingHitVFX(attackerPosition);
        }
    }

    public void RotateToTarget()
    {
        if (!_isDead)
        {
            transform.LookAt(_targetPlayer, Vector3.up);
        }
    }

    public override void DissolveMaterialDeath(float dissolveHeightStart, float dissolveHeightEnd, float dissolveDuration, float startDelay = 0f)
    {
        base.DissolveMaterialDeath(dissolveHeightStart, dissolveHeightEnd, dissolveDuration, startDelay);
        StartCoroutine(DeathRoutine(startDelay + dissolveDuration));
    }



    #endregion
}

