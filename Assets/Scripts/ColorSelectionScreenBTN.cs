using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorSelectionScreenBTN : MonoBehaviour
{
    public Button colorButton;

	void Start () {
		Button btn = colorButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick(){
        SceneManager.LoadScene("ColorSelectorScene");
		Debug.Log ("color button clicked");
    }
}
