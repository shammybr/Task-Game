using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour, InteractableBehaviour
{

    public ShopInventory Inventory;
    public GameObject ShopUI;
    public ShopUIBehaviour ShopUIBehaviour;
    public TextMeshPro DialogueMesh;
<<<<<<< HEAD
    public float _dialogueSpeed;
<<<<<<< HEAD
=======
>>>>>>> parent of 587d3a5 (Added Standalone Game and fixed version)
=======
    public List<string> Dialogue;
    public float DialogueSpeed;
>>>>>>> parent of 2fd967f (Merge branch 'master' of https://github.com/shammybr/Task-Game)
=======
>>>>>>> parent of 5f4413e (test)

    bool _isOpen;

    string _dialogueBuffer;
    bool _isGeneratingDialogue;
    int _dialogueIndex;
    float _dialogueTiming;
    

    //shop's collision
    BoxCollider2D _collisionBox;
    int _collisionLayer;

    private void Awake()
    {
        _collisionBox = gameObject.GetComponent<BoxCollider2D>();
        _collisionLayer = LayerMask.GetMask("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the shop is open, keep checking if player is within bounds
        if (_isOpen)   {

            RaycastHit2D hit = Physics2D.BoxCast(_collisionBox.bounds.center, _collisionBox.bounds.size, 0, Vector2.up, 0.0f, _collisionLayer);

            //if not, close it
            if (hit.collider == null)     {
                ShopUIBehaviour.HideMenu();
                _isOpen = false;
                CloseDialogue();
            }
        }

        //generates dialogue
        if (_isGeneratingDialogue)    {

            if (_dialogueTiming > 1.0f)
                DialogueLoop();
            else
                _dialogueTiming += Time.deltaTime * DialogueSpeed;

        }
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 587d3a5 (Added Standalone Game and fixed version)
=======

>>>>>>> parent of 2fd967f (Merge branch 'master' of https://github.com/shammybr/Task-Game)
=======
>>>>>>> parent of 5f4413e (test)
    }

    public void Interact()
    {
        
        //opens menu
        if (ShopUIBehaviour != null)
        {
            //if the shop is open, close it, else open it
            if (ShopUIBehaviour.IsOpen)   {
                ShopUIBehaviour.HideMenu();
                _isOpen = false;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
             
=======
              
>>>>>>> parent of 587d3a5 (Added Standalone Game and fixed version)
=======
                CloseDialogue();
>>>>>>> parent of 2fd967f (Merge branch 'master' of https://github.com/shammybr/Task-Game)
=======
             
>>>>>>> parent of 5f4413e (test)
            }
            else  {
                ShopUIBehaviour.ShowMenu(gameObject, Inventory.GetItemList());
                _isOpen = true;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
                Talk("Hello");
=======

>>>>>>> parent of 587d3a5 (Added Standalone Game and fixed version)
=======
                Talk(Dialogue[Random.Range(0, Dialogue.Count)]);
>>>>>>> parent of 2fd967f (Merge branch 'master' of https://github.com/shammybr/Task-Game)
=======
                Talk("Hello");
>>>>>>> parent of 5f4413e (test)
            }

        }
        
    }

    //remove item from shop's inventory
    public void RemoveItem(int ItemIndex)   {
        Inventory.GetItemList().RemoveAt(ItemIndex);
    }

    //add item in shop's inventory
    public void AddItem(ItemData Item)
    {
        Inventory.GetItemList().Add(Item);

    }


    public void Talk(string Dialogue)  {
        DialogueMesh.text = null;
        _dialogueBuffer = Dialogue;
        _isGeneratingDialogue = true;
        _dialogueIndex = 0;
    }

    public void DialogueLoop()
    {
        if (_dialogueIndex < _dialogueBuffer.Length) { 

            DialogueMesh.text += _dialogueBuffer[_dialogueIndex];
            _dialogueIndex++;

         }
        else    {
            _dialogueBuffer = null;
            _isGeneratingDialogue = false;
            _dialogueIndex = 0;
        }

        _dialogueTiming = 0;
    }

    public void CloseDialogue()   {
        DialogueMesh.text = null;
        _dialogueBuffer = null;
        _isGeneratingDialogue = false;
        _dialogueIndex = 0;

    }
}
