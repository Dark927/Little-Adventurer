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

    public void TakeDamage(int damage, Vector3 attackerPosition = new Vector3(), float attackForce = 1f)
    {
        _currentHp -= damage;
        CheckHealth(attackerPosition, attackForce);
    }

    public void CheckHealth(Vector3 attackerPosition = new Vector3(), float attackForce = 1f)
    {
        if (_currentHp <= 0)
        {
            _character.SetState(StateType.State_dead);
            _currentHp = 0;
        }
        else
        {
            bool mustStunned;

            // Enemies can be stunned by 25% chance 

            if (_character.GetCharacterType() == CharacterType.Character_enemy)
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
                _character.SetState(StateType.State_hurted);
                _character.AddImpact(attackerPosition, attackForce);
            }
        }
    }

    #endregion
}
