using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWardrobe : MonoBehaviour
{
    public List<ItemData.EItemID> ItemList;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemData.EItemID Item)
    {
        ItemList.Add(Item);


    }

    public void RemoveItem(int ItemIndex)  {

        ItemList.RemoveAt(ItemIndex);
    }

    public void EquipItem(int ItemIndex) {



    }
}
