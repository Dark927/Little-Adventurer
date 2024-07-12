using UnityEngine;

public class HurtedState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private Character _character;
    private Animator _animator;

    private float _impactDurationTime = 5f;
    private StateType _stateType = StateType.State_hurted;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_character.ActualImpactOn.magnitude > 0.2f)
        {
            _character.SetMovementVelocity(_character.ActualImpactOn * Time.deltaTime);
        }

        _character.ActualImpactOn = Vector3.Lerp(_character.ActualImpactOn, Vector3.zero,  _impactDurationTime * Time.deltaTime);
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public StateType CurrentStateType
    {
        get { return _stateType; }
    }

    public void Execute()
    {
        enabled = true;
        _animator.SetTrigger("Hurted");
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}
