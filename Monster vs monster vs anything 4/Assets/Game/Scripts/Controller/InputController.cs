using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class InputController : MonoBehaviour 
{

    public delegate void inputEventController();

	public KeyCode  				rightControl_left,                      // equal to button Squad
				                    rightControl_up,                        // equal to button Triangle
				                    rightControl_right,                     // equal to button Circle
				                    rightControl_down,                      // equal to button X
				                    rightControlUp_Up,                      // equal to button R2
				                    rightControlUp_Down,                    // equal to button R1
				                    leftControlUp_Up,                       // equal to button L2
				                    leftControlUp_Down,                     // equal to button L1
				                    leftControl_left,                       // equal to button left arrow
				                    leftControl_up,                         // equal to button up arrow
				                    leftControl_right,                      // equal to button right arrow
				                    leftControl_down,                       // equal to button down arrow
				                    control_start,                          // equal to button start
				                    control_select;                         // equal to button select

    public inputEventController     ev_rightControl_left,                      // event to button Squad
                                    ev_rightControl_up,                        // event to button Triangle
                                    ev_rightControl_right,                     // event to button Circle
                                    ev_rightControl_down,                      // event to button X
                                    ev_rightControlUp_Up,                      // event to button R2
                                    ev_rightControlUp_Down,                    // event to button R1
                                    ev_leftControlUp_Up,                       // event to button L2
                                    ev_leftControlUp_Down,                     // event to button L1
                                    ev_leftControl_left,                       // event to button left arrow
                                    ev_leftControl_up,                         // event to button up arrow
                                    ev_leftControl_right,                      // event to button right arrow
                                    ev_leftControl_down,                       // event to button down arrow
                                    ev_control_start,                          // event to button start
                                    ev_control_select,                         // event to button select
                                    ev_last_frame;

    private bool buttonPress = false;

    void Update()
    {
        if (Input.anyKey)
            buttonPress = true;

        if (Input.GetKeyDown(rightControl_left))
        {

            if (this.ev_rightControl_left != null)
            {
                this.ev_rightControl_left();
            }
        }
        
		if (Input.GetKeyDown(rightControl_right))
		{

			if (this.ev_rightControl_right != null)
			{
				this.ev_rightControl_right();
			}
		}

        if(Input.GetKey(leftControl_left))
        {
            if(this.ev_leftControl_left != null)
            {
                this.ev_leftControl_left();
            }
        }

        if (Input.GetKey(leftControl_right))
        {
            if (this.ev_leftControl_right != null)
            {
                this.ev_leftControl_right();
            }
        }

        if (Input.GetKey(leftControl_up))
        {
            if (this.ev_leftControl_up != null)
            {
                this.ev_leftControl_up();
            }
        }

        if (Input.GetKey(leftControl_down))
        {
            if (this.ev_leftControl_down != null)
            {
                this.ev_leftControl_down();
            }
        }

        if (Input.GetKeyUp(leftControl_down) || Input.GetKeyUp(leftControl_left) || Input.GetKeyUp(leftControl_up) ||
            Input.GetKeyUp(leftControl_right))
        {
            this.eventLastFrame();
        }

        this.notEnyPressButton();

    }

    void notEnyPressButton()
    {
        if (this.buttonPress && !Input.anyKey)
        {
            this.eventLastFrame();
        }
    }

    void eventLastFrame()
    {
        if (this.ev_last_frame != null)
        {
            this.buttonPress = !this.buttonPress;
            ev_last_frame();
        }
    }

}
