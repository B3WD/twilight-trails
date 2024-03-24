using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){}

    public override void EnterState(){
        Jump();
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){
        _ctx.IsJumpPressed = false;
    }
    
    public override void InitializeSubState(){}

    public override void CheckSwitchStates(){;
        if (_ctx.CharacterController.isGrounded){
            SwitchState(_factory.Grounded());
        }
    }

    private void Jump(){
        _ctx.Velocity += new Vector3(0f, _ctx.JumpPower, 0f);
        // _ctx.Velocity += new Vector3(0f, 10f, 0f);
        Debug.Log(_ctx.JumpPower);
    }
}
