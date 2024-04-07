using UnityEngine;

public class PlayerGroundedState : APlayerBaseState
{
    public PlayerGroundedState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory
        ): base(currentContext, playerStateFactory){
            _isRootState = true;
            InitializeSubState();
        }

    public override void EnterState(){
        Debug.Log("Entered grounded state!");
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){
        Debug.Log("Exited grounded state!");
    }

    public override void InitializeSubState(){
        if (!_ctx.IsWalkPressed && !_ctx.IsRunPressed){
            SetSubState(_stateFactory.Idle());
        } else if (_ctx.IsWalkPressed && !_ctx.IsRunPressed) {
            SetSubState(_stateFactory.Walk());
        } else {
            SetSubState(_stateFactory.Run());
        }
    }

    public override void CheckSwitchStates(){ // could this be unecesarry? Just put this logic in "UpdateState()"
        if (_ctx.IsJumpPressed){
            SwitchState(_stateFactory.Jump());
        }
    }
}
