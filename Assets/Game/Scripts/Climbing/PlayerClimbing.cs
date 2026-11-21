using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    private Climbable _currentClimbable;

    public bool CanClimb => _currentClimbable != null;

    public Climbable CurrentClimbable => _currentClimbable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Climbable climbable))
        {
            _currentClimbable = climbable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Climbable climbable) &&
            climbable == _currentClimbable)
        {
            _currentClimbable = null;
        }
    }
}
