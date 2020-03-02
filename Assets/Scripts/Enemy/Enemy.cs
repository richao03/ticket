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
  [SerializeField] GameObject AmmoExplosion;
  [SerializeField] bool isSquid = false;
  public Animator animator;
  private GameObject TargetToShoot;
  bool beenHit = false;
  bool canShoot = true;
  GameObject ammoExplosion;
  // Start is called before the first frame update
  void Start()
  {
    shotCounter = timeBetweenShots;
    if (isSquid) { StartCoroutine(Flicker()); }

  }

  // Update is called once per frame
  void Update()
  {
    // CountDownAndShoot();
  }

  private IEnumerator Flicker()
  {
    while (true)
    {
      gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
      yield return new WaitForSeconds(.25f);
      Flicker();
    }
  }

  public IEnumerator CountDownAndShoot()
  {
    // random firing
    // shotCounter -= Time.deltaTime;
    // if (shotCounter <= 0f)
    // {
    // shotCounter = timeBetweenShots;
    // }
    if (canShoot)
    {
      Debug.Log("FIRE");
      StartCoroutine(Fire());
      canShoot = false;
      yield return new WaitForSeconds(1f);
      canShoot = true;
    }
  }

  private IEnumerator Fire()
  {
    GameObject EmenyFire = Instantiate(EnemyProjectile, transform.position, Quaternion.identity) as GameObject;
    var enemyLaserRB = EmenyFire.GetComponent<Rigidbody2D>();
    if (isSquid)
    {
      //AIMED SHOT
      TargetToShoot = GameObject.FindGameObjectWithTag("Player");
      Vector2 target = (TargetToShoot.transform.position - transform.position).normalized * 10f;
      enemyLaserRB.velocity = new Vector2(target.x, target.y);
      enemyLaserRB.transform.eulerAngles = new Vector3(0, 0, 50);
    }
    else
    {
      //ANIMATION         //normal shot


      animator.SetBool("is_attacking", true);
      yield return new WaitForSeconds(.1f);
      EmenyFire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
      yield return new WaitForSeconds(.5f);
      animator.SetBool("is_attacking", false);
    }







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
