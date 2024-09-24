using System;
using UnityEngine;

namespace _Scripts
{
    public class CharacterControll : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 5f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private Transform cam;

        private PlayerControls playerControls;
        private Rigidbody rb;
        private MovementModifier movementModifier;
        private Vector3 minInAirSpeed;
        private CharacterCollisionManager characterCollisionManager;
        private Animator animator;

        private int isSlowRunningHash;
        private int isFastRunningHash;
        private int jump;

        private void Awake()
        {
            playerControls = new PlayerControls();
            rb = GetComponent<Rigidbody>();
            movementModifier = GetComponent<MovementModifier>();
            characterCollisionManager = GetComponent<CharacterCollisionManager>();
            animator = GetComponent<Animator>();

            isSlowRunningHash = Animator.StringToHash("isSlowRunning");
            isFastRunningHash = Animator.StringToHash("isFastRunning");
            jump = Animator.StringToHash("jump");
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            var camForward = cam.forward;
            var camRight = cam.right;

            var move = playerControls.Land.Move.ReadValue<Vector2>().normalized;

            var forwardRelative = move.y * camForward;
            var rightRelative = move.x * camRight;

            var moveDir = forwardRelative + rightRelative;
            var runningModifier = playerControls.Land.Run.ReadValue<float>() + 1;

            //handle jump
            if (playerControls.Land.Jump.triggered && characterCollisionManager.GroundCollisions > 0)
            {
                SetVelocityAccountingMovementModifier(new Vector3(rb.velocity.x, 1 * jumpHeight, rb.velocity.z));
                minInAirSpeed = rb.velocity;
                animator.SetTrigger(jump);
            }

            if (characterCollisionManager.GroundCollisions == 0)
                movementModifier.SetInAirMovement(new Vector3(moveDir.x * speed,
                    0,
                    moveDir.z * speed) / 2);
            else
                movementModifier.SetInAirMovement(Vector3.zero);

            //handle running
            Run(moveDir, runningModifier);
        }

        private void Run(Vector3 moveDir, float runningModifier)
        {
            if (moveDir != Vector3.zero)
            {
                var rotation =
                    Quaternion.LookRotation(
                        new Vector3(moveDir.x, 0, moveDir.z),
                        transform.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * runningModifier / 5);

                SetVelocityAccountingMovementModifier(new Vector3(moveDir.x * speed * runningModifier,
                    rb.velocity.y,
                    moveDir.z * speed * runningModifier));

                if (runningModifier > 1)
                {
                    animator.SetBool(isSlowRunningHash, false);
                    animator.SetBool(isFastRunningHash, true);
                }
                else
                {
                    animator.SetBool(isFastRunningHash, false);
                    animator.SetBool(isSlowRunningHash, true);
                }
            }
            else
            {
                SetVelocityAccountingMovementModifier(new Vector3(moveDir.x * speed * runningModifier,
                    rb.velocity.y,
                    moveDir.z * speed * runningModifier));
                animator.SetBool(isFastRunningHash, false);
                animator.SetBool(isSlowRunningHash, false);
            }
        }

        private void SetVelocityAccountingMovementModifier(Vector3 velocity)
        {
            rb.velocity = velocity + movementModifier.MovementModificatorContainer.InnerVector +
                          movementModifier.InAirMovement;
        }
    }
}