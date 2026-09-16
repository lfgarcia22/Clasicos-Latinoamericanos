using NaughtyAttributes;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{

  [Label("Event SO - Sound Manager")]
  [SerializeField]
  private EventSoundManagerSO soundManagerSO;

  [Label("Enable Sound Button GameObject")]
  [SerializeField]
  private GameObject enableButton;

  [Label("Disable Sound Button GameObject")]
  [SerializeField]
  private GameObject disableButton;


  private void Awake()
  {
    EnableDisableSoundGameObject();
  }


  /// <summary>
  /// Allow the player to enable the sound of the game.
  /// </summary>
  public void EnableSound()
  {
    soundManagerSO?.EnableDisableSound(true);
    EnableDisableSoundGameObject();
  }


  /// <summary>
  /// Allow the player to disable the sound of the game.
  /// </summary>
  public void DisableSound()
  {
    soundManagerSO?.EnableDisableSound(false);
    EnableDisableSoundGameObject();
  }


  /// <summary>
  /// Manage buttons (gameobjects) to show/hide based on player preferences.
  /// </summary>
  private void EnableDisableSoundGameObject()
  {
    var isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled") == 1;
    enableButton.SetActive(!isSoundEnabled);
    disableButton.SetActive(isSoundEnabled);
  }
}
