public abstract class APlayerBaseState
{
    protected bool _isRootState = false;
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _stateFactory;
    protected APlayerBaseState _currentSubState;
    protected APlayerBaseState _currentSuperState;

    public APlayerBaseState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory
    ){
        _ctx = currentContext;
        _stateFactory = playerStateFactory;
    }


    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();


    public void UpdateStates(){
        UpdateState();
        if(_currentSubState != null){
            _currentSubState.UpdateStates();
        }
    }
    
    protected void SwitchState(APlayerBaseState newState){
        ExitState();

        newState.EnterState();

        if (_isRootState){
            _ctx.CurrentState = newState; // C# getters are strange?
            
        } else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(APlayerBaseState newSuperState){
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(APlayerBaseState newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
