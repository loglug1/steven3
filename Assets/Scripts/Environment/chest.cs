using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class chest : MonoBehaviour
{   
    [Header("Inscribed")]
    public ItemPool itemPool;
    public GameObject popupCanvas;

    [SerializeField]
    private AudioClip chestOpen;
    [SerializeField]
    private AudioClip chestClose;

    private AudioSource source;
    [Header("Dynamic")]
    public List<ItemDefinition> items;    // random items chosen on spawn
    public bool open = false;

    static public chest c;        
              
    public void OnTriggerEnter(Collider collid)
    {
        c = this;
        GameObject go;
        go = collid.gameObject;
        if(go.GetComponent<PlayerController>() != null) 
        {
            ShopCanvasController canvas = Instantiate(popupCanvas).GetComponent<ShopCanvasController>();

            canvas.items[0].item = items[0];
            canvas.items[1].item = items[1];

            source.PlayOneShot(chestOpen);
            open = true;
            Time.timeScale = 0;
        }
    }
    public void FixedUpdate() {
        if (open) {
            source.PlayOneShot(chestClose);
            open = false;
        }
    }
    void Awake() {
        items.Add(Main.GET_RANDOM_ITEM(itemPool));// 1st chosen 

        //check if pool contains more than one type of item
        List<ItemType> poolTypes = Main.GET_ITEM_POOL(itemPool).Select(i => i.type).Distinct().ToList();
        bool containsMultipleTypes = poolTypes.Count > 1;
        

        ItemDefinition tempItem = Main.GET_RANDOM_ITEM(itemPool);
        // 2nd, make sure its not the same
        if (containsMultipleTypes) {
            while (items.Any(i => i.type == tempItem.type)) {
                tempItem = Main.GET_RANDOM_ITEM(itemPool);
            }
        }
        items.Add(tempItem); // 2nd chosen

        source = GetComponent<AudioSource>(); // chest sound player
    }
}
