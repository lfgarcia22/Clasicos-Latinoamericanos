using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class CantunitaSpawner : MonoBehaviour
{

  [Header("Conditionals")]
  [SerializeField] private bool isMainObject;
  [SerializeField] private bool isDiscoverable;
  [SerializeField] private bool isHideable;
  [SerializeField] private GameObject parentObj;


  [Header("Main Discoverable")]
  [ShowIf("isMainObject")]
  [Label("Prefab")]
  [SerializeField]
  private GameObject mainRock;


  [Header("Letters & Rocks")]
  [ShowIf("isDiscoverable")]
  [Label("Letter")]
  [SerializeField]
  private GameObject letter;

  [ShowIf("isDiscoverable")]
  [Label("Rocks")]
  [SerializeField]
  private GameObject rocks;

  [ShowIf("isDiscoverable")]
  [Label("Max Rocks to generate")]
  [SerializeField]
  private int maxRockQuantity = 5;


  [Header("Hideable")]
  [ShowIf("isHideable")]
  [Label("Prefab")]
  [SerializeField]
  private GameObject hideable;

  [ShowIf("isHideable")]
  [Label("Min Hedeable Objects")]
  [SerializeField]
  private int minHideableQuantity = 5;

  [ShowIf("isHideable")]
  [Label("Max Hedeable Objects")]
  [SerializeField]
  private int maxHideableQuantity = 10;


  private BoxCollider spawnArea;


  private void Awake()
  {
    spawnArea = GetComponent<BoxCollider>();
    SpawnMainDiscoverable();
    SpawnAllDiscoverableObjects();
    SpawnAllHideableObjects();
  }


  private async void SpawnMainDiscoverable()
  {
    if (!isMainObject)
    {
      return;
    }

    SpawnObject(mainRock);
    await Task.Delay(1000);
    Destroy(gameObject);
  }


  private async void SpawnAllDiscoverableObjects()
  {
    if (!isDiscoverable)
    {
      return;
    }

    SpawnObject(letter);
    for (int i = 0; i < maxRockQuantity; i++)
    {
      SpawnObject(rocks);
    }
    await Task.Delay(1000);
    Destroy(gameObject);
  }


  private async void SpawnAllHideableObjects()
  {
    if (!isHideable)
    {
      return;
    }

    var randomQuantity = Random.Range(minHideableQuantity, maxHideableQuantity);
    for (int i = 0; i < randomQuantity; i++)
    {
      SpawnObject(hideable);
    }
    await Task.Delay(1000);
    Destroy(gameObject);
  }


  private void SpawnObject(GameObject prefab)
  {
    Vector3 randomPosition = GenerateRandomPosition();
    Instantiate(prefab, randomPosition, Quaternion.identity, parentObj.transform);
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
