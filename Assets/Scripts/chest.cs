using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chest : MonoBehaviour
{
    [SerializeField]
    private AudioClip chestOpen;
    [SerializeField]
    private AudioClip chestClose;

    public elementTypes[] elementChances;           // chances for elements, currently 4/10 fire 4/10 grass 2/10 water
    public elementTypes[] chosenElements;    // chosen elements
    private AudioSource source;
    public GameObject  uiPopup;   
    public TMP_Text    button1;
    public TMP_Text    button2;

    public ElementDefinition def1;
    public ElementDefinition def2;

    static public chest c;        
              
    public void OnTriggerEnter(Collider c)
    {
        GameObject go;
        go = c.gameObject;
        if(go.GetComponent<PlayerController>() != null) 
        {
            source.PlayOneShot(chestOpen);
            uiPopup.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider c)
    {
        GameObject go = c.gameObject;
        if (go.GetComponent<PlayerController>() != null) {
            source.PlayOneShot(chestClose);
            uiPopup.SetActive(false);
        }
    }
    void Awake() {
        c = this;
        chosenElements[0] = elementChances[Random.Range(0, elementChances.Length - 1)];         // 1st chosen 
        def1 = Main.GET_ELEMENT_DEFINITION(chosenElements[0]);
        button1.text = def1.name;

        elementTypes elementChose = elementChances[Random.Range(0, elementChances.Length - 1)]; 
        // 2nd, make sure its not the same
        while (elementChose == chosenElements[0]) {
            elementChose = elementChances[Random.Range(0, elementChances.Length - 1)];
        }
        chosenElements[1] = elementChose;     // 2nd chosen

        def1 = Main.GET_ELEMENT_DEFINITION(chosenElements[1]);
        button2.text = def2.name;



        source = GetComponent<AudioSource>(); // chest sound player
        uiPopup.SetActive(false);             // pop up
    }
}
