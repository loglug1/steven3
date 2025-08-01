using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ColorPickerScript : MonoBehaviour
{
    [Header("Inscribed")]
    public TMP_InputField Rinput;
    public TMP_InputField Ginput;
    public TMP_InputField Binput;
    public Button submitBTN;
    public Image thisSlime;

    [Header("Dynamic")]
    public float R;
    public float G;
    public float B;

    void Start () {
		Button btn = submitBTN.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick(){
        Debug.Log("submitclicked");Debug.Log(Rinput.text);
        R=System.Int32.Parse(Rinput.text);
        G=System.Int32.Parse(Ginput.text);
        B=System.Int32.Parse(Binput.text);

        PlayerPrefs.SetFloat("PlayerColorR", R / 255f);
        PlayerPrefs.SetFloat("PlayerColorG", G / 255f);
        PlayerPrefs.SetFloat("PlayerColorB", B / 255f);
        
        Main.PlayerColor=new Color(PlayerPrefs.GetFloat("PlayerColorR"),
                                   PlayerPrefs.GetFloat("PlayerColorG"),
                                   PlayerPrefs.GetFloat("PlayerColorB"),
                                   1);
        thisSlime.GetComponent<Image>().material.color=Main.PlayerColor;
    }
}
