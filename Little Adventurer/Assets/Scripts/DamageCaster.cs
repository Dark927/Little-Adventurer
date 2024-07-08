using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private int _damage;

    private Collider _damageCasterCollider;
    private List<Collider> _damagedTargetsList;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _damageCasterCollider = GetComponent<Collider>();
        _damageCasterCollider.enabled = false;
        _damagedTargetsList = new();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_damagedTargetsList.Contains(other))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
                _damagedTargetsList.Add(other);
            }
        }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void EnableDamageCaster()
    {
        _damagedTargetsList.Clear();
        _damageCasterCollider.enabled = true;
    }

    public void DisableDamageCaster()
    {
        _damagedTargetsList.Clear();
        _damageCasterCollider.enabled = false;
    }

    #endregion
}
