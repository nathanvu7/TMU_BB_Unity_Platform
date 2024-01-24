using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveState : State //L3
{
    public OffensiveState(AI ai, StateMachine stateMachine) : base(ai, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        //ai.SetCurrentStateName("Attack Target State: \n Full target tracking with aggressive movement");
        ai.SetCurrentStateName(2);
        Debug.Log("Entered Offense");

    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit Offense");
    }

    public override void Update()
    {
        base.Update();
        //Base behavior of this state: Still locked on to target
        ai.TrackTarget();
        

        if (ai.IsL3() != true && ai.IsL2() == true)
        {
            ai.StateMachine.ChangeState(ai.FollowTargetState);
        }
        if (ai.IsL3() != true && ai.IsL2() != true)
        {
            ai.StateMachine.ChangeState(ai.SearchTargetState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        ai.MoveForward();
    }
}
