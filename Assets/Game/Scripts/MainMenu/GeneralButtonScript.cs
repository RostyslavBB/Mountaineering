using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GeneralButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _hoverScale = 1.15f;
    [SerializeField] private float _animationSpeed = 10f;

    private Vector3 _originalScale;
    private Vector3 _targetScale;

    protected bool _mouseOnButton = false;

    protected virtual void Awake()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale;
    }

    protected virtual void Update()
    {
        AnimateHover();
    }
    protected void AnimateHover()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            _targetScale,
            Time.deltaTime * _animationSpeed
        );
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _targetScale = _originalScale * _hoverScale;
        _mouseOnButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _targetScale = _originalScale;
        _mouseOnButton = false;
    }
}
