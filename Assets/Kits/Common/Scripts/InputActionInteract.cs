using UnityEngine;
using UnityEngine.UI;

public class InputActionInteract : MonoBehaviour
{

  [SerializeField] private EventInteractSO eventInteract;


  private bool isEnabled = false;
  private Button buttonComponent;


  private void Awake()
  {
    buttonComponent = GetComponent<Button>();
    buttonComponent.interactable = false;
  }


  private void OnEnable()
  {
    eventInteract?.RegisterInteractable(InteractEvent);
  }

  private void OnDisable()
  {
    eventInteract?.UnregisterInteractable(InteractEvent);
  }


  private void InteractEvent()
  {
    isEnabled = !isEnabled;
    buttonComponent.interactable = isEnabled;
  }

}
