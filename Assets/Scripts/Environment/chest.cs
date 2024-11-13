using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chest : MonoBehaviour
{   
    [Header("Inscribed")]
    public elementTypes[] elementChances;           // chances for elements, currently 4/10 fire 4/10 grass 2/10 water
    public elementTypes[] chosenElements;    // chosen elements
    public GameObject popupCanvas;

    [SerializeField]
    private AudioClip chestOpen;
    [SerializeField]
    private AudioClip chestClose;

    [Header("Dynamic")]
    private AudioSource source;
    public ElementDefinition def1;
    public ElementDefinition def2;

    static public chest c;        
              
    public void OnTriggerEnter(Collider collid)
    {
        c = this;
        GameObject go;
        go = collid.gameObject;
        if(go.GetComponent<PlayerController>() != null) 
        {
            ChestCanvasController canvas = Instantiate(popupCanvas).GetComponent<ChestCanvasController>();

            canvas.button2_text.text = def2.name;
            canvas.but2.GetComponent<Image>().sprite = def2.sprite;
            canvas.button1_text.text = def1.name;
            canvas.but1.GetComponent<Image>().sprite = def1.sprite;

            source.PlayOneShot(chestOpen);
            Time.timeScale = 0;
        }
    }
    public void OnTriggerExit(Collider c)
    {
        GameObject go = c.gameObject;
        if (go.GetComponent<PlayerController>() != null) {
            source.PlayOneShot(chestClose);
            //PopupController.ClosePopup();
        }
    }
    void Awake() {
        chosenElements[0] = elementChances[Random.Range(0, elementChances.Length)];         // 1st chosen 
        def1 = Main.GET_ELEMENT_DEFINITION(chosenElements[0]);

        elementTypes elementChose = elementChances[Random.Range(0, elementChances.Length)]; 
        // 2nd, make sure its not the same
        while (elementChose == chosenElements[0]) {
            elementChose = elementChances[Random.Range(0, elementChances.Length)];
        }
        chosenElements[1] = elementChose;     // 2nd chosen

        def2 = Main.GET_ELEMENT_DEFINITION(chosenElements[1]);

        source = GetComponent<AudioSource>(); // chest sound player
    }
}
