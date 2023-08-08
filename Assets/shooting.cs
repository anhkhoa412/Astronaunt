using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform shotPoint;
    public GameObject Enemyprojectile;

    public float timeBetweenShots;
    float nextShotTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + timeBetweenShots;
            Instantiate(Enemyprojectile, shotPoint.position, shotPoint.rotation);

        }
    }
}
