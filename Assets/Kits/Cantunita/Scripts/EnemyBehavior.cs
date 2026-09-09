using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehavior : MonoBehaviour
{

  [SerializeField] private float radius = 10f;
  [SerializeField] private float stopDistance = 1f;


  private NavMeshAgent agent;


  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  private void Update()
  {
    if (!agent.pathPending && agent.remainingDistance <= stopDistance)
    {
      Vector3 newTarget = GetRandomNavMeshPoint(transform.position, radius);
      agent.SetDestination(newTarget);
    }
  }


  private Vector3 GetRandomNavMeshPoint(Vector3 origin, float distance)
  {
    Vector3 randomDirection = Random.insideUnitSphere * distance;
    randomDirection += origin;

    NavMeshHit navHit;
    if (NavMesh.SamplePosition(randomDirection, out navHit, distance, -1))
    {
      return navHit.position;
    }

    return origin;
  }

}
