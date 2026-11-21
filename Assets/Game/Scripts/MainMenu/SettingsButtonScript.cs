using UnityEngine;
public class SettingsButtonScript : GeneralButtonScript
{
    [SerializeField] private Canvas _settingsCanvas;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private SettingsPanelAnimationScript settingsPanel;


    public void WhenPressed()
    {
        _settingsCanvas.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
        settingsPanel.Open();
        Debug.Log($"shit should be opened");
    }
}
