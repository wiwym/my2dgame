﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{

    public float health;
    public float bulletSpeed = 1f;
    private bool dead = false;
    private bool canShoot = true;
    public AudioSource takeDamageSound;
    public AudioSource shirukenSound;
    // Start is called before the first frame update

    public Slider slider;
    Transform muzzle;
    public Transform bullet, floatingText;
    void Start()
    {
        muzzle = transform.GetChild(1);
        slider.maxValue = health;
        slider.value = health;
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ShootBullet();
        }
    }



    public void StayGetDamage(float damage)
    {
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();
        
        if ((health - damage) >= 0)
        {
          
            health -= damage;
            takeDamageSound.Play();

        }

        if ( health <= 0)
        {
            Destroy(gameObject);
            
        }
        slider.value = health;
        
        AmIDead();
    }


    void AmIDead()
    {
        if (health <= 0)
        {
            dead = true;
        }
    }

    void ShootBullet()
    {
        if (canShoot)
        {
            Transform tempBullet;
            tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
            tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
            shirukenSound.Play();
            StartCoroutine(ShootDelay());
        }
      
    }

    private IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(1);
        canShoot = true;

    }

}
