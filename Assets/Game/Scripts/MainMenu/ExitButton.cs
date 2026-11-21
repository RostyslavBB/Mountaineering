using UnityEngine;

public class ExitButton : GeneralButtonScript
{
    public void WhenPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stops Play mode in the editor
#else
    Application.Quit(); // closes the built game
#endif
    }
}
