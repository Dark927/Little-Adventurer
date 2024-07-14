using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayStateUI : MonoBehaviour, IStateUI
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    [Space]
    [Header("UI Settings")]
    [Space]

    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _coinsText;

    private Health _playerHealth;
    private PlayerCoins _playerCoins;


    [Space]
    [Header("Key Settings")]
    [Space]

    [SerializeField] private KeyCode _enablePauseKey = KeyCode.Escape;

    private IStateUI.TYPE _stateType = IStateUI.TYPE.Gameplay;

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

    private void Start()
    {
        _playerHealth = GameManager.instance.PlayerCharacter.GetComponent<Health>();
        _playerCoins = GameManager.instance.PlayerCharacter.GetComponent<PlayerCoins>();
    }

    private void Update()
    {
        UpdateHealthAndCoinsUI();
        TrySetPauseState();
    }

    private void TrySetPauseState()
    {
        if (Input.GetKeyDown(_enablePauseKey))
        {
            UI.instance.SetStateUI(IStateUI.TYPE.Pause);
        }
    }

    private void UpdateHealthAndCoinsUI()
    {
        _healthSlider.value = _playerHealth.CurrentHealthPercentage;
        _coinsText.text = _playerCoins.CollectedAmount.ToString();
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void Execute()
    {
        enabled = true;
    }

    public void Exit()
    {
        enabled = false;
    }

    #endregion
}
