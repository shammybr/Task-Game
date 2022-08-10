using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItemBehaviour : MonoBehaviour
{

    public GameObject UIShop;
    public TextMeshProUGUI PriceText;
    public int ItemIndex;
    public int ItemPrice;
    public ItemData.EItemID ItemID;

    public void SetProprieties(GameObject UIShop, ItemData.EItemID ItemID, int ItemIndex, int ItemPrice)
    {
        this.UIShop = UIShop;
        this.ItemID = ItemID;
        this.ItemIndex = ItemIndex;
        this.ItemPrice = ItemPrice;
        PriceText.SetText("$" + ItemPrice);
    }

    public void BuyItem()
    {
        if(UIShop != null)
        {
            UIShop.GetComponent<ShopUIBehaviour>().BuyItem(gameObject);
        }


    }

    public void SellItem()
    {

        if (UIShop != null)
        {
            UIShop.GetComponent<ShopUIBehaviour>().SellItem(gameObject);
        }

    }

    public void PreviewItem()
    {


    }


}
