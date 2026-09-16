using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class CantunitaStoryManager : MonoBehaviour
{

  [Header("Start settings")]
  [Label("Start Button GameObject")]
  [SerializeField]
  private GameObject startButton;

  [Label("Replay Button GameObject")]
  [SerializeField]
  private GameObject replayButton;

  [Label("Event SO - Sound Manager")]
  [SerializeField]
  private EventSoundManagerSO soundManagerSO;

  [Label("Enable Sound Button GameObject")]
  [SerializeField]
  private GameObject enableButton;

  [Label("Disable Sound Button GameObject")]
  [SerializeField]
  private GameObject disableButton;

  [Header("Main Discoverable")]
  [Label("Story Dialog GameObject")]
  [SerializeField]
  private GameObject storyDialog;

  [Label("Story Dialog continue GameObject")]
  [SerializeField]
  private GameObject storyDialogContinue;

  [Label("Velocity to show dialog")]
  [SerializeField]
  private float storyDialogShowVelocity = 5f;

  [Label("GameObject to load text")]
  [SerializeField]
  private TextMeshProUGUI storyDialogText;

  [Label("Paragraphs of the story")]
  [ReorderableList]
  [SerializeField]
  private List<string> storyParagraphs;

  [Label("Writing speed")]
  [SerializeField]
  private float writingSpeed = 0.05f;


  private RectTransform storyDialogTransform;
  private bool isDialogShowing = false;
  private bool isDialogHiding = false;
  private int paragraphIdx = 0;
  private bool isWriting = false;


  private void Awake()
  {
    var isStoryRead = PlayerPrefs.GetInt("StoryRead") == 1;
    if (isStoryRead)
    {
      startButton.SetActive(true);
      replayButton.SetActive(true);
    }
    else
    {
      InitStoryDialog();
    }

    EnableDisableSoundGameObject();
  }


  private void Update()
  {
    MoveDialogToFinalPosition();
    MoveDialogToStartPosition();
  }


  /// <summary>
  /// Start all logic for showing the dialog with the story to tell.
  /// </summary>
  public void InitStoryDialog()
  {
    replayButton.SetActive(false);
    storyDialog.SetActive(true);
    paragraphIdx = 0;
    storyDialogText.text = "";
    storyDialogTransform = storyDialog.GetComponent<RectTransform>();
    storyDialogTransform.anchoredPosition = new Vector2(storyDialogTransform.anchoredPosition.x, -840f);
    isDialogShowing = true;
  }


  /// <summary>
  /// Move the dialog from outside the device to the bottom of the screen.
  /// </summary>
  private void MoveDialogToFinalPosition()
  {
    if (isDialogShowing)
    {
      Vector2 endPosition = new Vector2(storyDialogTransform.anchoredPosition.x, 40);
      storyDialogTransform.anchoredPosition = Vector2.Lerp(storyDialogTransform.anchoredPosition, endPosition, Time.deltaTime * storyDialogShowVelocity);
      if (storyDialogTransform.anchoredPosition.y > 39)
      {
        isDialogShowing = false;
        StartCoroutine(StartWritingParagraph());
      }
    }
  }

  /// <summary>
  /// Move the dialog from the bottom of the screen to outside of the device.
  /// </summary>
  private void MoveDialogToStartPosition()
  {
    if (isDialogHiding)
    {
      Vector2 endPosition = new Vector2(storyDialogTransform.anchoredPosition.x, -860);
      storyDialogTransform.anchoredPosition = Vector2.Lerp(storyDialogTransform.anchoredPosition, endPosition, Time.deltaTime * storyDialogShowVelocity);
      if (storyDialogTransform.anchoredPosition.y < -859)
      {
        isDialogHiding = false;
        startButton.SetActive(true);
        replayButton.SetActive(true);
      }
    }
  }


  /// <summary>
  /// Coroutine to start writing the story paragraphs with a typing machine style.
  /// </summary>
  /// <returns></returns>
  private IEnumerator StartWritingParagraph()
  {
    storyDialogContinue.SetActive(false);
    isWriting = true;
    storyDialogText.text = "";
    foreach (char letter in storyParagraphs[paragraphIdx].ToCharArray())
    {
      storyDialogText.text += letter;
      yield return new WaitForSeconds(writingSpeed);
    }
    isWriting = false;
    storyDialogContinue.SetActive(true);
  }


  /// <summary>
  /// Method to be called from the start button to continue with the game scene.
  /// </summary>
  public void ContinueToGame()
  {
    Addressables.InitializeAsync().Completed += (handle) =>
    {
      Addressables.LoadSceneAsync("Cantunita_Scenes/CantunitaBuild.unity", LoadSceneMode.Single);
    };
  }


  /// <summary>
  /// Method to be called from dialog box button to continue to the next paragraph.
  /// </summary>
  public void ContinueNextParagraph()
  {
    if (!isDialogShowing && isWriting)
    {
      StopAllCoroutines();
      storyDialogText.text = storyParagraphs[paragraphIdx];
      isWriting = false;
      storyDialogContinue.SetActive(true);
    }
    else if (!isDialogShowing && !isWriting)
    {
      paragraphIdx++;
      if (paragraphIdx < storyParagraphs.Count)
      {
        StartCoroutine(StartWritingParagraph());
      }
      else
      {
        PlayerPrefs.SetInt("StoryRead", 1);
        isDialogHiding = true;
      }
    }
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
