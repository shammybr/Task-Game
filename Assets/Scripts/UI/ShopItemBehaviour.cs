using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemBehaviour : MonoBehaviour
{

    public GameObject UIShop;
    public RawImage ShopSprite;
    public TextMeshProUGUI PriceText;
    public int ItemIndex;
    public int ItemPrice;
    public ItemData.EItemID ItemID;

    public void SetProprieties(GameObject UIShop, ItemData.EItemID ItemID, int ItemIndex, int ItemPrice, Texture ShopSprite)
    {
        this.UIShop = UIShop;
        this.ItemID = ItemID;
        this.ItemIndex = ItemIndex;
        this.ItemPrice = ItemPrice;
        this.ShopSprite.texture = ShopSprite;
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
        if (UIShop != null)
        {
            UIShop.GetComponent<ShopUIBehaviour>().PreviewItem(gameObject);
        }

    }


}
