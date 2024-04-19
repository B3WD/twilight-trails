using UnityEngine;

public class PlayerTiptoeState : APlayerBaseState
{
    public PlayerTiptoeState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){}

    public override void EnterState(){
        Debug.Log("Entered tiptoe state!");
    }
    
    public override void UpdateState(){
        CheckSwitchStates();
    }
    
    public override void ExitState(){
        Debug.Log("Exited tiptoe state!");
    }
    
    public override void InitializeSubState(){}
    
    public override void CheckSwitchStates(){
        if (!_ctx.IsTiptoePressed){
            SwitchState(_stateFactory.Walk());
        }
    }
}
