using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerMovementSystem
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;

        protected Vector2 movementInput;

        protected float baseSpeed = 5f;
        protected float speedModifier = 1f;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void Update()
        {

        }

        #region Main Methods

        private void ReadMovementInput()
        {
            movementInput = stateMachine.Player.Input.actions["Move"].ReadValue<Vector2>();
        }

        private void Move()
        {
            if (movementInput == Vector2.zero || speedModifier == 0f)
            {
                return;
            }

            Vector3 movementDirection = GetMovementInputDirection();

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayer2DVelocity = GetPlayer2DVelocity();

            stateMachine.Player.Rigidbody.AddForce(movementDirection * movementSpeed - currentPlayer2DVelocity, ForceMode.VelocityChange);
        }

        #endregion

        #region Common Methods

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(movementInput.x, 0, movementInput.y);
        }
        
        protected float GetMovementSpeed()
        {
            return baseSpeed * speedModifier;
        }

        protected Vector3 GetPlayer2DVelocity()
        {
            Vector3 player2DVelocity = stateMachine.Player.Rigidbody.velocity;

            player2DVelocity.y = 0;

            return player2DVelocity;
        }

        #endregion


    }

}