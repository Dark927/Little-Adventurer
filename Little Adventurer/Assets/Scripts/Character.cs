using System.Collections;
using UnityEngine;

public enum CharacterType
{
    Character_player = 0,
    Character_enemy = 1,
}

[RequireComponent(
    typeof(NormalState),
    typeof(AttackState),
    typeof(DeadState))
    ]

public abstract class Character : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("Movement")]
    [Space]

    public float Gravity = -9.8f;
    [SerializeField] protected float _movementSpeed;
    protected Vector3 _movementVelocity;
    [HideInInspector] public Vector3 ActualImpactOn;

    protected CharacterController _characterController;
    protected Animator _animator;
    protected Health _health;
    protected DamageCaster _damageCaster;

    protected IState _currentState;
    protected StateType _currentStateType;

    protected MaterialPropertyBlock _materialPropertyBlock;
    protected SkinnedMeshRenderer _skinnedMeshRenderer;

    protected bool _isDead = false;

    [Space]
    [Header("Main Settings")]
    [Space]

    [SerializeField] protected CharacterType _characterType;
    [SerializeField] protected GameObject _dropItem;

    [SerializeField] protected float _invincibleDuration = 2f;
    protected bool _isInvincible = false;

    #endregion


    // ----------------------------------------------------------------------------------
    // Protected Methods
    // ----------------------------------------------------------------------------------

    #region Protected Methods

    public abstract void ConfigureMovement();


    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _damageCaster = GetComponentInChildren<DamageCaster>();

        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _skinnedMeshRenderer.GetPropertyBlock(_materialPropertyBlock);
    }

    private void Start()
    {
        SetState(StateType.State_normal);
    }

    protected virtual void FixedUpdate()
    {

    }

    protected IEnumerator MaterialBlink()
    {
        _materialPropertyBlock.SetFloat("_blink", 0.4f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

        yield return new WaitForSeconds(0.2f);

        _materialPropertyBlock.SetFloat("_blink", 0f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    protected IEnumerator InvincibleRoutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibleDuration);
        _isInvincible = false;
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public bool IsDead
    {
        get { return _isDead; }
    }

    public CharacterType GetCharacterType()
    {
        return _characterType;
    }


    public virtual void SetState(StateType newStateType)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }


        _currentStateType = newStateType;

        switch (_currentStateType)
        {
            default:
            case StateType.State_normal:
                {
                    _currentState = GetComponent<NormalState>();
                    break;
                }

            case StateType.State_attack:
                {
                    _currentState = GetComponent<AttackState>();
                    break;
                }

            case StateType.State_hurted:
                {
                    _currentState = GetComponent<HurtedState>();
                    break;
                }

            case StateType.State_dead:
                {
                    _currentState = GetComponent<DeadState>();
                    _isDead = true;
                    break;
                }
        }

        _currentState.Execute();
    }

    public void ResetNormalState()
    {
        SetState(StateType.State_normal);
    }

    public void SetSlideVelocity(Vector3 slideVelocity)
    {
        _movementVelocity = slideVelocity;
    }

    public virtual void TakeDamage(int damage, Vector3 attackerPosition = new Vector3(), float force = 1f)
    {
        if (_health != null)
        {
            _health.TakeDamage(damage, attackerPosition, force);
            StartCoroutine(MaterialBlink());
            StartCoroutine(InvincibleRoutine());
        }
    }

    public void EnableDamageCaster()
    {
        _damageCaster.EnableDamageCaster();
    }

    public void DisableDamageCaster()
    {
        _damageCaster.DisableDamageCaster();
    }

    public virtual void DropItem()
    {
        if (_dropItem != null)
        {
            Instantiate(_dropItem, transform.position, Quaternion.identity);
        }
    }

    public void AddImpact(Vector3 attackerPosition, float force)
    {
        Vector3 impactDirection = (transform.position - attackerPosition).normalized;
        impactDirection.y = 0;

        ActualImpactOn = impactDirection * force;
    }

    public void SetMovementVelocity(Vector3 velocity)
    {
        _movementVelocity = velocity;
    }

    #endregion
}
