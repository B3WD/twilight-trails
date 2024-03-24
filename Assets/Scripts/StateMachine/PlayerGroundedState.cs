using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){}

    public override void EnterState(){
        Debug.Log("Enter grounded state!");
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){
        Debug.Log("Exit grounded state!");
    }

    public override void InitializeSubState(){}

    public override void CheckSwitchStates(){
        if (_ctx.IsJumpPressed){
            SwitchState(_factory.Jump());
        }
    }
}
