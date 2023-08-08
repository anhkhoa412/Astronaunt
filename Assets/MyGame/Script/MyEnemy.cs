using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class MyEnemy : MonoBehaviour
{
    public float speed;
    public Transform Player;
    public Animator animator;

    public Vector3 directionToPlayer;

  

    public GameObject[] heart;
    public int health;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Player = FindObjectOfType<MyPlayerController>().transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        RotateTowardsPlayer();
        transform.position = Vector2.MoveTowards(transform.position,Player.position, speed * Time.deltaTime);
    }

    public virtual void RotateTowardsPlayer()
    {
        directionToPlayer = Player.position - transform.position;
        float dotProduct = Vector3.Dot(directionToPlayer, transform.right);
        if (dotProduct > 0)
        {
            // Player is on the right side, flip the sprite to face right
            // Assuming you have a SpriteRenderer component on the enemy GameObject
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            // Player is on the left side, flip the sprite to face left
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
            
        }
        if(health  == 0)
        {
            speed = 0;
            animator.SetBool("isDeath", true);
        }
        if (other.tag == "Player")
        {
           // SceneManager.LoadScene("Game");
        }
    }

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, heart.Length + 1);
        
        for (int i = 0; i < heart.Length; i++)
        {
            if (i >= health)
            {
                heart[i].SetActive(false);
            }
        }
    }

    public virtual void death()
    {
        if (health <= 0)
        {

            Destroy(gameObject);
            animator.SetBool("isDeath", false);
        }
    }


}
