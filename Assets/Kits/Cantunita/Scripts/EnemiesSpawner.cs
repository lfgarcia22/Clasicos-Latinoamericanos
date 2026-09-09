using System.Collections.Generic;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
  [Label("Enemies Prefabs")]
  [SerializeField]
  private List<GameObject> prefabs;

  [Label("Max enemies to generate")]
  [SerializeField]
  private int maxQuantity = 10;

  private BoxCollider spawnArea;


  private void Awake()
  {
    spawnArea = GetComponent<BoxCollider>();
    SpawnEnemiesObj();
  }


  private async void SpawnEnemiesObj()
  {
    for (int i = 0; i < maxQuantity; i++)
    {
      var randomIdx = Random.Range(0, prefabs.Count - 1);
      SpawnObject(prefabs[randomIdx]);
    }
    await Task.Delay(1000);
    Destroy(gameObject);
  }


  private void SpawnObject(GameObject prefab)
  {
    Vector3 randomPosition = GenerateRandomPosition();
    Instantiate(prefab, randomPosition, Quaternion.identity, gameObject.transform.parent);
  }

  private Vector3 GenerateRandomPosition()
  {
    Bounds bounds = spawnArea.bounds;
    return new Vector3(
      Random.Range(bounds.min.x, bounds.max.x),
      bounds.min.y,
      Random.Range(bounds.min.z, bounds.max.z)
    );
  }

}
