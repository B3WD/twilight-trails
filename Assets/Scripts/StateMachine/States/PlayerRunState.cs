using UnityEngine;

public class PlayerRunState : APlayerBaseState
{
    public PlayerRunState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){}

    public override void EnterState(){
        Debug.Log("Entered run state!");
        _ctx.Velocity = _ctx.MoveDirection * _ctx.ForwardSpeed * _ctx.SprintMultiplier;
    }
    
    public override void UpdateState(){
        CheckSwitchStates();
    }
    
    public override void ExitState(){
        Debug.Log("Exited run state!");
    }
    
    public override void InitializeSubState(){}
    
    public override void CheckSwitchStates(){
        if(!_ctx.IsRunPressed){
            SwitchState(_stateFactory.Walk());
        }
    }
}
