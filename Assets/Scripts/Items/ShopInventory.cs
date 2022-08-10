using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    //shop item specifications
    public List<ShopItemData> ShopItems;


    public ItemDatabase ItemDB;

    //actual list of items in inventory
    List<ItemData> _itemList;

    void Start()
    {
        _itemList = new List<ItemData>();
        ConstructItemList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //construct item list acoording to individual shop specifications
    void ConstructItemList()  {
        _itemList.Clear();

        foreach (ShopItemData shopitem in ShopItems)    {
            ItemData item = new ItemData(shopitem.ItemID, shopitem.Discount, shopitem.Name, shopitem.ShopSprite, ItemDB);
            _itemList.Add(item);
        }
    }

    public List<ItemData> GetItemList()
    {

        return _itemList;
    }

}


[System.Serializable]
public class ShopItemData
{
    public ItemData.EItemID ItemID;
    public string Name;
    public int Discount;
    public Sprite ShopSprite;



}
