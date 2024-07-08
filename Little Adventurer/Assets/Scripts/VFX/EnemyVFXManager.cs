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
    [SerializeField] private ParticleSystem _beingHitVFX;
    [SerializeField] private VisualEffect _beingHitSplashVFX;

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

    public void BeingHitVFX(Vector3 attackerPos)
    {
        Vector3 forceForward = (transform.position - attackerPos).normalized;
        forceForward.y = 0;

        _beingHitVFX.transform.rotation = Quaternion.LookRotation(forceForward);
        _beingHitVFX.Play();

        BeingHitSplashVFX();
    }


    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void BeingHitSplashVFX()
    {
        Vector3 splashPos = transform.position + new Vector3(0, 2f, 0);
        VisualEffect splashEffect = Instantiate(_beingHitSplashVFX, splashPos, Quaternion.identity);
        splashEffect.Play();
        Destroy(splashEffect.gameObject, 10f);
    }

    #endregion
}
