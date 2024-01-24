using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTargetState : State //Inhrerit from State class, which itself has instance of AI and StateMachine
    //Neat trick: generate overrides quick from State 'interface' by rightclick -> gen override
    //Add  constructor w this too
{
    //Bool used to check between two substates 
    //they may work better as a seperate state but too late!!
    private bool goCenter;
    private bool investigateFront;

    

    public SearchTargetState(AI ai, StateMachine stateMachine) : base(ai, stateMachine)
    {
        //Investigation state where bot will move straight towards detected L1 sensors in order to find Target
        //If L2 triggered, switch to FollowTargetState
        //else, stay here
    }

    public override void EnterState()
    {
        base.EnterState();
        //ai.SetCurrentStateName("Search Target State: \n Limited tracking and reserved movement, can move to center if lose target entirely");
        ai.SetCurrentStateName(0);
        Debug.Log("Entered SearchTargetState");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit SearchTargetState");
    }

    public override void Update()
    {
        base.Update();
        //Base behavior of this state: for now is to be in the middle of the room and look
        //if the bot is currently investigating a quadrant, dont engage L1 checks for now
        //if not itll bounce back n forth
        //not sure if this is the best way to do it
            if (ai.IsL1() == true)
            {
                goCenter = false;
                ai.TrackTargetSlowly();
                investigateFront = true;

            }

        
        else if (ai.IsL1() == false) //outside of L1
        {
            goCenter = true;
            investigateFront = false;
        }




        //Switch state to FollowTargetState
        if (ai.IsL2() == true)
        {
            ai.StateMachine.ChangeState(ai.FollowTargetState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (goCenter == true)
        {
            ai.MoveToCenter();
        }
        if (investigateFront == true) //find a way to decouple bool check in updates and movement in physics update. Another bool?
        {
            ai.MoveSlowly();
        }


    }
    //slowly approach

}
