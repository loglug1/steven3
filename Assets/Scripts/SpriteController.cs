using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class SpriteController : MonoBehaviour
{
    [Header("Inscribed")]
    public List<GameObject> coloredSprites;
    public GameObject spriteAnchor;

    [Header("Dynamic")]
    public bool flipped = false;
    public List<Color> tintColors;
    private Color _color;
    public Color color {
        set { _color = value; SetColor(value); }
        get { return _color; }
    }

    //Only changes visible color, does not change the internal color variable
    void SetColor(Color color) {
        foreach(GameObject go in coloredSprites) {
            go.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void Flip() {
        Vector3 tempScale = spriteAnchor.transform.localScale;
        tempScale.x *= -1;
        spriteAnchor.transform.localScale = tempScale;
        flipped = !flipped;
    }

    public void Tint(Color tintColor, float time) {
        StartCoroutine(_Tint(tintColor, time));
    }

    IEnumerator _Tint(Color newTint, float time) {
        tintColors.Add(newTint);
        Color c = tintColors[0];
        foreach(Color tint in tintColors.Distinct()) {
            c = Color.Lerp(tint, c, (float)(1f/tintColors.Count));
        }
        SetColor(Color.Lerp(_color, c, 0.6f));
        yield return new WaitForSeconds(time);
        c = tintColors[0];
        tintColors.Remove(newTint);
        foreach(Color tint in tintColors.Distinct()) {
            c = Color.Lerp(tint, c, (float)(1f/tintColors.Count));
        }
        SetColor(Color.Lerp(_color, c, 0.6f));
        if (tintColors.Count == 0)
            SetColor(_color);
    }
}
