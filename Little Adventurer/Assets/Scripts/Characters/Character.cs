using System.Collections;
using UnityEngine;



[RequireComponent(
    typeof(NormalState),
    typeof(AttackState),
    typeof(DeadState))
    ]

public abstract class Character : MonoBehaviour
{
    public enum TYPE
    {
        Player = 0,
        Enemy = 1,
    }


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

    protected MaterialPropertyBlock _materialPropertyBlock;
    protected SkinnedMeshRenderer _skinnedMeshRenderer;

    protected bool _isDead = false;

    [Space]
    [Header("Main Settings")]
    [Space]

    [SerializeField] protected TYPE _type;
    [SerializeField] protected GameObject _dropItem;

    [SerializeField] protected float _invincibleDuration = 2f;
    protected bool _isInvincible = false;

    #endregion

    // ----------------------------------------------------------------------------------
    // Abstract Methods
    // ----------------------------------------------------------------------------------

    #region Abstract Methods

    public abstract void ConfigureMovement();

    #endregion


    // ----------------------------------------------------------------------------------
    // Protected Methods
    // ----------------------------------------------------------------------------------

    #region Protected Methods

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
        SetState(IState.TYPE.Spawn);
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

    protected IEnumerator InvincibleRoutine(float duration)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(duration);
        _isInvincible = false;
    }

    protected IEnumerator DissolveMaterialRoutine(float dissolveHeightStart, float dissolveHeightEnd, float dissolveDuration, float startDelay = 0f)
    {
        // Dissolve Material parameters

        _materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        _materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeightStart);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

        yield return new WaitForSeconds(startDelay);


        float dissolveHeight;
        float dissolvePassedTime = 0f;

        // Linearly change dissolve height value

        while (dissolvePassedTime < dissolveDuration)
        {
            dissolvePassedTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeightStart, dissolveHeightEnd, dissolvePassedTime / dissolveDuration);

            _materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

            yield return null;
        }


        // Set target value 

        _materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeightEnd);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
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

    public TYPE Type
    {
        get { return _type; }
    }

    public IState CurrentState
    {
        get { return _currentState; }
    }

    public void ResetNormalState()
    {
        SetState(IState.TYPE.Normal);
    }

    public void SetSlideVelocity(Vector3 slideVelocity)
    {
        _movementVelocity = slideVelocity;
    }

    public void EnableDamageCaster()
    {
        _damageCaster.EnableDamageCaster();
    }

    public void DisableDamageCaster()
    {
        _damageCaster.DisableDamageCaster();
    }

    public void SetMovementVelocity(Vector3 velocity)
    {
        _movementVelocity = velocity;
    }

    public virtual void Move()
    {
        _characterController.Move(_movementVelocity);
        _movementVelocity = Vector3.zero;
    }

    public virtual void SetState(IState.TYPE newStateType)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }


        switch (newStateType)
        {
            default:
            case IState.TYPE.Normal:
                {
                    _currentState = GetComponent<NormalState>();
                    break;
                }

            case IState.TYPE.Dash:
                {
                    _currentState = GetComponent<DashState>();
                    break;
                }

            case IState.TYPE.Attack:
                {
                    _currentState = GetComponent<AttackState>();
                    break;
                }

            case IState.TYPE.Hurted:
                {
                    _currentState = GetComponent<HurtedState>();
                    break;
                }

            case IState.TYPE.Spawn:
                {
                    _currentState = GetComponent<SpawnState>();
                    break;
                }

            case IState.TYPE.Dead:
                {
                    _currentState = GetComponent<DeadState>();
                    _isDead = true;
                    break;
                }
        }

        _currentState.Execute();
    }

    public virtual void TakeDamage(int damage, Vector3 attackerPosition = new Vector3(), float force = 1f)
    {
        if (_health != null)
        {
            _health.TakeDamage(damage, attackerPosition, force);
            StartCoroutine(MaterialBlink());
            BecomeInvincible(_invincibleDuration);
        }
    }

    public virtual void BecomeInvincible(float duration)
    {
        StartCoroutine(InvincibleRoutine(duration));
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

    public virtual void DissolveMaterialDeath(float dissolveHeightStart, float dissolveHeightEnd, float dissolveDuration, float startDelay = 0f)
    {
        StartCoroutine(DissolveMaterialRoutine(dissolveHeightStart, dissolveHeightEnd, dissolveDuration, startDelay));

        // Off collider 

        GetComponent<Collider>().enabled = false;
    }

    public virtual void DissolveMaterial(float dissolveHeightStart, float dissolveHeightEnd, float dissolveDuration, float startDelay = 0f)
    {
        StartCoroutine(DissolveMaterialRoutine(dissolveHeightStart, dissolveHeightEnd, dissolveDuration, startDelay));
    }

    #endregion
}
