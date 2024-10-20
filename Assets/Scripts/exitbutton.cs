using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class exitbutton : MonoBehaviour
{
public Button exitButton;

	void Start () {
		Button btn = exitButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick(){
        Application.Quit();
		Debug.Log ("exit button clicked");
	}
}
