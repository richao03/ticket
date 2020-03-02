using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] int waveIndex;
  [SerializeField] int durationBetweenSpan;
  public PatternController patternController;
  int startingWave = 0;
  void Start()
  {
    var currentWave = waveConfigs[startingWave];
    StartCoroutine(SpwanAllWaves());
  }

  // Start is called before the first frame update
  private IEnumerator SpawnAllEnemyInWave(WaveConfig waveConfig, int waveIndex)
  {
    for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
    {
      var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
      waveConfig.GetWaypoints()[startingWave].transform.position,
      Quaternion.identity);
      newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
      Enemy targetEnemy = newEnemy.GetComponent<Enemy>();
      patternController.AddToCollection(enemyCount, waveIndex, targetEnemy);

      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpwan());
    }
  }

  private IEnumerator waitSeconds(int seconds)
  {
    yield return new WaitForSeconds(seconds);   //Wait

  }

  //counts waveConfig for number of waves
  private IEnumerator SpwanAllWaves()
  {
    for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
    {
      var currentWave = waveConfigs[waveIndex];
      StartCoroutine(SpawnAllEnemyInWave(currentWave, waveIndex));
      yield return new WaitForSeconds(durationBetweenSpan);

    }
  }



  // Update is called once per frame
  void Update()
  {

  }
}
