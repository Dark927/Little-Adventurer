using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCoins : MonoBehaviour
{
    // ----------------------------------------------------------------------------------
    // Fields
    // ----------------------------------------------------------------------------------

    #region Fields

    private int _coinsAmount = 0;

    #endregion


    // ----------------------------------------------------------------------------------
    // Properties
    // ----------------------------------------------------------------------------------

    #region Properties

    public int CollectedAmount
    {
        get { return _coinsAmount; }
    }

    #endregion


    // ----------------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------------

    #region Public Methods

    public void AddCoins(int coins)
    {
        _coinsAmount += coins;
    }

    #endregion
}
