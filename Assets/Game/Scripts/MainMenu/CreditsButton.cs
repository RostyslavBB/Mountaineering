using UnityEngine;

public class CreditsButton : GeneralButtonScript
{
    [SerializeField] private Canvas _creditsCanvas;
    [SerializeField] private Canvas _mainMenuCanvas;
    public void WhenPressed()
    {
        _creditsCanvas.gameObject.SetActive(true);
        _mainMenuCanvas.gameObject.SetActive(false);
    }
}
