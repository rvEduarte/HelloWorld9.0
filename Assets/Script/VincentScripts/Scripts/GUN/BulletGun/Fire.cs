using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    public static bool enableFire;
    [SerializeField] private Rigidbody2D bullet;

    [SerializeField] private Transform barrel;

    private float bulletSpeed = 500f;

    private float fireRate = 1f;
    private float nextFire = 0f;

    // Update is called once per frame

    private void Start()
    {
        enableFire = true; //ENABLE
    }
    void Update()       
    {
        //if (!enableFire) return;

        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            var spawnedBullet = Instantiate(bullet,barrel.position, barrel.rotation);
            spawnedBullet.AddForce(barrel.right * bulletSpeed);


        }
    }
}
