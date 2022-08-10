using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWardrobe : MonoBehaviour
{
    public List<ItemData> ItemList;
    public GameObject Player;


    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()  {

    }


    public ItemData GetItem(int ItemIndex)
    {
        return ItemList[ItemIndex];

    }

    //add item to wardrobe
    public void AddItem(ItemData Item)
    {
        ItemList.Add(Item);


    }

    //remove item from wardrobe
    public void RemoveItem(ItemData Item)  {

        ItemList.Remove(Item);
    }

   public void SetEquipped(int ItemIndex, bool IsEquipped)
    {
        ItemList[ItemIndex].SetEquipped(IsEquipped);
    }
}
