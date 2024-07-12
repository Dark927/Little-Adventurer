using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private VisualEffect _footStep;
    [SerializeField] private VisualEffect _heal;
    [SerializeField] private VisualEffect _attackSlash;
    [SerializeField] private ParticleSystem _blade01;
    [SerializeField] private ParticleSystem _blade02;
    [SerializeField] private ParticleSystem _blade03;

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

    public void PlayBlade02()
    {
        _blade02.Play();
    }

    public void PlayBlade03()
    {
        _blade03.Play();
    }

    public void StopBlades()
    {
        StopSParticles(_blade01);
        StopSParticles(_blade02);
        StopSParticles(_blade03);
    }

    public void StopSParticles(ParticleSystem particles)
    {
        particles.Simulate(0);
        particles.Stop();
    }

    public void PlayAttackSlash(Vector3 position)
    {
        _attackSlash.transform.position = position;
        _attackSlash.Play();
    }

    public void PlayHeal()
    {
        _heal.Play();
    }

    #endregion
}
