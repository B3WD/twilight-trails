using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext) {
        _context = currentContext;
    }

    public APlayerBaseState Idle(){
        return new PlayerIdleState(_context, this);
    }

    public APlayerBaseState Walk(){
        return new PlayerWalkState(_context, this);
    }

    public APlayerBaseState Run(){
        return new PlayerRunState(_context, this);
    }

    public APlayerBaseState Jump(){
        return new PlayerJumpState(_context, this);
    }

    public APlayerBaseState Grounded(){
        return new PlayerGroundedState(_context, this);
    }

    public APlayerBaseState Crouch(){
        return new PlayerCrouchState(_context, this);
    }

    public APlayerBaseState Tiptoe(){
        return new PlayerTiptoeState(_context, this);
    }

    public APlayerBaseState Fall(){
        return new PlayerFallState(_context, this);
    }
}