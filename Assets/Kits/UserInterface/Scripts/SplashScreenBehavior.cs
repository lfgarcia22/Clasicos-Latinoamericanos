using System.Collections;
using Firebase;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenBehavior : MonoBehaviour
{

  [Header("Next scene info")]
  [SerializeField] private bool continueToScene = true;
  [SerializeField] private string sceneName;
  [SerializeField] private float waitToNextScene = 2.5f;

  [Header("Loading settings")]
  [SerializeField] private Slider slider;
  [SerializeField] private float waitSeconds = 0.1f;

  [Header("Other settings")]
  [SerializeField] private bool triggerCrash = false;


  private void Awake()
  {
    StartCoroutine(StartLoadingScene());
    StartCoroutine(TestCrash());
    InitFirebase();
  }


  /// <summary>
  /// Start all Firebase settings, this will check and fix dependencies
  /// </summary>
  private void InitFirebase()
  {
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    {
      var dependencyStatus = task.Result;
      if (dependencyStatus == DependencyStatus.Available)
      {
        var fireApp = FirebaseApp.DefaultInstance;

        StartCoroutine(MoveToFirstScene());
      }
      else
      {
        Debug.LogError($"[FIREBASE] No se pudieron resolver las dependencias de Firebase: {dependencyStatus}");
      }
    });
  }


  private float progress = 0.0f;

  /// <summary>
  /// Start a loading indicator before moving to main scene configured in the editor.
  /// </summary>
  /// <returns></returns>
  private IEnumerator StartLoadingScene()
  {
    while (true)
    {
      if (slider != null)
      {
        float currentValue = Mathf.Clamp01(progress / 0.9f);
        slider.value = currentValue;
      }

      if (progress > 1)
      {
        progress = 0;
      }
      else
      {
        progress += 0.01f;
      }

      yield return new WaitForSeconds(waitSeconds);
    }
  }


  /// <summary>
  /// Wait N seconds configured in the editor before moving to the scene configured.
  /// </summary>
  /// <returns></returns>
  private IEnumerator MoveToFirstScene()
  {
    yield return new WaitForSeconds(waitToNextScene);
    Debug.Log("Move to next scene");
    InitializeAddressables();
    yield return null;
  }


  /// <summary>
  /// Load the addressable scene.
  /// </summary>
  private void InitializeAddressables()
  {
    Addressables.InitializeAsync().Completed += (handle) =>
    {
      if (continueToScene)
      {
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
      }
    };
  }


  /// <summary>
  /// Allows to test a crash into Firebase.
  /// </summary>
  /// <returns></returns>
  private IEnumerator TestCrash()
  {
    if (triggerCrash)
    {
      UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.AccessViolation);
    }
    yield return null;
  }

}
