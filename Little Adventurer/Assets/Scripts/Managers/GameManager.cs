using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    public static GameManager instance;

    private Player _player;
    private bool _gameOver;

    #endregion


    // ----------------------------------------------------------------------------------
    // Properties
    // ----------------------------------------------------------------------------------

    #region Properties

    public Player PlayerCharacter
    {
        get { return _player; }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_gameOver)
            return;

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (_player.CurrentState.Type == IState.TYPE.Dead)
        {
            _gameOver = true;
            GameOver();
        }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void GameOver()
    {
        UI.instance.SetStateUI(IStateUI.TYPE.GameOver);
    }

    public void GameFinished()
    {
        UI.instance.SetStateUI(IStateUI.TYPE.GameFinished);
    }

    public void RestartGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion
}
