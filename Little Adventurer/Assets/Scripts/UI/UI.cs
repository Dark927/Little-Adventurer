using UnityEngine;

public class UI : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    public static UI instance;

    [SerializeField] private string _mainMenuSceneName;
    private IStateUI _currentState;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        SetStateUI(IStateUI.TYPE.Gameplay);
    }

    private void CleanCache()
    {
        _currentState.Exit();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void SetStateUI(IStateUI.TYPE stateType)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }


        switch (stateType)
        {
            case IStateUI.TYPE.Gameplay:
                {
                    _currentState = GetComponent<GameplayStateUI>();
                    break;
                }

            case IStateUI.TYPE.Pause:
                {
                    _currentState = GetComponent<PauseStateUI>();
                    break;
                }            
            
            case IStateUI.TYPE.GameOver:
                {
                    _currentState = GetComponent<GameOverStateUI>();
                    break;
                }

            case IStateUI.TYPE.GameFinished:
                {
                    _currentState = GetComponent<GameFinishedStateUI>();
                    break;
                }

        }

        _currentState.Execute();
    }


    public void OpenMainMenu()
    {
        CleanCache();
        GameManager.instance.LoadGameScene(_mainMenuSceneName);
    }

    public void RestartGameScene()
    {
        CleanCache();
        GameManager.instance.RestartGameScene();
    }

    #endregion
}
