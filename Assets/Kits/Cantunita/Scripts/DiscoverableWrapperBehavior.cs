using UnityEngine;

public class DiscoverableWrapperBehavior : MonoBehaviour
{

  private Rigidbody rigidbody;
  private DiscoverableHideableActions childGameObject;


  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
    childGameObject = GetComponentInChildren<DiscoverableHideableActions>();
  }


  private void OnTriggerEnter(Collider other)
  {
    if (other.tag != "Respawn")
    {
      rigidbody.useGravity = false;
      childGameObject.transform.SetParent(null, true);
      Destroy(gameObject);
    }
  }

}
