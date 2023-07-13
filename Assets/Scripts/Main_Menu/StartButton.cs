using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    float _fadeSpeed = 1f;
    [SerializeField]
    float _blinkSpeed = 10f;

    private Button _button;
    private ColorBlock _originalColors;
    private bool _isBlinking;

    private void Start()
    {
        _button = GetComponent<Button>();
        _originalColors = _button.colors;

        _isBlinking = false;
    }

    private void Update()
    {
        if (_isBlinking == true)
        {
            Blink();
        }
        else
        {
            Fade();
        }
    }

    private void Fade()
    {
        ColorBlock colors = _originalColors;

        float alpha = (Mathf.Sin(Time.time * _fadeSpeed) + 1) / 2;
        Color newNormalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, alpha);

        colors.normalColor = newNormalColor;
        _button.colors = colors;

        Color newHighlightedColor = new Color(colors.highlightedColor.r, colors.highlightedColor.g, colors.highlightedColor.b, alpha);

        colors.highlightedColor = newHighlightedColor;
        _button.colors = colors;
    }

    private void Blink()
    {
        ColorBlock colors = _originalColors;

        float alpha = Mathf.Round(Mathf.Sin(Time.time * _blinkSpeed));
        Color newSelectedColor = new Color(colors.selectedColor.r, colors.selectedColor.g, colors.selectedColor.b, alpha);

        colors.selectedColor = newSelectedColor;
        _button.colors = colors;
    }

    public void BlinkStart()
    {
        _isBlinking = true;
    }

}
