using UnityEngine;
using UnityEngine.UI;

public class InputActionSkill : MonoBehaviour
{

  [SerializeField] private EventSkillSO eventSkill;


  private bool isEnabled = false;
  private Button buttonComponent;


  private void Awake()
  {
    buttonComponent = GetComponent<Button>();
    buttonComponent.interactable = false;
  }


  private void OnEnable()
  {
    eventSkill?.RegisterInteractable(InteractEvent);
  }

  private void OnDisable()
  {
    eventSkill?.UnregisterInteractable(InteractEvent);
  }

  private void InteractEvent()
  {
    isEnabled = !isEnabled;
    buttonComponent.interactable = isEnabled;
  }

}
