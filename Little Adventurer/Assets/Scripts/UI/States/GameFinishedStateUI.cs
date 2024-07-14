using UnityEngine;

public class GameFinishedStateUI : MonoBehaviour, IStateUI
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("UI Settings")]
    [Space]

    [SerializeField] private GameObject _gameFinishedPanel;

    private IStateUI.TYPE _stateType = IStateUI.TYPE.GameFinished;

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
        _gameFinishedPanel.SetActive(true);
    }

    public void Exit()
    {
        enabled = false;
        _gameFinishedPanel.SetActive(false);
    }

    #endregion
}
