using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameTimerManager : MonoBehaviour
{
  [Label("Time to play (in minutes)")]
  [SerializeField] private float gameOverTimer = 2f;

  [SerializeField] private GameObject gameOverDialog;


  private TextMeshProUGUI timerText;

  private float timeInSeconds = 0;
  private float remainingTime = 0;


  private void Awake()
  {
    timerText = GetComponent<TextMeshProUGUI>();
    timeInSeconds = gameOverTimer * 60;
    remainingTime = timeInSeconds + 0;
  }


  void Update()
  {
    if (Time.timeScale != 0)
    {
      UpdateTimerText();
      remainingTime -= Time.deltaTime;
      ManageEndGame();
    }
  }


  private void UpdateTimerText()
  {
    var minutes = Mathf.FloorToInt(remainingTime / 60);
    var seconds = Mathf.FloorToInt(remainingTime % 60);
    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
  }

  private void ManageEndGame()
  {
    if (remainingTime < 1)
    {
      Time.timeScale = 0;
      gameOverDialog.SetActive(true);
    }
  }
}
