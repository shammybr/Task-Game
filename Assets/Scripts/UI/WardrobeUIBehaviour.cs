using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeUIBehaviour : MonoBehaviour
{

    public GameObject EquipItemPrefab;
    public GameObject Player;
    public GameObject WardrobeContents;
    public ItemDatabase ItemDB;
    public PlayerWardrobe PlayerWardrobe;
    public bool IsOpen;


    List<ItemData.EItemID> _wardrobeItems;
    Animator _wardrobeAnimator;
    List<GameObject> _listedItems;
    int _equippedHatIndex;
    ItemData.EItemID _equippedHatID;
    bool _isPreviewing;
    int _previewingHatIndex;

    private void Awake()
    {
        _listedItems = new List<GameObject>();
        _wardrobeAnimator = gameObject.GetComponent<Animator>();
        _equippedHatIndex = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ShowMenu()
    {
        IsOpen = true;

        CleanList();

        //put the shop on view
        if (_wardrobeAnimator != null)
            _wardrobeAnimator.Play("WardrobeUIIn");

        InitializeHatItemList();
    }


    public void HideMenu()
    {

        IsOpen = false;


        //hide shop
        if (_wardrobeAnimator != null)
            _wardrobeAnimator.Play("WardrobeUIOut");
        CleanList();

        //if previewing item, re equip old hat
        if (_isPreviewing)
            UnequipPreview();
    }

    public void CleanList()
    {
        _equippedHatIndex = -1;
        _listedItems.Clear();
        for (int i = 0; i < WardrobeContents.transform.childCount; i++)
        {

            Destroy(WardrobeContents.transform.GetChild(i).gameObject);
        }

    }

    public void InitializeHatItemList() {

        for (int i = 0; i < PlayerWardrobe.ItemList.Count; i++)
        {

            GameObject _createdShopItem = Instantiate(EquipItemPrefab);

            _createdShopItem.transform.SetParent(WardrobeContents.transform);
            _createdShopItem.transform.localScale = Vector3.one;
            _createdShopItem.GetComponent<WardrobeItemBehaviour>().SetProprieties(gameObject, PlayerWardrobe.ItemList[i].ItemID, i, PlayerWardrobe.ItemList[i].ItemName, PlayerWardrobe.ItemList[i].ShopSprite);
            if (PlayerWardrobe.ItemList[i].IsEquipped())
            {
                _createdShopItem.GetComponent<WardrobeItemBehaviour>().SetEquipped(true);
                _equippedHatIndex = i;
            }
            _listedItems.Add(_createdShopItem);



        }


    }


    public void PreviewItem(GameObject Item)
    {
        _isPreviewing = true;

        //stores what item is being previewed
        _previewingHatIndex = Item.GetComponent<WardrobeItemBehaviour>().ItemIndex;
        Player.GetComponent<PlayerStats>().EquipHat(Item.GetComponent<WardrobeItemBehaviour>().ItemID);


    }

    //reequips original hat
    public void UnequipPreview()
    {

        _isPreviewing = false;
        _previewingHatIndex = -1;
        Player.GetComponent<PlayerStats>().EquipHat(_equippedHatID);
    }



    public void EquipItem(GameObject Item)
    {
        //if equippedhatindex is > 0, then some item is equipped
        if (_equippedHatIndex >= 0)  {
            //unequip it
            _listedItems[_equippedHatIndex].GetComponent<WardrobeItemBehaviour>().SetEquipped(false);
        }

        //get equipped hat index
        _equippedHatIndex = Item.GetComponent<WardrobeItemBehaviour>().ItemIndex;

        //set item to equiped in wardrobe
        PlayerWardrobe.SetEquipped(_equippedHatIndex, true);
        Item.GetComponent<WardrobeItemBehaviour>().SetEquipped(true);

        //equip  hat
        _isPreviewing = false;
        _previewingHatIndex = -1;
        _equippedHatID = Item.GetComponent<WardrobeItemBehaviour>().ItemID;
        Player.GetComponent<PlayerStats>().EquipHat(_equippedHatID);
       

    }


    public void UnequipItem(GameObject Item)
    {

        if(_equippedHatIndex == Item.GetComponent<WardrobeItemBehaviour>().ItemIndex)   {

            Player.GetComponent<PlayerStats>().UnequipHat();
            PlayerWardrobe.SetEquipped(Item.GetComponent<WardrobeItemBehaviour>().ItemIndex, false);
            _equippedHatID = ItemData.EItemID.NoHat;

            //refresh the button state and update hatindex
            if (_equippedHatIndex >= 0)  {
                _listedItems[_equippedHatIndex].GetComponent<WardrobeItemBehaviour>().SetEquipped(false);
                _equippedHatIndex = -1;
            }
               

        }


    }
}
