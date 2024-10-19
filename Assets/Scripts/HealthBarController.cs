using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    public float value {
        get { return _value; }
        set { SetValue(value); }
    }

    private float _value;
    public float maxValue;

    private GameObject valueBar;


    void Awake() {
        valueBar = transform.GetChild(0).gameObject;
        value = maxValue * 0.5f;
    }
    void SetValue(float v) {
        _value = v;
        Vector3 scale = valueBar.transform.localScale;
        scale.x = _value * 0.9f / maxValue;
        valueBar.transform.localScale = scale;
    }
}
