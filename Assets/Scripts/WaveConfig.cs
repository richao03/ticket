using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject
{
  [SerializeField] GameObject enemyPrefab;
  [SerializeField] GameObject pathPrefab;
  [SerializeField] float timeBetweenSpwan = 0.5f;
  [SerializeField] int numberOfEnemies = 6;
  [SerializeField] float movementSpeed = 3f;

  public GameObject GetEnemyPrefab()
  {
    return enemyPrefab;
  }

  public List<Transform> GetWaypoints()
  {
    var waveWaypoints = new List<Transform>();
    foreach (Transform child in pathPrefab.transform)
    {
      waveWaypoints.Add(child);
    }
    return waveWaypoints;
  }

  public float GetMovementSpeed()
  {
    return movementSpeed;
  }

  public GameObject GetPathPrefab()
  {
    return pathPrefab;
  }

  public float GetTimeBetweenSpwan()
  {
    return timeBetweenSpwan;
  }

  public float GetNumberOfEnemies()
  {
    return numberOfEnemies;
  }
}
