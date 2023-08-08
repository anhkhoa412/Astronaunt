using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MyEnemy {


    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public Transform shotPoint;
    public GameObject EnemyProjectile;

    public Transform weapon;

    public float timeBetweenShots;
    float nextShotTime;

    public float offset;

    public Rigidbody2D rb;

    public RangeEnemy()
    {
    }


    // Start is called before the first frame update
    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        base.Start();
        nextShotTime = timeBetweenShots;
       
    }

    // Update is called once per frame
    public override void Update()
    {

        if (Vector2.Distance(Player.position, transform.position) > distanceToShoot)
        {
            base.Update();
        }

        
        if (Time.time - nextShotTime >= timeBetweenShots)
        {
            WeaponRotation();
            Shoot();
            nextShotTime = Time.time;
        }

    }

    public override void RotateTowardsPlayer()
    {
        base.RotateTowardsPlayer();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void death()
    {
        base.death();
    }

    private void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(EnemyProjectile, shotPoint.position, shotPoint.rotation);
            nextShotTime = timeBetweenShots;
        } else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    private void WeaponRotation()
    {
        Vector3 displacement = Player.position - weapon.position; // Calculate displacement relative to weapon position

        // Check if the enemy is flipped
        float flipMultiplier = Mathf.Sign(transform.localScale.x); // +1 if not flipped, -1 if flipped

        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        angle += offset;

        // Adjust the angle based on the enemy's orientation
        if (flipMultiplier == -1)
        {
            angle = 180f - angle; // Flip the angle if the enemy is flipped
        }

        weapon.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
