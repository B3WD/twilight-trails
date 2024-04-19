using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerStateMachine : MonoBehaviour
{
    private CharacterController _characterController;
    private Camera _playerCamera;
    private Vector2 input;
    private float mouseY;
    private float mouseX;
    private Vector3 _moveDirection;
    private Vector2 lookDirection;
    private Vector3 bodyRotation;
    private Vector3 _cameraPitch;
    private Vector3 _relativeVelocity;
    
    private Vector3 _velocity = Vector3.zero;
    private float _gravity = -9.81f;


    [Header("Mouse settings")]
    [SerializeField] private float mouseSensitivity;

    [Header("Movement settings")]
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float backwardSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float _gravityMultiplier;
    [SerializeField] private float _jumpPower;

    // Variables
    private bool _isWalkPressed;
    private bool _isRunPressed;
    private bool _isCrouchPressed;
    private bool _isTiptoePressed;
    private bool _isJumpPressed;

    // States
    private APlayerBaseState _currentState;
    private PlayerStateFactory _stateFactory;


    // Getters and Setters
    public APlayerBaseState CurrentState { 
        get { return _currentState; } 
        set { _currentState = value; }
    }

    public CharacterController CharacterController{
        get { return _characterController; }
    }

    public Vector3 Velocity { 
        get { return _velocity; }
        set { _velocity = value; } 
    }

    public Vector3 MoveDirection {
        get { return _moveDirection; }
        set { _moveDirection = value; }
    }

    public bool IsJumpPressed {
        get { return _isJumpPressed; }
        set { _isJumpPressed = value; }
    }

    public bool IsWalkPressed{	
        get { return _isWalkPressed; }
        set { _isWalkPressed = value; }
    }

    public bool IsCrouchPressed{
        get { return _isCrouchPressed; }
    }

    public bool IsRunPressed{
        get { return _isRunPressed; }
    }

    public bool IsTiptoePressed{
        get { return _isTiptoePressed; }
    }

    public float JumpPower {
        get { return _jumpPower; }
        set { _jumpPower = value; }
    }

    public float ForwardSpeed {
        get { return _forwardSpeed; }
        set { _forwardSpeed = value; }
    }

    public float SprintMultiplier {
        get { return _sprintMultiplier; }
        set { _sprintMultiplier = value; }
    }

    // public float CrouchSpeed{
    //     get { return _crouchSpeed; }
    //     set { _crouchSpeed = value; }
    // }


    // public float TiptoeSpeed{
    //     get { return _tiptoeSpeed; }
    //     set { _tiptoeSpeed = value; }
    // }


    void Awake(){
        _characterController = GetComponent<CharacterController>();
        _playerCamera = GetComponentInChildren<Camera>();

        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.Grounded();
        _currentState.EnterState();

        _cameraPitch = _playerCamera.transform.localRotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start(){}

    void Update(){
        _currentState.UpdateStates();

        ApplyGravity();
        UpdatePosition();
        UpdateView();
    }

    private void ApplyGravity(){
        if(_characterController.isGrounded && _velocity.y < 0f){
            _velocity.y = -1f;
        } else {
            _velocity.y += _gravity * _gravityMultiplier;
        }
    }

    private void UpdatePosition(){
        _relativeVelocity = transform.TransformDirection(_velocity * Time.deltaTime);
        CharacterController.Move(_relativeVelocity);
    }

    private void UpdateView(){
        RotateBody();
        PitchCamera();
    }

    private void RotateBody(){
        mouseX = mouseSensitivity * lookDirection.x * Time.deltaTime;

        bodyRotation = transform.localRotation.eulerAngles;
        bodyRotation.y += mouseX;

        transform.localRotation = Quaternion.Euler(bodyRotation);
    }

    private void PitchCamera(){
        mouseY = mouseSensitivity * lookDirection.y * Time.deltaTime;

        _cameraPitch.x -= mouseY;
        _cameraPitch.x = Mathf.Clamp(_cameraPitch.x, -90f, 90f);

        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraPitch);
    }

    // Actions
    public void OnMove(InputAction.CallbackContext context){
        input = context.ReadValue<Vector2>();
        _isWalkPressed = !input.Equals(Vector2.zero); // make func?

        _moveDirection = new Vector3(input.x, 0f, input.y);
        _velocity = new Vector3(0f, _velocity.y, 0f) + (_moveDirection * _forwardSpeed);
    }

    public void OnLook(InputAction.CallbackContext context){
        lookDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context){
        _isJumpPressed = context.performed; // TODO: Fix auto jump bug
    }

    public void OnSprint(InputAction.CallbackContext context){ //TODO: Fix sprint bug - spam shift to kill fall
        _isRunPressed = context.performed;
    }

    // private void OnCrouch(InputAction.CallbackContext context){
    //     _isCrouchPressed = inputValue.isPressed;
    // }

    // private void OnTiptoe(InputAction.CallbackContext context){
    //     _isTiptoePressed = inputValue.isPressed;
    // }
}
