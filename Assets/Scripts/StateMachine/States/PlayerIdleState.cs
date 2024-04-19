using UnityEngine;

public class PlayerIdleState : APlayerBaseState
{
    public PlayerIdleState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory
        ): base(currentContext, playerStateFactory){}
        
    public override void EnterState(){
        Debug.Log("Entered idle state!");
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){
        Debug.Log("Exited idle state!");
    }

    public override void InitializeSubState(){}
    
    public override void CheckSwitchStates(){
        if(_ctx.IsWalkPressed){
            SwitchState(_stateFactory.Walk());

        } else if (_ctx.IsTiptoePressed){
            SwitchState(_stateFactory.Tiptoe());
            
        } else if (_ctx.IsCrouchPressed){
            SwitchState(_stateFactory.Crouch());
        }
    }
}
