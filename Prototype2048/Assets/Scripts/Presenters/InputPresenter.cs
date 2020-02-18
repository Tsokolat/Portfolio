using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputPresenter : Presenter
{
    public Action<Vector2> OnSwipeAction;
    public Action KeySpacePressed;
    public Action KeyAPressed;
    public Action KeyDPressed;
    public Action KeySPressed;
    public Action KeyWPressed;
    public Action LevelUpPressed;
    public Action<Vector2> Swiped;

    public Action KeyPPressed;


    //Swipe related fields
    private Vector2 fingerDown;
    private Vector2 fingerUp;

    public float swipeThreshold = 20f;
    public bool detectSwipeOnlyAfterRelease = false;


    public override void OnAwake()
    {

    }

    void Update()
    {
        ProcessInput();
      

    }



    public void checkSwipe()
    {
        // Check if Vertical Swipe
        if (verticalMove() > swipeThreshold && verticalMove() > horizontalValMove())
        {
            Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0) //Up Swipe
            {
                OnSwipeUp();
                Swiped(Vector2.up);
            }
            else if (fingerDown.y - fingerUp.y < 0)// Down Swipe
            {
                OnSwipeDown();
                Swiped(Vector2.down);
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal Swipe
        else if (horizontalValMove() > swipeThreshold && horizontalValMove() > verticalMove())
        {
            Debug.Log("Horizontal");

            if (fingerDown.x - fingerUp.x > 0) // Swipe Right
            {
                OnSwipeRight();
                Swiped(Vector2.right);
                
            }
            else if (fingerDown.x - fingerUp.x < 0) // Swipe Left
            {
                OnSwipeLeft();
                Swiped(Vector2.left);
            }
            fingerUp = fingerDown;
        }

        else
        {
            Debug.Log("No Swipe Detected");
        }
    }


    private void OnSwipeLeft()
    {
        Debug.Log("Swipe Direction : Left");
    }

    private void OnSwipeRight()
    {
        Debug.Log("Swipe Direction : Right");
    }

    private void OnSwipeDown()
    {
        Debug.Log("Swipe Direction : Down");
    }

    private void OnSwipeUp()
    {
        Debug.Log("Swipe Direction : UP");
    }

    private float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    private float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }
   

    void ProcessInput()
    {
        ProcessTouchInput();
        ProcessKeyboardInput();
        
        
    }

    private void ProcessTouchInput()
    {
        //if (Input.touchCount > 0)
        //{
            //Goes through the amount of touches.

            Touch touch;
            try
            {
                touch = Input.touches.First(t => t.fingerId == 0);
            } 
            catch
            {
                return;
            }

                if (touch.phase == TouchPhase.Began)
                {
            HandleTouchBegin(touch);
                    
                }
                //Detects swipe while finger is still moving

                if (touch.phase == TouchPhase.Moved)
                {
            HandleTouchMove(touch);
                    
                }
                //Detects Swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
            HandleTouchEnded(touch);
                    
                }


        //}
    }

    private void HandleTouchEnded(Touch touch)
    {
        fingerDown = touch.position;
        checkSwipe();
    }

    private void HandleTouchMove(Touch touch)
    {
        if (!detectSwipeOnlyAfterRelease)
        {
            fingerDown = touch.position; //starting position.
            checkSwipe();
        }
    }

    private void HandleTouchBegin(Touch touch)
    {
        fingerUp = touch.position;
        fingerDown = touch.position;
    }

    private void ProcessKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            KeyPPressed();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            KeySpacePressed();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            KeyAPressed();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            KeyDPressed();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            KeySPressed();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            KeyWPressed();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            OnSwipeAction(Vector2.up);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnSwipeAction(Vector2.right);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            OnSwipeAction(Vector2.down);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnSwipeAction(Vector2.left);
        }
    }
}




