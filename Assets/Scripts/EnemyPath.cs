﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
  [SerializeField] WaveConfig waveConfig;
  List<Transform> waypoints;
  [SerializeField] float moveSpeed = 2f;
  int waypointIndex = 0;

  // Start is called before the first frame update
  void Start()
  {
    waypoints = waveConfig.GetWaypoints();
    moveSpeed = waveConfig.GetMovementSpeed();
    transform.position = waypoints[waypointIndex].transform.position;
    Debug.Log(waypoints[waypointIndex]);
  }

  // Update is called once per frame
  void Update()
  {
    Move();
  }

  public void SetWaveConfig(WaveConfig waveConfig)
  {
    this.waveConfig = waveConfig;
  }

  private void Move()
  {
    if (waypointIndex <= waypoints.Count - 1)
    {
      Debug.Log(waypoints[waypointIndex]);
      var targetPosition = waypoints[waypointIndex].transform.position;
      var movement = 2f * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, movement);

      if (transform.position == targetPosition)
      {
        Debug.Log("POsition same");
        if (waypointIndex == waypoints.Count - 1)
        { waypointIndex = 1; }
        else { waypointIndex++; }
      }
    }
  }
}