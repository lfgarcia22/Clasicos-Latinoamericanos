using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSoundManager", menuName = "VON - Custom Assets/Event - Sound Manager")]
public class EventSoundManagerSO : ScriptableObject
{

  private Action<bool> onEnableDisableSoundAction;

  public void EnableDisableSound(bool isEnabled)
  {
    PlayerPrefs.SetInt("SoundEnabled", isEnabled ? 1 : 0);
    onEnableDisableSoundAction?.Invoke(isEnabled);
  }

  public void RegisterEnableDisableSound(Action<bool> listener)
  {
    onEnableDisableSoundAction += listener;
  }

  public void UnregisterEnableDisableSound(Action<bool> listener)
  {
    onEnableDisableSoundAction -= listener;
  }

}
