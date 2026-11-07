public class PlayerModel 
{
    private readonly PlayerSettings settings;

    public float MoveSpeed => settings.MoveSpeed;
    public float SprintSpeed => settings.SprintSpeed;
    public float JumpForce => settings.JumpForse;

    public PlayerModel(PlayerSettings settings)
    {
        this.settings = settings;
    }
}
