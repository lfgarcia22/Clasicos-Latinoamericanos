using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{

  [SerializeField] private InputActionAsset inputActions;

  [SerializeField] private string currentScene;

  [SerializeField] private string homeScene;


  private InputAction restartAction;
  private InputAction homeAction;


  private void Awake()
  {
    restartAction = InputSystem.actions.FindAction("Restart");
    homeAction = InputSystem.actions.FindAction("Home");
  }


  private void Update()
  {
    if (Time.timeScale == 0 && restartAction.WasPressedThisFrame())
    {
      Addressables.InitializeAsync().Completed += (handle) =>
      {
        Addressables.LoadSceneAsync(currentScene, LoadSceneMode.Single);
        Time.timeScale = 1;
      };
    }

    if (Time.timeScale == 0 && homeAction.WasPressedThisFrame())
    {
      Addressables.InitializeAsync().Completed += (handle) =>
      {
        Addressables.LoadSceneAsync(homeScene, LoadSceneMode.Single);
        Time.timeScale = 1;
      };
    }
  }

}
