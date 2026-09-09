
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputActions : MonoBehaviour
{

  private InputAction lookAction;

  private Vector2 lookVector;
  private CinemachineFollow cinemachineFollow;


  private void Awake()
  {
    lookAction = InputSystem.actions.FindAction("Look");
    cinemachineFollow = GetComponent<CinemachineFollow>();
  }

  private void Update()
  {
    lookVector = lookAction.ReadValue<Vector2>();
  }

  private void FixedUpdate()
  {
    if (cinemachineFollow != null)
    {
      var xPosition = lookVector.x < -1 ? -1 : lookVector.x > 1 ? 1 : lookVector.x;
      var yPosition = lookVector.y < -1 ? -1 : lookVector.y > 1 ? 1 : lookVector.y;

      var newVector = Vector3.Lerp(
        cinemachineFollow.FollowOffset,
        new Vector3(
          0 + (xPosition * 2.5f),
          12,
          -10 + (yPosition * 3f)),
        Time.deltaTime * 5f
      );
      cinemachineFollow.FollowOffset = newVector;
    }
  }

}
