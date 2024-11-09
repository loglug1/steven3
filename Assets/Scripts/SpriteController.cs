using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [Header("Inscribed")]
    public List<GameObject> coloredSprites;
    public GameObject spriteAnchor;

    [Header("Dynamic")]
    public bool flipped = false;
    private Color _color;
    public Color color {
        set { SetColor(value); }
        get { return _color; }
    }

    void SetColor(Color color) {
        _color = color;
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

    public IEnumerator Blink(Color blinkColor) {
        Color tempColor = color;
        color = Color.Lerp(blinkColor, color, 0.5f);
        yield return new WaitForSeconds(1f);
        color = tempColor;
    }
}
