using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIBehaviour : MonoBehaviour
{
    //UI shop item prefab
    public GameObject ItemPrefab;

    //UI shop wrapper
    public GameObject ShopContents;

    //the shop being open
    public GameObject Shop;

    public GameObject Player;
    public PlayerWardrobe PlayerWardrobe;


    public ItemDatabase ItemDB;


    public bool IsOpen;
    Animator _uiAnimator;


    List<GameObject> ListedItems;

    private void Awake() {
        ListedItems = new List<GameObject>();
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

    public void ShowMenu()
    {
        IsOpen = true;

        //put the shop on view
        if (_uiAnimator != null)
            _uiAnimator.Play("UIAnimationIn");
    }


    public void HideMenu()
    {

        IsOpen = false;

        //hide shop
        if(_uiAnimator != null)
        _uiAnimator.Play("UIAnimationOut");
        CleanList();
    }

    //Populates the list of items
    public void InitializeItemList(GameObject Shop, List<ItemData.EItemID> ItemList)  {

        //reference from the opening shop
        this.Shop = Shop;

        
        for (int i = 0; i < ItemList.Count; i++) {

                switch (ItemList[i])
                {
                    case ItemData.EItemID.TopHat:
                        GameObject _createdShopItem = Instantiate(ItemPrefab);

                        _createdShopItem.transform.SetParent(ShopContents.transform);
                        _createdShopItem.transform.localScale = Vector3.one;
                        _createdShopItem.GetComponent<ShopItemBehaviour>().SetProprieties(gameObject, ItemData.EItemID.TopHat, i, ItemDB.GetItemBasePrice(ItemData.EItemID.TopHat));
                         ListedItems.Add(_createdShopItem);

                    break;

                }
              
            
            
        }
        


    }

    //Empties the list of items
    public void CleanList()
    {
        ListedItems.Clear();
        for (int i = 0; i < ShopContents.transform.childCount; i++)
        {
            
            Destroy(ShopContents.transform.GetChild(i).gameObject);
        }


    }

    public void BuyItem(GameObject Item)  {

        PlayerWardrobe.AddItem(Item.GetComponent<ShopItemBehaviour>().ItemID);
        Shop.GetComponent<ShopBehaviour>().RemoveItem(Item.GetComponent<ShopItemBehaviour>().ItemIndex);
        ListedItems.Remove(Item);

        for (int i = 0; i < ListedItems.Count; i++)   {
            ListedItems[i].GetComponent<ShopItemBehaviour>().ItemIndex = i;
        }

        Destroy(Item);
      


    }
}
