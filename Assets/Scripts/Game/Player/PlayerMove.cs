using UnityEngine;
using SpellStone.Managers;

namespace SpellStone.Player
{
  public class PlayerMove : MonoBehaviour
  {
    private CharacterController characterController;
    private float speed = 20.0f;
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

      Vector3 moveVector = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;

      // Perform raycast to check for obstacles in the way
      RaycastHit hit;
      if (!Physics.Raycast(transform.position, moveVector, out hit, moveVector.magnitude, environmentLayer))
      {
        // If no obstacles, move the player using CharacterController
        characterController.Move(moveVector);
      }
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
