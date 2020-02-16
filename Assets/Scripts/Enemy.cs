using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float health = 100;
  [SerializeField] float shotCounter;
  [SerializeField] float timeBetweenShots = 3f;
  [SerializeField] GameObject EnemyProjectile;
  [SerializeField] float projectileSpeed = 3f;
  // Start is called before the first frame update
  void Start()
  {
    shotCounter = timeBetweenShots;
    StartCoroutine(Flicker());
  }

  // Update is called once per frame
  void Update()
  {
    CountDownAndShoot();
  }

  private IEnumerator Flicker()
  {
    while (true)
    {
      gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
      yield return new WaitForSeconds(.3f);
      Flicker();
    }
  }
  private void CountDownAndShoot()
  {
    shotCounter -= Time.deltaTime;
    if (shotCounter <= 0f)
    {
      Fire();
      shotCounter = timeBetweenShots;
    }
  }

  private void Fire()
  {
    GameObject EmenyFire = Instantiate(EnemyProjectile, transform.position, Quaternion.identity) as GameObject;
    EmenyFire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    Debug.Log("Fire");

  }

  private void ProcessHit(DamageDealer damageDealer)
  {

    health -= damageDealer.GetDamage();
    StartCoroutine(damageDealer.Hit());
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "PlayerFire")
    {
      DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
      ProcessHit(damageDealer);
    }
  }
}
