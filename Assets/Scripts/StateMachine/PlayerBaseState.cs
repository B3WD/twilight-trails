using Unity.VisualScripting.FullSerializer;

public abstract class PlayerBaseState
{
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;
    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;

    public PlayerBaseState(
        PlayerStateMachine currentContext, 
        PlayerStateFactory playerStateFactory
    ){
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();


    void UpdateStates(){}
    
    protected void SwitchState(PlayerBaseState newState){
        ExitState();

        newState.EnterState();

        _ctx.CurrentState = newState; // C# getters are strange?
    }

    protected void SetSuperState(PlayerBaseState newSubState){
        _currentSuperState = newSubState;
    }
    protected void SetSubState(PlayerBaseState newSubState){
        _currentSubState = newSubState;
        newSubState.SetSubState(this);
    }
}
