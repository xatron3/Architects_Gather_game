using UnityEngine;
using SpellStone.Managers;

namespace SpellStone.Player
{
  public class PlayerMove : MonoBehaviour
  {
    private CharacterController characterController;
    private float speed = 20.0f;
    private float gravity = 9.81f; // Gravity value
    private Vector3 moveDirection = Vector3.zero;
    public LayerMask environmentLayer;

    void Start()
    {
      characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
      MovePlayer();
      UpdateCameraTarget();
    }

    private void MovePlayer()
    {
      float moveHorizontal = InputManager.instance.GetHorizontalAxisValue();
      float moveVertical = InputManager.instance.GetVerticalAxisValue();

      Vector3 moveVector = new Vector3(moveHorizontal, 0.0f, moveVertical);

      // Apply gravity
      if (characterController.isGrounded)
      {
        moveDirection = moveVector * speed;
      }
      else
      {
        moveDirection.y -= gravity * Time.deltaTime;
      }

      // Perform raycast to check for obstacles in the way
      RaycastHit hit;
      if (!Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, environmentLayer))
      {
        // If no ground detected, apply additional gravity
        moveDirection.y -= gravity * Time.deltaTime;
      }

      // Move the player using CharacterController
      characterController.Move(moveDirection * Time.deltaTime);
    }

    private void UpdateCameraTarget()
    {
      Camera.CameraFollow cameraFollow = FindObjectOfType<Camera.CameraFollow>();
      if (cameraFollow != null)
      {
        cameraFollow.SetTarget(transform);
      }
      else
      {
        Debug.LogWarning("No CameraFollow script found in the scene.");
      }
    }
  }
}
