using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXManager : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private VisualEffect _burstFootStep;
    [SerializeField] private VisualEffect _attackSmashVFX;

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void PlayAttackVFX()
    {
        _attackSmashVFX.Play();
    }

    public void BurstFootStep()
    {
        _burstFootStep.Play();
    }

    #endregion
}
