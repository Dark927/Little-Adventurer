using UnityEngine;

public class GameOverStateUI : MonoBehaviour, IStateUI
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("UI Settings")]
    [Space]

    [SerializeField] private GameObject _gameOverPanel;

    private IStateUI.TYPE _stateType = IStateUI.TYPE.GameOver;

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
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void Execute()
    {
        enabled = true;
        _gameOverPanel.SetActive(true);
    }

    public void Exit()
    {
        enabled = false;
        _gameOverPanel.SetActive(false);
    }

    #endregion
}