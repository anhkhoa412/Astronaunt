using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyPlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform weapon;
    public float offset;

    public Rigidbody2D rb;

    public Animator animator;

    public Transform shotPoint;
    public GameObject projectile;

    public GameObject Effect;

    public float timeBetweenShots;
    float nextShotTime;

    private bool isSlide = false;


    Vector2 movement;

    public SpriteRenderer spriteRender;
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            
        } else
            animator.SetFloat("Speed", 0f);

        if (Input.GetKeyDown(KeyCode.Space) && !isSlide)
        {
            isSlide = true;
            speed = 10f;
        }

        animator.SetBool("isSlide", isSlide);

        WeaponRotation();
        Shotting();
        
    }

    public void OnSliceAnimationEnd()
    {
        isSlide = false;
        animator.SetBool("isSlide", isSlide);
        speed = 5f;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

    }

    private void Shotting()
    {
        //Shooting 
        if (Input.GetMouseButtonDown(0))
        {

            Effect.GetComponent<Renderer>().enabled = true;
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + timeBetweenShots;
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
                Instantiate(Effect, shotPoint.position, shotPoint.rotation);


            }
        }
    }

    private void WeaponRotation()
    {

        // Weapon Rotation
        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Flip the Gun sprite based on the mouse position
        if (mousePosition.x < weapon.position.x)
        {
            weapon.localScale = new Vector3(1f, 1f, 1f);

        }
        else
        {
            weapon.localScale = new Vector3(1f, -1f, 1f);
        }
    }

}
