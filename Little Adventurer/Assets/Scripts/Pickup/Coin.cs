using UnityEngine;

public class Coin : PickUp
{
    [SerializeField] private ParticleSystem _visualEffect;

    protected override void OnPickUp(Character character)
    {
        PlayerCoins playerCoins = character.GetComponent<PlayerCoins>();

        if(playerCoins != null)
        {
            playerCoins.AddCoins(_value);
            Instantiate(_visualEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
