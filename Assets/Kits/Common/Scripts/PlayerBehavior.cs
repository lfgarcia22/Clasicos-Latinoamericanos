using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{

  [SerializeField] private EventInteractSO eventInteract;
  [SerializeField] private EventSkillSO eventSkill;


  private InputAction skillAction;


  private void Awake()
  {
    skillAction = InputSystem.actions.FindAction("Skill");
  }

  private void Update()
  {
    if (skillAction.WasPressedThisFrame())
    {
      eventSkill?.CompleteAction();
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    eventInteract?.IsInteractable();
  }

  private void OnTriggerExit(Collider other)
  {
    eventInteract?.IsInteractable();
  }

}
