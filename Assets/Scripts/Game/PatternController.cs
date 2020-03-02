using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatternController : MonoBehaviour
{
  // Start is called before the first frame update
  private List<Enemy> enemies = new List<Enemy>();
  private Dictionary<string, Enemy> enemyDictionary = new Dictionary<string, Enemy>();
  int indexCounter = 0;
  float startTime;
  void Start()
  {
    startTime = Time.time;
  }

  public void AddToCollection(int enemyCount, int waveIndex, Enemy enemyToAdd)
  {
    enemyDictionary.Add(waveIndex.ToString() + enemyCount.ToString(), enemyToAdd);
    indexCounter++;
  }

  private void checkThenShoot(Enemy enemy)
  {
    if (enemy)
    {
      StartCoroutine(enemy.CountDownAndShoot());
    }
    else
    {
      return;
    }
  }

  private void FirePattern()
  {

    int timeSince = (int)System.Math.Round(Time.time - startTime);
    if (timeSince % 4 == 0)
    {
      foreach (KeyValuePair<string, Enemy> enemy in enemyDictionary)
      {
        if (System.Convert.ToInt32(enemy.Key) < 6)
          checkThenShoot(enemy.Value);
      }
    }
    if (timeSince % 3 == 0)
    {
      foreach (KeyValuePair<string, Enemy> enemy in enemyDictionary)
      {
        if (System.Convert.ToInt32(enemy.Key) > 12 && System.Convert.ToInt32(enemy.Key) < 24)
          checkThenShoot(enemy.Value);
      }
    }

    if (timeSince % 5 == 0)
    {
      foreach (KeyValuePair<string, Enemy> enemy in enemyDictionary)
      {
        if (System.Convert.ToInt32(enemy.Key) > 24)
          checkThenShoot(enemy.Value);
      }
    }
    Debug.Log(timeSince);
    if (timeSince >= 0 && timeSince < 1)
    {
      checkThenShoot(enemyDictionary["0"]);
    }
    if (timeSince > 0 && timeSince < 2)
    {
      checkThenShoot(enemyDictionary["1"]);
    }
    if (timeSince > 1 && timeSince < 3)
    {
      checkThenShoot(enemyDictionary["2"]);
    }
    if (timeSince > 2 && timeSince < 4)
    {
      checkThenShoot(enemyDictionary["3"]);
    }
    if (timeSince > 3 && timeSince < 5)
    {
      checkThenShoot(enemyDictionary["4"]);
    }
    if (timeSince > 4 && timeSince < 6)
    {
      checkThenShoot(enemyDictionary["5"]);
    }
    if (timeSince > 5 && timeSince < 7)
    {
      checkThenShoot(enemyDictionary["6"]);
    }
    if (timeSince > 6 && timeSince < 8)
    {
      checkThenShoot(enemyDictionary["7"]);
    }
    if (timeSince > 7 && timeSince < 9)
    {
      checkThenShoot(enemyDictionary["8"]);
    }
  }


  // Update is called once per frame
  void Update()
  {
    FirePattern();
  }
}
