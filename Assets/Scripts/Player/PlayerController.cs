using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //store player movement input
    float _horizontalInput, _verticalInput, _horizontalSign, _verticalSign;

    //player collision
    BoxCollider2D _collisionBox;

    //player speed
    public float Speed;

    int _collisionLayer;
    int _interactableLayer;

    Animator _animator;
  
    public enum EFacingDirection { Up, Down, Left, Right };

    //direction the player is facing
    EFacingDirection _facingDirection;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
       _collisionLayer = LayerMask.GetMask("Collision");
        _interactableLayer = LayerMask.GetMask("Interactable");
       _collisionBox = gameObject.GetComponent<BoxCollider2D>();
       _animator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        Movement();

        //if space is pressed (interact button)
        if (Input.GetButtonDown("Interact"))
            Interact();
    }
        
    void Movement(){
        //store horizontal input
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        //store vertical input
        _verticalInput = Input.GetAxisRaw("Vertical");

        //set animator variables
        _animator.SetFloat("HorizontalInput", _horizontalInput);
        _animator.SetFloat("VerticalInput", _verticalInput);
      

        //store input direction
        if (_horizontalInput < 0)
        {
            _horizontalSign = 1.0f;
        }
        else
        {
            _horizontalSign = -1.0f;
        }

        //store input direction

        if (_verticalInput < 0)
        {
            _verticalSign = 1.0f;
        }
        else
        {
            _verticalSign = -1.0f;
        }
        //checks for collision with 2D raycasts

        if(_horizontalInput == 0.0f && _verticalInput == 0.0f)      {
            _animator.SetBool("Stopped", true);
          
        }
        else
        {
            _animator.SetBool("Stopped", false);
          
        }

        if (_horizontalInput != 0)
        {
            RaycastHit2D horizontalHit = Physics2D.BoxCast(_collisionBox.bounds.center, _collisionBox.bounds.size, 0, new Vector2(-_horizontalSign, 0), Time.deltaTime * Speed + 0.2f, _collisionLayer);

            //if horizontal raycast didn't hit
            if (horizontalHit.collider == null)
            {
                //move horizontally
                transform.position += new Vector3(_horizontalInput * Time.deltaTime * Speed, 0, 0);
            }
            else
            {
                //move player to wall bounds
                transform.position = new Vector3(horizontalHit.collider.bounds.center.x + (horizontalHit.collider.bounds.size.x / 2 * _horizontalSign) + (_collisionBox.size.x / 2 * _horizontalSign) + (0.1f * _horizontalSign), transform.position.y, 0);

            }
        }

        if (_verticalInput != 0)
        {
            RaycastHit2D verticalHit = Physics2D.BoxCast(_collisionBox.bounds.center, _collisionBox.bounds.size, 0, new Vector2(0 , -_verticalSign), Time.deltaTime * Speed + 0.2f, _collisionLayer);

            //if vertical raycast didn't hit
            if (verticalHit.collider == null)
            {
                //move vertically
                transform.position += new Vector3(0, _verticalInput * Time.deltaTime * Speed, 0);
            }
            else
            {
                //move player to wall bounds
                transform.position = new Vector3(transform.position.x , verticalHit.collider.bounds.center.y + (verticalHit.collider.bounds.size.y / 2 * _verticalSign) + (_collisionBox.size.y / 2 * _verticalSign) + (0.1f * _verticalSign), 0);

            }
        }



    }

    //update player direction status
    public void SetDirection(EFacingDirection direction)
    {

        _facingDirection = direction;


    }

    void Interact() {

        Vector2 _arrayDirection = Vector2.up;
        
        //check the direction the player is facing
        switch (_facingDirection)
        {
            case EFacingDirection.Left:

                _arrayDirection = Vector2.left;
                break;


            case EFacingDirection.Right:

                _arrayDirection = Vector2.right;
                break;

            case EFacingDirection.Up:

                _arrayDirection = Vector2.up;
                break;

            case EFacingDirection.Down:

                _arrayDirection = Vector2.down;
                break;



        }

        //casts an array where there player is facing
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _arrayDirection, 3.0f, _interactableLayer);

        //if the array hit
        if (hit.collider != null)
        {
            //interact with the object
            InteractableBehaviour interactable =  hit.collider.gameObject.GetComponent<InteractableBehaviour>();
            interactable.Interact();

        }

    }


}
