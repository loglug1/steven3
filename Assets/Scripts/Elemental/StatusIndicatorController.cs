using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StatusIndicatorController : MonoBehaviour
{
    private elementTypes _element;
    public elementTypes element {
        set { SetElement(value); }
        get { return _element; }
    }
    private void SetElement(elementTypes e) {
        _element = e;
        GetComponent<SpriteRenderer>().color = Main.GET_ELEMENT_DEFINITION(e).color;
    }
}
