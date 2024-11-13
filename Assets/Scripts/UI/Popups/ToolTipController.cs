using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public void OnPointerEnter(PointerEventData ped) {
        tooltip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData ped) {
        tooltip.SetActive(false);
    }
}
