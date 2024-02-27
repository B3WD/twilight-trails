using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Camera playerCamera;
    private Vector2 input;
    private float mouseY;
    private float mouseX;
    private Vector3 moveDirection;
    private Vector2 lookDirection;
    private Vector3 bodyRotation;
    private Vector3 cameraPitch;
    private Vector3 relativeVelocity;

    [Header("Mouse settings")]
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("Movement settings")]
    [SerializeField] private float forwardSpeed = 5f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        cameraPitch = playerCamera.transform.localRotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdatePosition();
        UpdateView();
    }

    private void UpdatePosition()
    {
        relativeVelocity = transform.TransformDirection(forwardSpeed * Time.deltaTime * moveDirection);
        characterController.Move(relativeVelocity);
    }

    private void UpdateView()
    {
        RotateBody();
        PitchCamera();
    }

    private void RotateBody()
    {
        mouseX = mouseSensitivity * lookDirection.x * Time.deltaTime;

        bodyRotation = transform.localRotation.eulerAngles;
        bodyRotation.y += mouseX;

        transform.localRotation = Quaternion.Euler(bodyRotation);
    }

    private void PitchCamera()
    {
        mouseY = mouseSensitivity * lookDirection.y * Time.deltaTime;

        cameraPitch.x -= mouseY;
        cameraPitch.x = Mathf.Clamp(cameraPitch.x, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch);
    }

    private void OnJump()
    {
        Debug.Log("JUMPED!");
    }

    private void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
        moveDirection = new Vector3(input.x, 0f, input.y);
    }

    private void OnLook(InputValue inputValue)
    {
        lookDirection = inputValue.Get<Vector2>();
    }
}
