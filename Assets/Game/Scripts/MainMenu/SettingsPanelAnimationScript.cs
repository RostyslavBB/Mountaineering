using UnityEngine;

// Slides the panel in from the right edge of the screen.
// Inspector setup on the panel's Rect Transform:
//   anchors on the right edge (e.g. "right-stretch" preset), Pivot X = 1, Pos X = 0.
// Closed = pushed right by its own width (just off screen), Open = Pos X 0.
// The positions come from the panel's width in canvas units, so they work at any resolution or aspect ratio.
[RequireComponent(typeof(RectTransform))]
public class SettingsPanelAnimationScript : MonoBehaviour
{
    [SerializeField] private float _slideSpeed = 10f;
    [SerializeField] private bool _startOpen = false;

    private RectTransform _panel;
    private bool _isOpen;

    public bool IsOpen => _isOpen;

    private void Awake()
    {
        _panel = (RectTransform)transform;
        _isOpen = _startOpen;
        SetX(TargetX); // start in place, without animating
    }

    private void Update()
    {
        // TargetX is recalculated every frame, so resizing the window while the panel is closed keeps it hidden.
        float x = Mathf.Lerp(_panel.anchoredPosition.x, TargetX, Time.unscaledDeltaTime * _slideSpeed);
        SetX(x);
    }

    public void Open() => _isOpen = true;
    public void Close() => _isOpen = false;
    public void Toggle() => _isOpen = !_isOpen;

    private float TargetX => _isOpen ? 0f : _panel.rect.width;

    private void SetX(float x)
    {
        Vector2 pos = _panel.anchoredPosition;
        pos.x = x;
        _panel.anchoredPosition = pos;
    }
}
