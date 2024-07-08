using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] protected int _value;

    #endregion


    // ----------------------------------------------------------------------------------
    // Protected Methods
    // ----------------------------------------------------------------------------------

    #region Protected Methods

    protected virtual void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            OnPickUp(player);
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickUp(Character character);

    #endregion
}
