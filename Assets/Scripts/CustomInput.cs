using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

    public class CustomInput : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public Vector2 mousePos;

        public bool jump;
        public bool leftMouseClicked;
        public bool rightMouseClicked;
        public bool sprint;

        [Header("Mouse Control Values")]
        [Range(0.0f, 2.0f)]
        public float mouseSentivity;
        public Vector2 mouseMultiplier;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [Header("Aim Settings")]
        public GameObject AimObject;
        public float MaxAimpointDistance;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnMousePos(InputValue value)
        {
            MousePosInput(value.Get<Vector2>());
        }

        public void OnLeftClick(InputValue value)
        {
            LeftClicked(value.isPressed);
        }

        public void OnRightClick(InputValue value)
        {
            RightClicked(value.isPressed);
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void MousePosInput(Vector2 newMousePos)
        {
            mousePos = newMousePos;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void LeftClicked(bool newClickState)
        {
            leftMouseClicked = newClickState;
        }

        public void RightClicked(bool newClickState)
        {
            rightMouseClicked = newClickState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }