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
    private int _currentHp;

    private Character _character;

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

        if(_currentHp > _maxHp)
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
            bool mustStunned;

            // Enemies can be stunned by 25% chance 

            if (_character.Type == Character.TYPE.Enemy)
            {
                mustStunned = (Random.Range(0, 4) == 0);
            }
            // Player can be stunned by 80% chance 
            else
            {
                mustStunned = (Random.Range(0, 5) != 0);
            }

            if (mustStunned)
            {
                _character.SetState(IState.TYPE.Hurted);
                _character.AddImpact(attackerPosition, attackForce);
            }
        }
    }

    #endregion
}
