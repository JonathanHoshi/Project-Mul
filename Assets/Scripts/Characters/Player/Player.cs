using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovementSystem
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public PlayerInput Input { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            Input = GetComponent<PlayerInput>();
            Rigidbody = GetComponent<Rigidbody>();

            MainCameraTransform = Camera.main.transform;

            movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}
