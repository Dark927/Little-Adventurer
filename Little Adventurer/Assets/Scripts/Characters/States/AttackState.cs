using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields


    [SerializeField] private float _attackSlideDuration = 0.1f;
    [SerializeField] private float _attackSlideSpeed = 0.5f;
    private float _attackStartTime;

    private Character _character;
    private Animator _animator;
    private DamageCaster _damageCaster;

    private float _attackAnimationDuration;
    private IState.TYPE _stateType = IState.TYPE.Attack;


    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
        _damageCaster = GetComponentInChildren<DamageCaster>();
    }

    private void Update()
    {
        ConfigurePlayerAttack();
    }

    private void ConfigurePlayerAttack()
    {
        if (_character.Type == Character.TYPE.Player)
        {
            // Attack slide 

            float timePassed = Time.time - _attackStartTime;
            Vector3 slideVelocity = Vector3.Lerp(transform.forward * _attackSlideSpeed, Vector3.zero, timePassed / _attackSlideDuration);

            _character.SetSlideVelocity(slideVelocity);
            _character.Move();

            AttackCombo();
        }
    }

    private void AttackCombo()
    {
        Player player = GetComponent<Player>();

        if (Input.GetMouseButtonDown(0) && player.IsGrounded())
        {
            string currentClipName = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            _attackAnimationDuration = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            string lastAttackName = "LittleAdventurerAndie_ATTACK_03";

            if ((currentClipName != lastAttackName) && (_attackAnimationDuration > 0.5f) && (_attackAnimationDuration <= 0.7f))
            {
                _character.SetState(IState.TYPE.Attack);
                _character.ConfigureMovement();
            }
        }
    }

    private void ConfigurePlayerLookDirection()
    {
        Player player = GetComponent<Player>();

        if (player != null)
        {
            player.LookAtCursor();
        }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public IState.TYPE Type
    {
        get { return _stateType; }
    }

    public virtual void Execute()
    {
        enabled = true;

        _animator.SetTrigger("Attack");
        _attackStartTime = Time.time;

        ConfigurePlayerLookDirection();
    }

    public virtual void Exit()
    {
        if (_damageCaster != null)
        {
            _damageCaster.DisableDamageCaster();
        }

        if(_character.Type == Character.TYPE.Player)
        {
            GetComponent<PlayerVFXManager>().StopBlades();
        }

        enabled = false;
    }

    #endregion
}
