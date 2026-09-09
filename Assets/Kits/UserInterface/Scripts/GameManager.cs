using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

  [SerializeField] private InputActionAsset inputActions;
  [SerializeField] private EventPauseSO eventPause;


  private InputAction pauseAction;


  private void Awake()
  {
    pauseAction = InputSystem.actions.FindAction("Pause");
  }

  private void Update()
  {
    if (pauseAction.WasPressedThisFrame())
    {
      eventPause?.CompleteAction();
    }
  }


  private void OnEnable()
  {
    inputActions.FindActionMap("Player").Enable();
  }

  private void OnDisable()
  {
    inputActions.FindActionMap("Player").Disable();
  }

}
