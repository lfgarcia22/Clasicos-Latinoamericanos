using System.Collections.Generic;
using UnityEngine;

public class HideableWrapperBehavior : MonoBehaviour
{

  [SerializeField] private List<GameObject> prefabs;


  private void Awake()
  {
    ExecuteLoadChild();
  }


  private void ExecuteLoadChild()
  {
    if (prefabs.Count == 0)
    {
      return;
    }

    var idx = Random.Range(0, prefabs.Count - 1);
    var rotation = transform.rotation;
    var rotationY = Random.Range(0, 180);
    Instantiate(
      prefabs[idx],
      transform.position,
      new Quaternion(
        rotation.x,
        rotationY,
        rotation.z,
        rotation.w
      ),
      transform
    );
  }

}
