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
        Character character = other.GetComponent<Character>();

        if(character != null)
        {
            OnPickUp(character);
        }
    }

    protected abstract void OnPickUp(Character character);

    #endregion
}
