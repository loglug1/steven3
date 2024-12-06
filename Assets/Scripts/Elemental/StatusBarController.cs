using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    [Header("Inscribed")]
    public float space;
    public GameObject inidcatorPrefab;
    private List<StatusIndicatorController> indicators = new List<StatusIndicatorController>();
    public void AddIndicator(elementTypes elementType) {
        if (indicators.Find(i => i.element == elementType) != null) return;
        StatusIndicatorController indicator = Instantiate(inidcatorPrefab, transform).GetComponent<StatusIndicatorController>();
        indicator.element = elementType;
        indicators.Add(indicator);
        CenterIndicators();
    }
    public void RemoveIndicator(elementTypes elementType) {
        Debug.Log("Remove: " + elementType);
        StatusIndicatorController indicator = indicators.Find(i => i.element == elementType);
        if (indicator == null) return;
        indicators.Remove(indicator);
        Destroy(indicator.gameObject);
        CenterIndicators();
    }
    private void CenterIndicators() {
        float baseX = -space * (indicators.Count + 1) / 2f;
        for(int i = 0; i < indicators.Count; i++) {
            Vector3 pos = indicators[i].transform.localPosition;
            pos.x = baseX + ((i+1) * space);
            indicators[i].transform.localPosition = pos;
        }
    }
}
