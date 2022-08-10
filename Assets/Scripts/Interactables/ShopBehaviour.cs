using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour, InteractableBehaviour
{

    public ShopInventory Inventory;
    public GameObject ShopUI;
    public ShopUIBehaviour ShopUIBehaviour;
 

    private void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        //opens menu
        if (ShopUIBehaviour != null)
        {
            //if the shop is open, close it, else open it
            if (ShopUIBehaviour.IsOpen)   {
                ShopUIBehaviour.HideMenu();
            }
            else  {
                ShopUIBehaviour.ShowMenu();
                //refresh shop item list
                ShopUIBehaviour.InitializeItemList(Inventory.ItemList);
            }

        }
        
    }


}
