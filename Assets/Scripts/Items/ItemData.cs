using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{

    // Start is called before the first frame update
    public EItemID ItemID;
    public Sprite ShopSprite;
    public string ItemName;
    public int ItemValue;
    public List<AnimationClip> AnimationClips;

    bool _equipped;

    public ItemData(EItemID ItemID, int Discount, string Name, Sprite ShopSprite, ItemDatabase ItemDB)
    {
        this.ItemID = ItemID;

        if (ItemName == null)
            this.ItemName = ItemDB.ListItemData[(int)ItemID].ItemName;
        else
            this.ItemName = Name;

        if (ShopSprite == null)
            this.ShopSprite = ItemDB.ListItemData[(int)ItemID].ShopSprite;
        else
            this.ShopSprite = ShopSprite;

      
        this.AnimationClips = ItemDB.ListItemData[(int)ItemID].AnimationClips;
        this.ItemValue = ItemDB.ListItemData[(int)ItemID].ItemValue - Discount;
     
    }

    public enum EItemID  {
        NoHat = 0,
        TopHat = 1
            
    }

    public bool IsEquipped()
    {
        return _equipped;
    }

    public void SetEquipped(bool Equipped)
    {
        _equipped = Equipped;
    }
}
