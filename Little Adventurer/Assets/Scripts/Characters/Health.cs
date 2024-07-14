using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Health : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private int _maxHp;
    [SerializeField] [Range(0, 100)] private int _chanceToBeStunned;
    private int _currentHp;

    private Character _character;

    #endregion


    // ----------------------------------------------------------------------------------
    // Properties
    // ----------------------------------------------------------------------------------

    #region Properties

    public float CurrentHealthPercentage
    {
        get { return (float)_currentHp / _maxHp; }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _currentHp = _maxHp;
        _character = GetComponent<Character>();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void TakeHeal(int heal)
    {
        _currentHp += heal;

        if (_currentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }
    }

    public void TakeDamage(int damage, Vector3 attackerPosition = new Vector3(), float attackForce = 1f)
    {
        _currentHp -= damage;
        CheckHealth(attackerPosition, attackForce);
    }

    public void CheckHealth(Vector3 attackerPosition = new Vector3(), float attackForce = 1f)
    {
        if (_currentHp <= 0)
        {
            _character.SetState(IState.TYPE.Dead);
            _currentHp = 0;
        }
        else
        {
            bool mustStunned = Random.Range(0, 100) <= _chanceToBeStunned;

            if (mustStunned)
            {
                _character.SetState(IState.TYPE.Hurted);
                _character.AddImpact(attackerPosition, attackForce);
            }
        }
    }

    #endregion
}
