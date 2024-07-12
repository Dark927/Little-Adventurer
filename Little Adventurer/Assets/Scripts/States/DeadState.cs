using UnityEngine;
using System.Collections;

public class DeadState : MonoBehaviour, IState
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    Character _character;
    Animator _animator;
    SkinnedMeshRenderer _skinnedMeshRenderer;
    MaterialPropertyBlock _materialPropertyBlock;

    private StateType _stateType = StateType.State_dead;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();

        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _skinnedMeshRenderer.GetPropertyBlock(_materialPropertyBlock);

    }

    private IEnumerator DissolveMaterial()
    {
        yield return new WaitForSeconds(2f);


        // Disable collider

        GetComponent<Collider>().enabled = false;


        // Dissolve Material parameters

        float dissolveDurationTime = 2f;
        float dissolvePassedTime = 0f;

        float dissolveHeightStart = 20f;
        float dissolveHeightEnd = -10f;
        float dissolveHeight = 0f;

        _materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);


        // Linearly change dissolve height value

        while(dissolvePassedTime < dissolveDurationTime)
        {
            dissolvePassedTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeightStart, dissolveHeightEnd, dissolvePassedTime / dissolveDurationTime);

            _materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

            yield return null;
        }


        // Set target value 

        _materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeightEnd);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);


        // Drop item

        _character.DropItem();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public StateType CurrentStateType
    {
        get { return _stateType; }
    }

    public void Execute()
    {
        enabled = true;
        _animator.SetTrigger("Dead");
        StartCoroutine(DissolveMaterial());
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}
