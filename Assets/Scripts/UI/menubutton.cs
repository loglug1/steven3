using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menubutton : MonoBehaviour
{
public Button menuButton;

	void Start () {
		Button btn = menuButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick(){
        SceneManager.LoadScene("StartScene");
		Debug.Log ("menu button clicked");
	}
}
