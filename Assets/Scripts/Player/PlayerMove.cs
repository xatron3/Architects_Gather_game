using UnityEngine;
using SpellStone.Managers;

namespace SpellStone.Player
{
  public class PlayerMove : MonoBehaviour
  {
    private float speed = 6.0f;

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
      transform.position = transform.position + moveVector;
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