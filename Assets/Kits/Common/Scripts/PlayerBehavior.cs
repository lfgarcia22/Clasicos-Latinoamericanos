using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{

  [SerializeField] private EventInteractSO interactSO;

  [SerializeField] private EventSkillSO skillSO;

  [SerializeField] private float skillWaitingTime = 5f;


  private InputAction skillAction;
  private bool skillIsEnabled = false;


  private void Awake()
  {
    skillAction = InputSystem.actions.FindAction("Skill");
    StartCoroutine(WaitForSkillUsage());
  }


  private void Update()
  {
    if (skillIsEnabled && skillAction.WasPressedThisFrame())
    {
      StopCoroutine(WaitForSkillUsage());
      HandleSkillEnable(false);
      skillSO?.CompleteAction();
      StartCoroutine(WaitForSkillUsage());
    }
  }


  private void OnTriggerEnter(Collider other)
  {
    interactSO?.IsInteractable();
  }

  private void OnTriggerExit(Collider other)
  {
    interactSO?.IsInteractable();
  }


  private IEnumerator WaitForSkillUsage()
  {
    yield return new WaitForSeconds(skillWaitingTime);
    HandleSkillEnable(true);
    skillWaitingTime *= 1.5f;
  }

  private void HandleSkillEnable(bool value)
  {
    skillIsEnabled = value;
    skillSO?.IsEnabled(value);
  }

}
