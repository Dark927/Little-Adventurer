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
        float timePassed = Time.time - _attackStartTime;
        Vector3 slideVelocity = Vector3.Lerp(transform.forward * _attackSlideSpeed, Vector3.zero, timePassed / _attackSlideDuration);

        _character.SetSlideVelocity(slideVelocity);
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public virtual void Execute()
    {
        enabled = true;
        _animator.SetTrigger("Attack");
        _attackStartTime = Time.time;
    }

    public virtual void Exit()
    {
        enabled = false;
    }

    #endregion
}
