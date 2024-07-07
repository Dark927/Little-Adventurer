using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private VisualEffect _footStep;
    [SerializeField] private ParticleSystem _blade01;

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void UpdateFootStep(bool isRunning)
    {
        if(isRunning)
        {
            _footStep.Play();
        }
        else
        {
            _footStep.Stop();
        }
    }

    public void PlayBlade01()
    {
        _blade01.Play();
    }

    #endregion
}
