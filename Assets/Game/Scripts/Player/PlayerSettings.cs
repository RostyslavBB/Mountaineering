using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerSettings",
    menuName = "Game/Player Settings"
)]

public class PlayerSettings : ScriptableObject
{
    public float MoveSpeed;
    public float SprintSpeed;
    public float JumpForse;
}
