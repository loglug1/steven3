using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BackBTN : MonoBehaviour
{
    public Button startButton;

	void Start () {
		Button btn = startButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick(){
        SceneManager.LoadScene("StartScene");
		Debug.Log ("start screen button clicked");
	}
}
