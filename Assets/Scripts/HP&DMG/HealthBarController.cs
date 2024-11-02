using TMPro;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    public float value {
        get { return _value; }
        set { SetValue(value); }
    }

    public Color color {
        get { return _color; }
        set { SetColor(value); }
    }

    private float _value;
    private Color _color;
    public float maxValue;
    public float darkenAmount = 100f;

    private GameObject valueBar;
    private GameObject darkBar;


    void Awake() {
        valueBar = transform.GetChild(0).gameObject;
        darkBar = transform.GetChild(1).gameObject;
        value = maxValue * 0.5f;
    }
    void SetValue(float v) {
        _value = v;
        Vector3 scale = valueBar.transform.localScale;
        scale.x = _value * 0.9f / maxValue;
        valueBar.transform.localScale = scale;
    }

    void SetColor(Color c) {
        _color = c;
        valueBar.transform.GetChild(0).GetComponent<Renderer>().material.color = c;
        Color backgroundC = new Color(Mathf.Max(c.r - darkenAmount, 0f), Mathf.Max(c.g - darkenAmount, 0f), Mathf.Max(c.b - darkenAmount, 0f));
        darkBar.GetComponent<Renderer>().material.color = backgroundC;
    }
}
