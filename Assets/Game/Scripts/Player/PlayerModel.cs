public class PlayerModel 
{
    private readonly PlayerSettings settings;

    public float MoveSpeed => settings.MoveSpeed;
    public float SprintSpeed => settings.SprintSpeed;
    public float JumpForce => settings.JumpForce;
    public float ClimbSpeed => settings.ClimbSpeed;
    public float GroundCheckRadius => settings.GroundCheckRadius;

    public PlayerModel(PlayerSettings settings)
    {
        this.settings = settings;
    }
}
