using UnityEngine;

public class InputActionPause : MonoBehaviour
{

  [SerializeField] private EventPauseSO eventPause;
  [SerializeField] private GameObject pauseMenu;
  [SerializeField] private GameObject controlManagement;

  private bool isPaused = false;


  private void OnEnable()
  {
    eventPause?.RegisterCompleteAction(PauseEvent);
  }

  private void OnDisable()
  {
    eventPause?.UnregisterCompleteAction(PauseEvent);
  }


  private void PauseEvent()
  {
    isPaused = !isPaused;
    pauseMenu.SetActive(isPaused);
    controlManagement.SetActive(!isPaused);
    if (isPaused)
    {
      Time.timeScale = 0;
    }
    else
    {
      Time.timeScale = 1;
    }
  }

}
