using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class InputActionMove : MonoBehaviour
{

  [SerializeField] private GameObject playerWrapper;

  [SerializeField] private GameObject player;

  [SerializeField] private float walkSpeed = 5;

  [SerializeField] private EventMoveSO moveSO;

  [SerializeField] private EventSkillSO skillSO;


  private bool allowToMove = true;
  private OnScreenStick childStickController;
  private InputAction moveAction;
  private Vector3 moveVector;
  private Animator animator;
  private float previousWalkSpeed;


  private void Awake()
  {
    moveAction = InputSystem.actions.FindAction("Move");
    childStickController = GetComponentInChildren<OnScreenStick>();
    animator = playerWrapper?.GetComponentInChildren<Animator>();
    previousWalkSpeed = walkSpeed + 0;
  }


  private void Update()
  {
    var moveControllerVector = moveAction.ReadValue<Vector2>();
    moveVector = new Vector3(moveControllerVector.x, 0.0f, moveControllerVector.y);
  }

  private void FixedUpdate()
  {
    if (allowToMove)
    {
      if (playerWrapper != null)
      {
        playerWrapper.transform.Translate(Vector3.right * moveVector.x * walkSpeed * Time.deltaTime, Space.World);
        playerWrapper.transform.Translate(Vector3.forward * moveVector.z * walkSpeed * Time.deltaTime, Space.World);
      }

      var isMoving = moveVector.x != 0 || moveVector.z != 0;
      animator.SetBool("IsWalking", isMoving);
      if (isMoving)
      {
        player.transform.LookAt(new Vector3(
          playerWrapper.transform.localPosition.x + (moveVector.x * 4),
          moveVector.y,
          playerWrapper.transform.localPosition.z + (moveVector.z * 4)
        ));
      }
    }
  }


  private void OnEnable()
  {
    moveSO?.RegisterMovable(MovableEvent);
    skillSO?.RegisterCompleteAction(CompleteSkillEvent);
  }

  private void OnDisable()
  {
    moveSO?.UnregisterMovable(MovableEvent);
    skillSO?.UnregisterCompleteAction(CompleteSkillEvent);
  }


  private void MovableEvent()
  {
    allowToMove = !allowToMove;
    childStickController.enabled = allowToMove;
  }


  private void CompleteSkillEvent()
  {
    StopCoroutine(WaitBeforeReduceSpeed());
    walkSpeed *= 2;
    StartCoroutine(WaitBeforeReduceSpeed());
  }

  private IEnumerator WaitBeforeReduceSpeed()
  {
    yield return new WaitForSeconds(3);
    walkSpeed = previousWalkSpeed + 0;
  }

}
