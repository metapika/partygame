using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore
{

    namespace Player
    {

        public class WordsBlockCharacter : MonoBehaviour
        {
            #region Fields
            [SerializeField] private float speed = 10f;
            [SerializeField] private float speedSmoothTime = 0.1f;
            [SerializeField] private float jumpRaycastDistance = 1f;

            [SerializeField] private Vector3 velocity;
            [SerializeField] private float gravity = -9.81f;

            private Animator anim = null;
            private CharacterController controller = null;
            private float speedSmoothVelocity = 0f;
            private float currentSpeed = 0f;

            private static readonly int hashSpeedPercentage = Animator.StringToHash("SpeedPercentage");

            #endregion

            #region Private Functions

            private void Awake()
            {
                controller = GetComponent<CharacterController>();
                anim = GetComponent<Animator>();
            }

            private void Update()
            {
                Move();
            }

            private void Move()
            {
                Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

                Vector3 desiredMoveDirection = (Vector3.forward * movementInput.y + Vector3.right * movementInput.x).normalized;
                
                if (desiredMoveDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
                }

                float targetSpeed = speed * movementInput.magnitude;
                currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

                if (IsGrounded() && velocity.y < 0)
                {
                    velocity.y = 0f;
                }

                controller.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);

                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);

                anim.SetFloat(hashSpeedPercentage, 1f * movementInput.magnitude, speedSmoothTime, Time.deltaTime);
            }

            private bool IsGrounded()
            {
                return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);
            }
            #endregion
        }
    }
}
