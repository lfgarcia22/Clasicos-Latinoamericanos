using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventInteract", menuName = "VON - Custom Assets/Event - Interact")]
public class EventInteractSO : ScriptableObject
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
