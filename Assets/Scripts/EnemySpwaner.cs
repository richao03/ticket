using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] int waveIndex;
  [SerializeField] int durationBetweenSpan;
  int startingWave = 0;

  // Start is called before the first frame update
  private IEnumerator SpawnAllEnemyInWave(WaveConfig waveConfig)
  {
    for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
    {
      var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
      waveConfig.GetWaypoints()[startingWave].transform.position,
      Quaternion.identity);
      newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpwan());
    }
  }

  private IEnumerator waitSeconds(int seconds)
  {
    yield return new WaitForSeconds(seconds);   //Wait

  }
  private IEnumerator SpwanAllWaves()
  {
    for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
    {
      var currentWave = waveConfigs[waveIndex];
      StartCoroutine(SpawnAllEnemyInWave(currentWave));
      yield return new WaitForSeconds(durationBetweenSpan);

    }
  }

  void Start()
  {
    var currentWave = waveConfigs[startingWave];
    StartCoroutine(SpwanAllWaves());
  }


  // Update is called once per frame
  void Update()
  {

  }
}
