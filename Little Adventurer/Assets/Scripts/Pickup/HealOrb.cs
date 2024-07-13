
public class HealOrb : PickUp
{
    protected override void OnPickUp(Character character)
    {
        if (character.Type != Character.TYPE.Player)
            return;


        Health health = character.GetComponent<Health>();
        health.TakeHeal(_value);

        // Heal visual effect 

        PlayerVFXManager playerVFX = character.GetComponent<PlayerVFXManager>();
        
        if(playerVFX != null)
        {
            playerVFX.PlayHeal();
        }

        Destroy(gameObject);
    }
}
