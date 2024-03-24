using System.Xml.Schema;
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
    private Vector3 cameraPitch;
    private Vector3 _relativeVelocity;

    private Vector3 _velocity = Vector3.zero;
    private float _gravity = -9.81f;


    [Header("Mouse settings")]
    [SerializeField] private float mouseSensitivity;

    [Header("Movement settings")]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float _jumpPower;

    // Variables
    private bool _isJumpPressed;

    // States
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    

    // Getters and Setters
    public PlayerBaseState CurrentState { 
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

    public float JumpPower {
        get { return _jumpPower; }
        set { _jumpPower = value; }
    }


    void Awake(){
        _characterController = GetComponent<CharacterController>();
        _playerCamera = GetComponentInChildren<Camera>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        cameraPitch = _playerCamera.transform.localRotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start(){
        Debug.Log(_jumpPower);
    }

    void Update(){
        _currentState.UpdateState();

        ApplyGravity();
        UpdatePosition();
        UpdateView();
    }

    private void ApplyGravity(){
        if(_characterController.isGrounded && _velocity.y < 0f){
            _velocity.y = -1f;
        } else {
            _velocity.y += _gravity * gravityMultiplier;
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

        cameraPitch.x -= mouseY;
        cameraPitch.x = Mathf.Clamp(cameraPitch.x, -90f, 90f);

        _playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch);
    }

    // Actions
    private void OnJump(InputValue inputValue){
        _isJumpPressed = inputValue.isPressed;
    }

    private void OnSprint(){
        _velocity = _moveDirection * forwardSpeed * _sprintMultiplier;
    }

    private void OnMove(InputValue inputValue){
        input = inputValue.Get<Vector2>();
        _moveDirection = new Vector3(input.x, 0f, input.y);
        _velocity = new Vector3(0f, _velocity.y, 0f) + (_moveDirection * forwardSpeed);
    }

    private void OnLook(InputValue inputValue){
        lookDirection = inputValue.Get<Vector2>();
    }
}
