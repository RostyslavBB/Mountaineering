using UnityEngine;

public class SettingsButtonScript : GeneralButtonScript
{
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private SettingsPanelAnimationScript settingsPanel;

    protected override void Update()
    {
        base.Update();
        if(Input.GetMouseButtonDown(0) && _mouseOnButton)
        {
            WhenPressed();
        }

    }
    private void WhenPressed()
    {
        settingsCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        settingsPanel.Open();
    }
}
