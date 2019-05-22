using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// Это заготовка кода под Character Controller, решил не соединять его с джойстиками,
//а использовал готовый код из асета.


public class PlayerCtrl : MonoBehaviour {

float speed, jumpForce, gravity, sensitivity;
    GameObject player;
    CharacterController controller;
    Vector3 moveDirection, cameraRotation;
    Transform cameraTransform;
    GameObject[] craftMaterials;
        private void Awake()
        {
            
            speed = 7.5f;
            jumpForce = 1;
            gravity = 2;
            sensitivity = 50;
            if (Camera.main != null)
                cameraTransform = Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
            controller = GameObject.Find("Player").GetComponent<CharacterController>();
            player = GameObject.FindWithTag("Player");
        }

    private void Update()
    {
        MouseRotation();
        InputHandler();
    }

    public void MouseRotation()
        {
            float sens = sensitivity * Time.deltaTime;
            float mouseX = Input.GetAxis("Mouse X") * sens;
            player.transform.Rotate(Vector3.up * mouseX);
            float mouseY = Input.GetAxis("Mouse Y") * sens;
            cameraRotation.x -= mouseY;
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, -65, 65);
            cameraTransform.localEulerAngles = cameraRotation;
        }

        public void InputHandler()
        {
            if (controller.isGrounded)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                moveDirection = new Vector3(h, 0, v);
                moveDirection = player.transform.TransformDirection(moveDirection);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveDirection.y = jumpForce;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            
        }
        private void OnTriggerExit(Collider other)
        {
            
        }
}
