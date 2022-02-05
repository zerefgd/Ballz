using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private List<Color> _colors;
    private int _hitsRemaining;
    private SpriteRenderer _spriteRenderer;
    private TMPro.TMP_Text _text;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _text = GetComponentInChildren<TMPro.TMP_Text>();
    }

    private void UpdateVisualState()
    {
        _text.SetText(_hitsRemaining.ToString());
        int colorIndex = _hitsRemaining / 10;
        float mix = (_hitsRemaining % 10) / 10f;
        _spriteRenderer.color = Color.Lerp(_colors[colorIndex % _colors.Count], _colors[(colorIndex + 1) % _colors.Count],mix);
    }

    public void SetHit(int hits)
    {
        _hitsRemaining = hits;
        UpdateVisualState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _hitsRemaining--;
        if(_hitsRemaining > 0)
        {
            UpdateVisualState();
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
