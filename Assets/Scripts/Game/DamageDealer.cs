using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
  [SerializeField] int damage = 10;
  [SerializeField] GameObject AmmoExplosion;
  public int GetDamage()
  {
    return damage;
  }

  public IEnumerator Hit()
  {
    Destroy(gameObject);
    var ammoExplosion = Instantiate(AmmoExplosion,
    gameObject.transform.position,
    Quaternion.identity) as GameObject;
    Debug.Log("create explosion");
    yield return new WaitForSeconds(.2f);
    Debug.Log("destroy explosion");
    Destroy(ammoExplosion);

  }
}
