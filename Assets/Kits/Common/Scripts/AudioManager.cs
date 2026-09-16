using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

  public static AudioManager Instance;

  [SerializeField] private EventSoundManagerSO soundManagerSO;

  [SerializeField] private float cutStartSeconds = 1.0f;
  [SerializeField] private float cutEndSeconds = 2.0f;


  private AudioSource audioSource;
  private float audioSize;
  private float initialCut = 0;


  private void Awake()
  {
    audioSource = GetComponent<AudioSource>();
    audioSource.loop = true;
    audioSize = audioSource.clip.length;

    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }


  private void Start()
  {
    StartCoroutine(RepeatLoop());
  }


  private void OnEnable()
  {
    soundManagerSO?.RegisterEnableDisableSound(EnableDisableSound);
  }

  private void OnDisable()
  {
    soundManagerSO?.UnregisterEnableDisableSound(EnableDisableSound);
  }


  /// <summary>
  /// Fix audio clip size based on start and end seconds.
  /// </summary>
  /// <returns></returns>
  private IEnumerator RepeatLoop()
  {
    while (true)
    {
      audioSource.time = initialCut;
      float songSize = audioSize - initialCut - cutEndSeconds;
      yield return new WaitForSeconds(songSize);
      initialCut = cutStartSeconds;
    }
  }

  private void EnableDisableSound(bool isEnabled)
  {
    audioSource.volume = isEnabled ? 1 : 0;
  }

}
