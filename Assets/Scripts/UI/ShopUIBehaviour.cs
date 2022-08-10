using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIBehaviour : MonoBehaviour
{
    //UI shop item prefab
    public GameObject BuyingItemPrefab;
    public GameObject SellingItemPrefab;

    //UI shop wrapper
    public GameObject ShopContents;
    public GameObject Buttons;
    //the shop being open
    public GameObject Shop;

   


    public GameObject Player;
    public PlayerWardrobe PlayerWardrobe;


    public ItemDatabase ItemDB;


    public bool IsOpen;
    Animator _uiAnimator;


    List<GameObject> _listedItems;
    List<ItemData> _shopItems;

    private void Awake()
    {
        _listedItems = new List<GameObject>();
        _uiAnimator = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        IsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowMenu(GameObject Shop, List<ItemData> ShopItems)
    {
        IsOpen = true;

        this.Shop = Shop;
        this._shopItems = ShopItems;
        Buttons.SetActive(true);
        CleanList();

        //put the shop on view
        if (_uiAnimator != null)
            _uiAnimator.Play("UIAnimationIn");
    }


    public void HideMenu()
    {

        IsOpen = false;

        Shop = null;
        _shopItems = null;

        //hide shop
        if (_uiAnimator != null)
            _uiAnimator.Play("UIAnimationOut");
        CleanList();
    }

    //Populates the list of items
    public void InitializeBuyingItemList()
    {

        //reference from the opening shop
        
        Buttons.SetActive(false);

        for (int i = 0; i < _shopItems.Count; i++)
        {

    
           GameObject _createdShopItem = Instantiate(BuyingItemPrefab);

            _createdShopItem.transform.SetParent(ShopContents.transform);
            _createdShopItem.transform.localScale = Vector3.one;
            _createdShopItem.GetComponent<ShopItemBehaviour>().SetProprieties(gameObject, _shopItems[i].ItemID, i, _shopItems[i].ItemValue);
            _listedItems.Add(_createdShopItem);



        }



    }


    public void InitializeSellingItemList()
    {
        Buttons.SetActive(false);

        for (int i = 0; i < PlayerWardrobe.ItemList.Count; i++)
        {

             GameObject _createdShopItem = Instantiate(SellingItemPrefab);

             _createdShopItem.transform.SetParent(ShopContents.transform);
             _createdShopItem.transform.localScale = Vector3.one;
             _createdShopItem.GetComponent<ShopItemBehaviour>().SetProprieties(gameObject, PlayerWardrobe.ItemList[i].ItemID, i, Mathf.CeilToInt(PlayerWardrobe.ItemList[i].ItemValue * 0.9f));
             _listedItems.Add(_createdShopItem);

    

        }

    }
    //Empties the list of items
    public void CleanList()
    {
        _listedItems.Clear();
        for (int i = 0; i < ShopContents.transform.childCount; i++)
        {

            Destroy(ShopContents.transform.GetChild(i).gameObject);
        }


    }

    public void BuyItem(GameObject Item)
    {

        PlayerWardrobe.AddItem(Shop.GetComponent<ShopInventory>().GetItemList()[Item.GetComponent<ShopItemBehaviour>().ItemIndex]);
        Shop.GetComponent<ShopBehaviour>().RemoveItem(Item.GetComponent<ShopItemBehaviour>().ItemIndex);
        _listedItems.Remove(Item);

        for (int i = 0; i < _listedItems.Count; i++)
        {
            _listedItems[i].GetComponent<ShopItemBehaviour>().ItemIndex = i;
        }

        Destroy(Item);



    }


    public void SellItem(GameObject Item)  {

       Shop.GetComponent<ShopBehaviour>().AddItem(PlayerWardrobe.GetItem(Item.GetComponent<ShopItemBehaviour>().ItemIndex));
       PlayerWardrobe.RemoveItem(PlayerWardrobe.GetItem(Item.GetComponent<ShopItemBehaviour>().ItemIndex));
        _listedItems.Remove(Item);

        for (int i = 0; i < _listedItems.Count; i++)
        {
            _listedItems[i].GetComponent<ShopItemBehaviour>().ItemIndex = i;
        }

        Destroy(Item);

    }

    //return to shop's main menu
    public void Return() {
        CleanList();
        Buttons.SetActive(true);
        

    }

}
