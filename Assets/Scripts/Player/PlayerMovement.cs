using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 6.0f;

        private float camRayLength = 100.0f;
        private Vector3 movement;
        private int floorMask;
        private Rigidbody myRigidbody;
        private Animator myAnimator;

        private void Awake()
        {
            floorMask = LayerMask.GetMask("Floor");
            myRigidbody = GetComponent<Rigidbody>();
            myAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");

            Move(hInput, vInput);
            Rotate();
            Animate(hInput, vInput);
        }

        private void Move(float h, float v)
        {
            movement.Set(h, 0.0f, v);
            // Normalizing movement makes the player to move with same speed diagonally
            movement = movement.normalized * speed * Time.deltaTime;

            myRigidbody.MovePosition(transform.position + movement);
        }

        private void Rotate()
        {
            // TODO: We should not access Camera.main directly (take camera transform? or tags?)
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); // Point below mouse cursor

            // Tries to hit only things at floor layer
            if (Physics.Raycast(camRay, out RaycastHit floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0.0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                myRigidbody.MoveRotation(newRotation);
            }
        }

        private void Animate(float h, float v)
        {
            myAnimator.SetBool("IsWalking", IsWalking(h, v));
        }

        private bool IsWalking(float h, float v)
        {
            return (h != 0.0f || v != 0.0f);
        }
    }
}
