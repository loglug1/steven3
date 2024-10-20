using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsBTN : MonoBehaviour
{
    public Button creditsButton;

	void Start () {
		Button btn = creditsButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick(){
        SceneManager.LoadScene("CreditsScene");
		Debug.Log ("credits button clicked");
    }
}
