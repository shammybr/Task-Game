using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIBehaviour : MonoBehaviour
{
    public GameObject ItemPrefab;
    public GameObject ShopContents;
    public ItemDatabase ItemDB;
    public bool IsOpen;
    Animator _uiAnimator;
    
    private void Awake() {
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
    public void InitializeItemList(List<ItemData.EItemID> ItemList)  {
  

        foreach(ItemData.EItemID item in ItemList)   {

            //get itemprefab from the database
             GameObject _createdShopItem = Instantiate(ItemPrefab);
             _createdShopItem.transform.SetParent(ShopContents.transform);


        }

        


    }

    //Empties the list of items
    public void CleanList()
    {
        for (int i = 0; i < ShopContents.transform.childCount; i++)
        {
            Destroy(ShopContents.transform.GetChild(i).gameObject);
        }


    }
}
