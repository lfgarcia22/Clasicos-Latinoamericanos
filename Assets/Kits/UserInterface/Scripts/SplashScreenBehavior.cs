using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.AppCheck;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenBehavior : MonoBehaviour
{

  [Header("Firebase setup")]
  [SerializeField] private string bucketUrl = "gs://://appspot.com";

  [Header("Next scene info")]
  [SerializeField] private bool continueToScene = true;
  [SerializeField] private string sceneName;

  [Header("Loading section")]
  [SerializeField] private Slider slider;
  [SerializeField] private float waitSeconds = 0.1f;
  private float progress = 0.0f;


  private const string storageKey = "storage://";
  private Dictionary<string, string> urlCache = new Dictionary<string, string>();
  private FirebaseApp fireApp;
  private FirebaseStorage fireStorage;


  private void Awake()
  {
    StartCoroutine(StartLoadingScene());
    InitFirebase();
    Addressables.WebRequestOverride = EditWebRequestURL;
  }


  private void InitFirebase()
  {
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    {
      var dependencyStatus = task.Result;
      if (dependencyStatus == DependencyStatus.Available)
      {
        // InitFirebaseAppCheck();
        fireApp = FirebaseApp.DefaultInstance;

        fireStorage = FirebaseStorage.DefaultInstance;
        Addressables.ResourceManager.InternalIdTransformFunc = TransformAddressablesId;
        InitializeAddressables();
      }
      else
      {
        Debug.LogError($"[FIREBASE] No se pudieron resolver las dependencias de Firebase: {dependencyStatus}");
      }
    });
  }


  private void InitFirebaseAppCheck()
  {
    // #if UNITY_EDITOR
    // Usar el proveedor de depuración en el Editor de Unity
    FirebaseAppCheck.SetAppCheckProviderFactory(DebugAppCheckProviderFactory.Instance);
    // #else
    // Usar Play Integrity en el dispositivo Android real
    // FirebaseAppCheck.SetAppCheckProviderFactory(PlayIntegrityProviderFactory.Instance);
    // PlayIntegrityAppCheckProviderFactory.Instance
    // #endif
  }


  private string TransformAddressablesId(IResourceLocation location)
  {
    string path = location.InternalId.Replace(storageKey, "");

    if (location.InternalId.StartsWith(storageKey))
    {
      if (urlCache.TryGetValue(location.InternalId, out string realUrl))
      {
        return realUrl;
      }
      Debug.LogWarning($"[ADDRESSABLES] La URL {location.InternalId} no estaba en caché. Intentando resolver de emergencia.");
    }
    return path;
  }


  private void InitializeAddressables()
  {
    Addressables.InitializeAsync().Completed += (handle) =>
    {
      PrecacheFirebaseUrls(handle.Result);
    };
  }

  private async void PrecacheFirebaseUrls(IResourceLocator runtimeData)
  {
    List<Task> precacheTasks = new List<Task>();

    foreach (var location in Addressables.ResourceLocators)
    {
      foreach (var key in location.Keys)
      {
        IList<IResourceLocation> locations;
        if (location.Locate(key, typeof(object), out locations))
        {
          foreach (var loc in locations)
          {
            if (loc.InternalId.StartsWith(storageKey) && !urlCache.ContainsKey(loc.InternalId))
            {
              precacheTasks.Add(FetchAndCacheUrl(loc.InternalId));
            }
          }
        }
      }
    }

    await Task.WhenAll(precacheTasks);
    Debug.Log($"[ADDRESSABLES] Precarga completa. {urlCache.Count} URLs cacheadas de Firebase.");

    Debug.Log($"[ADDRESSABLES] Navegación a la primera escena.");
    if (continueToScene)
    {
      Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
  }

  private async Task FetchAndCacheUrl(string internalId)
  {
    try
    {
      string relativePath = internalId.Replace(storageKey, "");
      string path = $"{bucketUrl}/{relativePath}";
      StorageReference reference = fireStorage.GetReferenceFromUrl(path);
      Uri downloadUri = await reference.GetDownloadUrlAsync();
      urlCache[internalId] = downloadUri.ToString();
    }
    catch (Exception e)
    {
      Debug.LogError($"[FIREBASE] Error al precargar {internalId}: {e.Message}");
    }
  }


  private void EditWebRequestURL(UnityWebRequest request)
  {
    if (request.url.Contains("googleapis.com"))
    {
      string url = request.url;

      if (url.Contains("/o/"))
      {
        int indexO = url.IndexOf("/o/") + 3;
        int indexQuery = url.IndexOf("?");

        string basePart = url.Substring(0, indexO);
        string pathPart = indexQuery >= 0 ? url.Substring(indexO, indexQuery - indexO) : url.Substring(indexO);
        pathPart = pathPart.Replace("/", "%2F");
        string queryPart = indexQuery >= 0 ? url.Substring(indexQuery) : "";

        request.url = basePart + pathPart + queryPart;
      }
    }
  }


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

}
