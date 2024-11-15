using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
public Button startButton;

	void Start () {
		Button btn = startButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick(){
        SceneManager.LoadScene("_Procedural Generation Finalization 1");//whatever the start scene is
		Debug.Log ("start button clicked");
	}
}
