using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeItemBehaviour : MonoBehaviour
{
    public GameObject UIWardrobe;
    public RawImage ItemIcon;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ButtonText;
    public int ItemIndex;
    public ItemData.EItemID ItemID;

    string _itemName;
    bool _isEquipped;

    public void SetProprieties(GameObject UIWardrobe, ItemData.EItemID ItemID, int ItemIndex, string ItemName, Texture ItemIcon)
    {
        this.UIWardrobe = UIWardrobe;
        this.ItemID = ItemID;
        this.ItemIndex = ItemIndex;
        this._itemName = ItemName;
        this.ItemIcon.texture = ItemIcon;
        this.ItemName.SetText(ItemName);


    }

    public void PreviewItem()
    {
        if (UIWardrobe != null)
        {
            UIWardrobe.GetComponent<WardrobeUIBehaviour>().PreviewItem(gameObject);
        }

    }

    public void EquipItem()
    {
        if (UIWardrobe != null)
        {
            if(!_isEquipped)
            UIWardrobe.GetComponent<WardrobeUIBehaviour>().EquipItem(gameObject);
            else
            UIWardrobe.GetComponent<WardrobeUIBehaviour>().UnequipItem(gameObject);
        }


    }

    public void SetEquipped(bool Equipped)
    {
        if (Equipped) {
            ButtonText.SetText("Unequip");
            _isEquipped = true;
        }
        else   {
            ButtonText.SetText("Equip");
            _isEquipped = false;
        }
    }
}
