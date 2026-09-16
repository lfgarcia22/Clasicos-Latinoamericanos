using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSkill", menuName = "VON - Custom Assets/Event - Skill")]
public class EventSkillSO : ScriptableObject
{

  private Action<bool> isVisible;

  public void IsVisible(bool value)
  {
    isVisible?.Invoke(value);
  }

  public void RegisterIsVisible(Action<bool> listener)
  {
    isVisible += listener;
  }

  public void UnregisterIsVisible(Action<bool> listener)
  {
    isVisible -= listener;
  }


  private Action<bool> isEnabled;

  public void IsEnabled(bool value)
  {
    isEnabled?.Invoke(value);
  }

  public void RegisterIsEnabled(Action<bool> listener)
  {
    isEnabled += listener;
  }

  public void UnregisterIsEnabled(Action<bool> listener)
  {
    isEnabled -= listener;
  }


  private Action onCompleteAction;

  public void CompleteAction()
  {
    onCompleteAction?.Invoke();
  }

  public void RegisterCompleteAction(Action listener)
  {
    onCompleteAction += listener;
  }

  public void UnregisterCompleteAction(Action listener)
  {
    onCompleteAction -= listener;
  }

}
