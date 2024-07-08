
public class Coin_PickUp : PickUp
{
    protected override void OnPickUp(Character character)
    {
        Player player = character.GetComponent<Player>();

        if(player != null)
        {
            player.TakeCoins(_value);
        }
    }
}
