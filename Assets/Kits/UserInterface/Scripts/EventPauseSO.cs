using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventPause", menuName = "VON - Custom Assets/Event - Pause")]
public class EventPauseSO : ScriptableObject
{

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
