﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerManager : MonoBehaviour
{
    private Animator playerAnimator;
    public static float health;
    public static float money;
    private float delayTime = 3f;
    public float bulletSpeed = 1f;
    private bool dead = false;
    private bool canShoot = true;
    public GameObject ReplayButton;
    public AudioSource takeDamageSound;
    public AudioSource shirukenSound;
    public AudioSource coinSound;
    public AudioSource dieSound;
    public AudioSource gameOverSound;
    public AudioSource christmasSong;
    public GameObject dieScreen;
    public Text moneyText;
    public Slider slider;
    Transform muzzle;
    public Transform bullet, floatingText;

    void Awake()
    {
        money = 0;
        health = 100;
    }


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        muzzle = transform.GetChild(1);
        slider.maxValue = health;
    }

    private void LateUpdate()
    {
    }


    void Update()
    {
        slider.value = health;
        playerAnimator.SetFloat("playerHealth", health);
        moneyText.text = "x " + money.ToString();

        if (Time.timeScale == 0)
        {
            christmasSong.Stop();
        }
    }


    public void GainMoney()
    {
        money++;
        coinSound.Play();
    }

    public void StayGetDamage(float damage)
    {
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text =
            damage.ToString();

        if ((health - damage) >= 0)
        {
            health -= damage;
            takeDamageSound.Play();
        }

        if (health <= 0)
        {
            DestroyPlayer();
        }

        slider.value = health;
        AmIDead();
    }

    public void DestroyPlayer()
    {
        Invoke(nameof(DieScreenDelay), 0.8f);
        // Invoke(nameof(DieScreenDelay),2f);
        christmasSong.Stop();
        dieSound.Play();
        FreezePlayer();
        health = 0;
        Destroy(gameObject, 1.3f);
        gameOverSound.PlayDelayed(1f);
    }

    public void FreezePlayer()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }


    void AmIDead()
    {
        if (health <= 0)
        {
            dieSound.Play();
            dead = true;
        }
    }

    public void ShootBullet()
    {
        if (canShoot)
        {
            StartCoroutine(ShootDelay());
            Transform tempBullet;
            tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
            tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
            shirukenSound.Play();
        }
    }

    private IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(1);
        canShoot = true;
    }

    public void DieScreenDelay()
    {
        dieScreen.SetActive(true);
    }
}