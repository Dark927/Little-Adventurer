
public class Heal_PickUp : PickUp
{
    protected override void OnPickUp(Character character)
    {
        Health health = character.GetComponent<Health>();
        health.TakeHeal(_value);

        // Heal visual effect 

        PlayerVFXManager playerVFX = character.GetComponent<PlayerVFXManager>();
        
        if(playerVFX != null)
        {
            playerVFX.PlayHeal();
        }
    }
}
