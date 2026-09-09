using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventMove", menuName = "VON - Custom Assets/Event - Move")]
public class EventMoveSO : ScriptableObject
{

  private Action onMovable;

  public void IsMovable()
  {
    onMovable?.Invoke();
  }

  public void RegisterMovable(Action listener)
  {
    onMovable += listener;
  }

  public void UnregisterMovable(Action listener)
  {
    onMovable -= listener;
  }

}
