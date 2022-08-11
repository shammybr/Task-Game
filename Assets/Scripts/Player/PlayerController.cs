using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator HatAnimator;


    //store player movement input
    float _horizontalInput, _verticalInput, _horizontalSign, _verticalSign;

    //player collision
    BoxCollider2D _collisionBox;

    //player speed
    public float Speed;

    int _collisionLayer;
    int _interactableLayer;
    bool _isRunning;
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

    //begins with the player facing down
    void Start()
    {

        _animator.Play("Player_IdleDown", 0, 0);
        _facingDirection = EFacingDirection.Down;
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
      

        if(_horizontalInput == 0.0f && _verticalInput == 0.0f)      {
            _animator.SetBool("Stopped", true);
            _isRunning = false;
        }
        else
        {
            _animator.SetBool("Stopped", false);
            _isRunning = true;
        }

        //  Debug.DrawRay(_collisionBox.bounds.center + new Vector3(0, _collisionBox.bounds.size.y / 2, 0), new Vector3(0, Time.deltaTime * Speed + 0.2f), Color.red);

        //checks for collision with 2D raycasts
        //the center of the collision box doesn't update instantly after changing the transform position, so we have to
        //calculate the location of the hitbox ourselves using the offset
        if (_horizontalInput != 0)
        {
            RaycastHit2D horizontalHit = Physics2D.BoxCast(transform.position + new Vector3(_collisionBox.offset.x, _collisionBox.offset.y, 0), _collisionBox.bounds.size, 0, new Vector2(-_horizontalSign, 0), Time.deltaTime * Speed + 0.2f, _collisionLayer);

            //if horizontal raycast didn't hit
            if (horizontalHit.collider == null)
            {
                //move horizontally
                transform.position += new Vector3(_horizontalInput * Time.deltaTime * Speed, 0, 0);
            }
            else
            {
                //move player to wall bounds
                transform.position = new Vector3(-_collisionBox.offset.x + horizontalHit.collider.bounds.center.x + (horizontalHit.collider.bounds.size.x / 2 * _horizontalSign) + (_collisionBox.size.x / 2 * _horizontalSign) + (0.1f * _horizontalSign), transform.position.y, 0);

            }
        }

 

        if (_verticalInput != 0)
        {
            RaycastHit2D verticalHit = Physics2D.BoxCast(transform.position + new Vector3(_collisionBox.offset.x, _collisionBox.offset.y, 0), _collisionBox.bounds.size, 0, new Vector2(0 , -_verticalSign), Time.deltaTime * Speed + 0.2f, _collisionLayer);

            //if vertical raycast didn't hit
            if (verticalHit.collider == null)
            {
                //move vertically
                transform.position += new Vector3(0, _verticalInput * Time.deltaTime * Speed, 0);
            }
            else
            {
                //move player to wall bounds
                transform.position = new Vector3(transform.position.x , -_collisionBox.offset.y + verticalHit.collider.bounds.center.y + (verticalHit.collider.bounds.size.y / 2 * _verticalSign) + (_collisionBox.size.y / 2 * _verticalSign) + (0.1f * _verticalSign), 0);

            }
        }



    }

    //update player direction status and syncs hat's animation
    public void SetDirection(EFacingDirection direction)
    {
        float time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        _facingDirection = direction;
      
        switch (direction)  {
            case EFacingDirection.Left:
                if (_isRunning)
                    HatAnimator.Play("HatRunLeft", 0 , time);
                else
                    HatAnimator.Play("HatIdleLeft", 0, time);

               
                break;


            case EFacingDirection.Up:
                if (_isRunning)
                    HatAnimator.Play("HatRunUp", 0, time);
                else
                    HatAnimator.Play("HatIdleUp", 0, time);

                break;





            case EFacingDirection.Down:
                if (_isRunning)
                    HatAnimator.Play("HatRunDown", 0, time);
                else
                    HatAnimator.Play("HatIdleDown", 0, time);


                break;


            case EFacingDirection.Right:
                if (_isRunning)
                    HatAnimator.Play("HatRunRight", 0, time);
                else
                    HatAnimator.Play("HatIdleRight", 0, time);


                break;
        }

    }
    
    public void SetHatAnimation(int animation){
      

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _arrayDirection, 0.5f, _interactableLayer);

        //if the array hit
        if (hit.collider != null)
        {
            //interact with the object
            InteractableBehaviour interactable =  hit.collider.gameObject.GetComponent<InteractableBehaviour>();
            interactable.Interact();

        }

    }


}
