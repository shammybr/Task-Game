using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeBehaviour : MonoBehaviour, InteractableBehaviour
{
    public List<ItemData.EItemID> ItemList;
    public WardrobeUIBehaviour WardrobeUIBehaviour;
    public PlayerWardrobe PlayerWardrobe;
    bool _isOpen;

    //wardrobe's collision
    BoxCollider2D _collisionBox;
    int _collisionLayer;


    private void Awake()
    {
        _collisionBox = gameObject.GetComponent<BoxCollider2D>();
        _collisionLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //if the wardrobe is open, keep checking if player is within bounds
        if (_isOpen)
        {

            RaycastHit2D hit = Physics2D.BoxCast(_collisionBox.bounds.center, _collisionBox.bounds.size, 0, Vector2.up, 0.0f, _collisionLayer);

            //if not, close it
            if (hit.collider == null)
            {
                WardrobeUIBehaviour.HideMenu();
                _isOpen = false;
            }
        }

    }


    public void Interact()
    {
        //opens menu
        if (WardrobeUIBehaviour != null)
        {
            //if the shop is open, close it, else open it
            if (WardrobeUIBehaviour.IsOpen)
            {
                WardrobeUIBehaviour.HideMenu();
                _isOpen = false;

            }
            else
            {
                WardrobeUIBehaviour.ShowMenu();
                _isOpen = true;

            }

        }



    }

    //add item to wardrobe
    public void AddItem(ItemData Item)
    {
        PlayerWardrobe.ItemList.Add(Item);

    }

    //remove item from wardrobe
    public void RemoveItem(int ItemIndex)
    {

        PlayerWardrobe.ItemList.RemoveAt(ItemIndex);
    }



}
