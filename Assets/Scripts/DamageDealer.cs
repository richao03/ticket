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

  private IEnumerator Flicker(GameObject gameObject)
  {
    while (true)
    {
      gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
      yield return new WaitForSeconds(.01f);
      Flicker(gameObject);
    }
  }

  public IEnumerator Hit()
  {
    Destroy(gameObject);
    GameObject ammoExplosion = Instantiate(AmmoExplosion, transform.position, Quaternion.identity) as GameObject;
    Flicker(ammoExplosion);
    yield return new WaitForSeconds(.1f);
    Destroy(ammoExplosion);

  }
}
