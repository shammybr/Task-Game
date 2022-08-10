using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    public List<ItemData> ListItemData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int GetItemBasePrice(ItemData.EItemID ItemID) {

        return ListItemData[(int)ItemID].ItemValue;
    }

    public string GetItemName(ItemData.EItemID ItemID) {

        return ListItemData[(int)ItemID].ItemName;


    }

    public List<AnimationClip> GetItemAnimation(ItemData.EItemID ItemID)
    {
       
        return ListItemData[(int)ItemID].AnimationClips;
    }
}
