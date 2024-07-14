using UnityEngine;

public class PauseStateUI : MonoBehaviour, IStateUI
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("UI Settings")]
    [Space]

    [SerializeField] private GameObject _pausePanel;

    [Space]
    [Header("Key Settings")]
    [Space]

    [SerializeField] private KeyCode _disablePauseKey = KeyCode.Escape;

    private IStateUI.TYPE _stateType = IStateUI.TYPE.Pause;

    #endregion

    // ----------------------------------------------------------------------------------
    // Properties
    // ----------------------------------------------------------------------------------

    #region Properties
    public IStateUI.TYPE Type
    {
        get { return _stateType; }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Update()
    {
        TrySetGameplayState();
    }

    private void TrySetGameplayState()
    {
        if (Input.GetKeyDown(_disablePauseKey))
        {
            SetGameplayState();
        }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void SetGameplayState()
    {
        UI.instance.SetStateUI(IStateUI.TYPE.Gameplay);
    }

    public void Execute()
    {
        enabled = true;
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
       
    }

    public void Exit()
    {
        enabled = false;
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    #endregion
}

