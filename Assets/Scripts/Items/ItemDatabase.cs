using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    public List<GameObject> ItemDB;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetItemPrefab(ItemData.EItemID ItemID)   {
        GameObject item = null; 
        switch(ItemID)  {
            case ItemData.EItemID.TopHat:
               item = ItemDB[0];
                Debug.Log(ItemDB[0].name);
                break;

        }

        return item;
    }
}
