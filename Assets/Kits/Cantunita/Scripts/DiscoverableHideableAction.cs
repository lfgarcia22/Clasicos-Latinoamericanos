using UnityEngine;
using UnityEngine.InputSystem;

public class DiscoverableHideableActions : MonoBehaviour
{

  [SerializeField] private EventInteractSO interactSO;

  [SerializeField] private EventMoveSO moveSO;

  [SerializeField] private EventSkillSO skillSO;

  [SerializeField] private bool isHideableObject = false;


  private InputAction interactAction;
  private bool isActionable = false;
  private GameObject player;
  private bool isPlayerHidden = false;



  private void Awake()
  {
    interactAction = InputSystem.actions.FindAction("Interact");
  }

  private void Update()
  {
    if (interactAction.WasPressedThisFrame())
    {
      CompleteActionEvent();
    }
  }


  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      isActionable = true;
      player = other.gameObject;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (!isPlayerHidden)
    {
      isActionable = false;
      player = null;
    }
  }


  private void CompleteActionEvent()
  {
    if (isActionable && !isHideableObject)
    {
      interactSO.IsInteractable();
      Destroy(gameObject);
    }

    if (isActionable && isHideableObject && player != null)
    {
      var playerRigidbody = player.GetComponent<Rigidbody>();
      playerRigidbody.isKinematic = !isPlayerHidden;
      skillSO.IsVisible(isPlayerHidden);

      var playerMeshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
      playerMeshRenderer.enabled = isPlayerHidden;
      isPlayerHidden = !isPlayerHidden;
      moveSO.IsMovable();
    }
  }

}
