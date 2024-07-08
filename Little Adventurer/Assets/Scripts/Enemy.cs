using UnityEngine;

public class Enemy : Character
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private Transform _targetPlayer;


    #endregion


    // ----------------------------------------------------------------------------------
    // Protected Methods
    // ----------------------------------------------------------------------------------

    #region Protected Methods


    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _targetPlayer = FindObjectOfType<Player>().transform;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ConfigureMovement()
    {
        bool farFromPlayer = Vector3.Distance(_targetPlayer.position, transform.position) >= _navMeshAgent.stoppingDistance;

        if(farFromPlayer)
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

    #endregion
}

