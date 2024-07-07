using UnityEngine;

public enum StateType
{
    State_normal = 0,
    State_attack = 1,
}


[RequireComponent(typeof(NormalState), typeof(AttackState))]
public abstract class Character : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    public float Gravity = -9.8f;
    [SerializeField] protected float _movementSpeed;
    protected Vector3 _movementVelocity;

    protected CharacterController _characterController;
    protected Animator _animator;

    protected IState _currentState;
    protected StateType _currentStateType;


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
    }

    private void Start()
    {
        SetState(StateType.State_normal);
    }

    protected virtual void FixedUpdate()
    {

    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

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
        }

         _currentState.Execute();
    }


    public void AttackAnimationEnd()
    {
        SetState(StateType.State_normal);
    }

    public void SetSlideVelocity(Vector3 slideVelocity)
    {
        _movementVelocity = slideVelocity;
    }

    #endregion
}
