using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSkill", menuName = "VON - Custom Assets/Event - Skill")]
public class EventSkillSO : ScriptableObject
{

  private Action onInteractable;

  public void IsInteractable()
  {
    onInteractable?.Invoke();
  }

  public void RegisterInteractable(Action listener)
  {
    onInteractable += listener;
  }

  public void UnregisterInteractable(Action listener)
  {
    onInteractable -= listener;
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
