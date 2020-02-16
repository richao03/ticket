using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  //configuration parameters
  [SerializeField] float moveSpeed = 10f;
  [SerializeField] float paddingX = 0.1f;
  [SerializeField] float paddingY = .1f;
  [SerializeField] int health = 200;
  [SerializeField] GameObject PlayerFire;
  [SerializeField] float projectileSpeed = 5f;
  [SerializeField] float projectileRate = 1f;
  float xMin;
  float xMax;
  float yMin;
  float yMax;
  // Start is called before the first frame update
  void Start()
  {
    SetUpMovementBoundaries();
    StartCoroutine(ContinuousFire());
  }

  // Update is called once per frame
  void Update()
  {
    Move();

  }

  IEnumerator ContinuousFire()
  {
    while (true)
    {
      GameObject PlayerLaser = Instantiate(PlayerFire, transform.position, Quaternion.identity) as GameObject;
      PlayerLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
      yield return new WaitForSeconds(projectileRate);
    }
  }

  private void Move()
  {
    var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
    var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
    var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
    var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    transform.position = new Vector2(newXPos, newYPos);
  }

  private void SetUpMovementBoundaries()
  {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + paddingX;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - paddingX;
    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + paddingY;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y - paddingY;

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
    if (other.gameObject.tag == "EnemyFire")
    {
      DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
      ProcessHit(damageDealer);
    }
  }
}
