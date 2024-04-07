using UnityEngine;

public class PlayerWalkState : APlayerBaseState
{
    public PlayerWalkState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){}

    public override void EnterState(){
        Debug.Log("Entered walk state!");
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }
    
    public override void ExitState(){
        Debug.Log("Exited walk state!");
        _ctx.IsWalkPressed = false;
    }
    
    public override void InitializeSubState(){}
    
    public override void CheckSwitchStates(){
        if(!_ctx.IsWalkPressed){
            SwitchState(_stateFactory.Idle());
        
        } else if (_ctx.IsRunPressed){
            SwitchState(_stateFactory.Run());

        } else if (_ctx.IsTiptoePressed){
            SwitchState(_stateFactory.Tiptoe());
            
        } else if (_ctx.IsCrouchPressed){
            SwitchState(_stateFactory.Crouch());
        }
    }
}
