using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private int _damage;
    [SerializeField] private float _force;
    [SerializeField] private string _targetTag;

    private PlayerVFXManager _playerVFX;
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
        _playerVFX = GetComponentInParent<PlayerVFXManager>();
        _damageCasterCollider.enabled = false;
        _damagedTargetsList = new();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_damagedTargetsList.Contains(other))
        {
            Character target = other.GetComponent<Character>();

            if ((target != null) && (target.CompareTag(_targetTag)))
            {
                if (!target.IsDead)
                {
                    target.TakeDamage(_damage, transform.parent.position, _force);

                    PlaySlashVFX();

                    _damagedTargetsList.Add(other);
                }
            }
        }
    }

    private void PlaySlashVFX()
    {
        if (_playerVFX == null)
            return;

        RaycastHit hit;

        Vector3 originalPosition = transform.position + (-_damageCasterCollider.bounds.extents.z) * transform.forward;

        bool isHit = Physics.BoxCast(
            originalPosition,
            _damageCasterCollider.bounds.extents / 2,
            transform.forward,
            out hit,
            transform.rotation,
            _damageCasterCollider.bounds.extents.z,
            1 << 6
            );

        if (isHit)
        {
            _playerVFX.PlayAttackSlash(hit.point + new Vector3(0, 0.5f, 0));
        }
    }

    private void OnDrawGizmos()
    {
        if (_damageCasterCollider == null)
        {
            _damageCasterCollider = GetComponent<Collider>();
        }

        RaycastHit hit;

        Vector3 originalPosition = transform.position + (-_damageCasterCollider.bounds.extents.z) * transform.forward;

        bool isHit = Physics.BoxCast(
            originalPosition,
            _damageCasterCollider.bounds.extents / 2,
            transform.forward,
            out hit,
            transform.rotation,
            _damageCasterCollider.bounds.extents.z,
            1 << 6
            );

        if (isHit)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hit.point, 0.3f);
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
