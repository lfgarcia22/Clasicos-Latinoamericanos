using UnityEngine;
using UnityEngine.UI;

public class InputActionSkill : MonoBehaviour
{

  [SerializeField] private EventSkillSO skillSO;


  private bool isEnabled = false;
  private Button buttonComponent;
  private RectTransform rectTransform;


  private void Awake()
  {
    buttonComponent = GetComponent<Button>();
    buttonComponent.interactable = false;
    rectTransform = GetComponent<RectTransform>();
  }


  private void OnEnable()
  {
    skillSO?.RegisterIsVisible(IsVisibleEvent);
    skillSO?.RegisterIsEnabled(IsEnabledEvent);
  }

  private void OnDisable()
  {
    skillSO?.UnregisterIsVisible(IsVisibleEvent);
    skillSO?.UnregisterIsEnabled(IsEnabledEvent);
  }


  private void IsVisibleEvent(bool value)
  {
    rectTransform.transform.localScale = value ? Vector3.one : Vector3.zero;
  }


  private void IsEnabledEvent(bool value)
  {
    isEnabled = !isEnabled;
    buttonComponent.interactable = isEnabled;
  }

}
