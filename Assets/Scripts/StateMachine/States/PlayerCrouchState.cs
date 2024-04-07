using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : APlayerBaseState
{
    public PlayerCrouchState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){}

    public override void EnterState(){
        Debug.Log("Entered crouch state!");
    }
    
    public override void UpdateState(){
        CheckSwitchStates();
    }
    
    public override void ExitState(){
        Debug.Log("Exited crouch state!");
    }
    
    public override void InitializeSubState(){}
    
    public override void CheckSwitchStates(){
        if (!_ctx.IsCrouchPressed){
            SwitchState(_stateFactory.Walk());
        }
    }
}
