using UnityEngine;

public class DamageOrb : PickUp
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

    [Space]
    [Header("VFX Settings")]
    [Space]

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
        Fly();
    }

    private void Fly()
    {
        if (_speed > 0f)
        {
            Vector3 moveVector = transform.position + transform.forward * _speed * Time.deltaTime;
            _rb.MovePosition(moveVector);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        // if collides with another object like wall or enemy (not pickups), explode orb

        bool isAnotherPickup = other.GetComponent<PickUp>();

        if (!isAnotherPickup)
        {
            Explode();
        }
    }

    protected override void OnPickUp(Character character)
    {
        if (character.Type == Character.TYPE.Player)
        {
            character.TakeDamage(_damage, transform.position);
            Explode();
        }
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
