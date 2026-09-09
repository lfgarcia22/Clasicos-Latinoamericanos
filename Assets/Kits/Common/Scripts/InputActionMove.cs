using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class InputActionMove : MonoBehaviour
{

  [SerializeField] private GameObject player;

  [SerializeField] private float walkSpeed = 5;

  [SerializeField] private EventMoveSO eventMove;


  private bool allowToMove = true;
  private OnScreenStick childStickController;
  private InputAction moveAction;
  private Vector3 moveVector;


  private void Awake()
  {
    moveAction = InputSystem.actions.FindAction("Move");
    childStickController = GetComponentInChildren<OnScreenStick>();
  }


  private void Update()
  {
    var moveControllerVector = moveAction.ReadValue<Vector2>();
    moveVector = new Vector3(moveControllerVector.x, 0.0f, moveControllerVector.y);

  }

  private void FixedUpdate()
  {
    if (allowToMove && player != null)
    {
      player.transform.Translate(Vector3.forward * moveVector.z * walkSpeed * Time.deltaTime, Space.World);
      player.transform.Translate(Vector3.right * moveVector.x * walkSpeed * Time.deltaTime, Space.World);
    }
  }


  private void OnEnable()
  {
    eventMove?.RegisterMovable(MovableEvent);
  }

  private void OnDisable()
  {
    eventMove?.UnregisterMovable(MovableEvent);
  }


  private void MovableEvent()
  {
    allowToMove = !allowToMove;
    childStickController.enabled = allowToMove;
  }

}
