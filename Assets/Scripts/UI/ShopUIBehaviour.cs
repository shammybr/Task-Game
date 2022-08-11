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

    GameObject _previewingHat;
    ItemData.EItemID _equippedHat;
    int _previewingHatIndex;
    bool _isPreviewing;

    private void Awake()
    {
        _shopItems = new List<ItemData>();
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

        //stores actually equipped hat
        _equippedHat = Player.GetComponent<PlayerStats>().GetEquippedHat();
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

        if (_isPreviewing)
            UnequipPreview();
    }

    //Populates the list of items
    public void InitializeBuyingItemList()
    {

        Buttons.SetActive(false);
    
        for (int i = 0; i < _shopItems.Count; i++)
        {

    
           GameObject _createdShopItem = Instantiate(BuyingItemPrefab);

            _createdShopItem.transform.SetParent(ShopContents.transform);
            _createdShopItem.transform.localScale = Vector3.one;
            _createdShopItem.GetComponent<ShopItemBehaviour>().SetProprieties(gameObject, _shopItems[i].ItemID, i, _shopItems[i].ItemValue, _shopItems[i].ShopSprite);
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
             _createdShopItem.GetComponent<ShopItemBehaviour>().SetProprieties(gameObject, PlayerWardrobe.ItemList[i].ItemID, i, Mathf.CeilToInt(PlayerWardrobe.ItemList[i].ItemValue * 0.9f), PlayerWardrobe.ItemList[i].ShopSprite);
             _listedItems.Add(_createdShopItem);

    

        }

    }

    //Empties the list of items
    public void CleanList()  {

        _listedItems.Clear();
        for (int i = 0; i < ShopContents.transform.childCount; i++)
        {

            Destroy(ShopContents.transform.GetChild(i).gameObject);
        }


    }

    public void BuyItem(GameObject Item) {

        int ItemIndex = Item.GetComponent<ShopItemBehaviour>().ItemIndex;

        PlayerWardrobe.AddItem(Shop.GetComponent<ShopInventory>().GetItemList()[ItemIndex]);
        Shop.GetComponent<ShopBehaviour>().RemoveItem(ItemIndex);

        //if previewing, equip it
        if (_isPreviewing && _previewingHatIndex == ItemIndex)   
            EquipItem(Item);

        if (_previewingHatIndex >= 0)
            //correct for new indexes
            _previewingHat = _listedItems[_previewingHatIndex];
        else
            _previewingHat = null;


        _listedItems.Remove(Item);
        //att the indexes
        for (int i = 0; i < _listedItems.Count; i++)
        {
            _listedItems[i].GetComponent<ShopItemBehaviour>().ItemIndex = i;
        }

        if(_previewingHat != null)
        _previewingHatIndex = _previewingHat.GetComponent<ShopItemBehaviour>().ItemIndex;

        Destroy(Item);



    }


    public void SellItem(GameObject Item)  {
      
        int ItemIndex = Item.GetComponent<ShopItemBehaviour>().ItemIndex;

        //unequip if hat is equipped
        if (PlayerWardrobe.ItemList[ItemIndex].IsEquipped())    {
            Player.GetComponent<PlayerStats>().UnequipHat();
            PlayerWardrobe.SetEquipped(ItemIndex, false);
            _equippedHat = ItemData.EItemID.NoHat;
        }

        Shop.GetComponent<ShopBehaviour>().AddItem(PlayerWardrobe.GetItem(ItemIndex));
        PlayerWardrobe.RemoveItem(PlayerWardrobe.GetItem(ItemIndex));
        _listedItems.Remove(Item);

        //att the indexes
        for (int i = 0; i < _listedItems.Count; i++)
        {
            _listedItems[i].GetComponent<ShopItemBehaviour>().ItemIndex = i;
        }

        Destroy(Item);

    }

    public void PreviewItem(GameObject Item){
        
        _isPreviewing = true;

        //stores what item is being previewed
        _previewingHatIndex = Item.GetComponent<ShopItemBehaviour>().ItemIndex;
        Player.GetComponent<PlayerStats>().EquipHat(Item.GetComponent<ShopItemBehaviour>().ItemID);


    }

    //reequips original hat
    public void UnequipPreview() {
  
        _isPreviewing = false;
        _previewingHatIndex = -1;
        Player.GetComponent<PlayerStats>().EquipHat(_equippedHat);
    }

    public void EquipItem(GameObject Item)  {
 
        Player.GetComponent<PlayerStats>().EquipHat(Item.GetComponent<ShopItemBehaviour>().ItemID);
        PlayerWardrobe.SetEquipped(PlayerWardrobe.ItemList.Count - 1, true);
        _isPreviewing = false;
        _previewingHatIndex = -1;

    }

    //return to shop's main menu
    public void Return() {
        CleanList();
        Buttons.SetActive(true);
        

    }

}
