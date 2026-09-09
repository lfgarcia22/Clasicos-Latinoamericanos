using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CanvasSafeArea : MonoBehaviour
{

  private RectTransform rectTransform;
  private Rect lastSafeArea = Rect.zero;
  private Vector2 lastScreenSize = Vector2.zero;
  private ScreenOrientation lastOrientation = ScreenOrientation.Portrait;


  private void Awake()
  {
    rectTransform = GetComponent<RectTransform>();
    RefreshSafeArea();
  }


  private void Update()
  {
    if (Screen.safeArea != lastSafeArea ||
            new Vector2(Screen.width, Screen.height) != lastScreenSize ||
            Screen.orientation != lastOrientation)
    {
      RefreshSafeArea();
    }
  }


  private void RefreshSafeArea()
  {
    Rect safeArea = Screen.safeArea;

    lastSafeArea = safeArea;
    lastScreenSize = new Vector2(Screen.width, Screen.height);
    lastOrientation = Screen.orientation;

    Vector2 anchorMin = safeArea.position;
    Vector2 anchorMax = safeArea.position + safeArea.size;

    anchorMin.x /= Screen.width;
    anchorMin.y /= Screen.height;
    anchorMax.x /= Screen.width;
    anchorMax.y /= Screen.height;

    rectTransform.anchorMin = anchorMin;
    rectTransform.anchorMax = anchorMax;
  }

}
