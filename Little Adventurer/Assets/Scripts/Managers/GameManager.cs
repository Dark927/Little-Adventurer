using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private Player _player;
    private bool _gameOver;

    #endregion


    // ----------------------------------------------------------------------------------
    // Private Methods
    // ----------------------------------------------------------------------------------

    #region Private Methods

    private void Start()
    {
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
        Debug.Log("Game Over");
    }

    public void GameFinished()
    {
        Debug.Log("Game Finished");
    }

    #endregion
}
