using UnityEngine;

public class DamageOrb : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("Orb Settings")]
    [Space]

    [SerializeField] private float _speed = 9f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private ParticleSystem _hitVFX;
    private Rigidbody _rb;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Explode(_lifeTime);
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = transform.position + transform.forward * _speed * Time.deltaTime;
        _rb.MovePosition(moveVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(_damage, transform.position);
        }

        Explode();
    }

    private void Explode(float timeDelay = 0f)
    {
        Destroy(gameObject, timeDelay);
    }

    private void OnDestroy()
    {
        Instantiate(_hitVFX, transform.position, Quaternion.identity);
    }

    #endregion
}
