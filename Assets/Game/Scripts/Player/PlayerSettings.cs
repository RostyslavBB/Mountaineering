using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerSettings",
    menuName = "Game/Player Settings"
)]

public class PlayerSettings : ScriptableObject
{
    [field:SerializeField]
    public float MoveSpeed { get; private set; }
    [field:SerializeField]
    public float SprintSpeed { get; private set; }
    [field:SerializeField]
    public float JumpForce { get; private set; }
    [field: SerializeField]
    public float ClimbSpeed { get; private set; }
    [field: SerializeField]
    public float GroundCheckRadius { get; private set; }
}
