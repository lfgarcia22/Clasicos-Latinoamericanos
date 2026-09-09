using UnityEngine;

public class HideableAction : MonoBehaviour
{

  [SerializeField] private EventInteractSO eventInteract;


  private bool isActionable = false;


  private void OnEnable()
  {
    eventInteract?.RegisterCompleteAction(CompleteActionEvent);
  }

  private void OnDisable()
  {
    eventInteract?.UnregisterCompleteAction(CompleteActionEvent);
  }


  private void OnTriggerEnter(Collider other)
  {
    isActionable = true;
  }

  private void OnTriggerExit(Collider other)
  {
    isActionable = false;
  }


  private void CompleteActionEvent()
  {
    if (isActionable)
    {
      eventInteract.IsInteractable();
      Destroy(gameObject);
    }
  }

}
