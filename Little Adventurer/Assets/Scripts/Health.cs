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

    Character _character;

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

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
    }

    #endregion
}
