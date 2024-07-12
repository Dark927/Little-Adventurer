using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private Transform _shootPosition;
    [SerializeField] private GameObject _damageOrbPrefab;

    private Enemy _enemy;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        _enemy.RotateToTarget();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void Shoot()
    {
        Instantiate(_damageOrbPrefab, _shootPosition.position, Quaternion.LookRotation(_shootPosition.forward));
    }

    #endregion

}
