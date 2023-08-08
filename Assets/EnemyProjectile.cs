using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour

{
    public Rigidbody2D rb;
    public float speed;
    public int damage;
    public float lifetime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 bulletMovement = speed * Time.deltaTime * transform.forward;

        // Apply the movement to the Rigidbody2D's position
        rb.MovePosition(rb.position + bulletMovement);
    }
}
