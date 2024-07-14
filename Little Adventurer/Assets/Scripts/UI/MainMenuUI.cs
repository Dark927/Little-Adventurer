using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuUI : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [SerializeField] private string _gamePlaySceneName;

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void StartGame()
    {
        SceneManager.LoadScene(_gamePlaySceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    #endregion
}
