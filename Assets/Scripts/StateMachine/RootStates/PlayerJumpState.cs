using UnityEngine;

public class PlayerJumpState : APlayerBaseState
{
    public PlayerJumpState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory
        ): base(currentContext, playerStateFactory){
            _isRootState = true;
            InitializeSubState();
        }

    public override void EnterState(){
        Debug.Log("Entered jump state!");
        Jump();
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){
        Debug.Log("Exited jump state!");
    }
    
    public override void InitializeSubState(){}

    public override void CheckSwitchStates(){;
        if (_ctx.CharacterController.isGrounded){
            SwitchState(_stateFactory.Grounded());
        }
    }

    private void Jump(){
        _ctx.Velocity += new Vector3(0f, _ctx.JumpPower, 0f);
    }
}
