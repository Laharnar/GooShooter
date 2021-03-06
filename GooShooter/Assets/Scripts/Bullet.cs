﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody rig;
    public float speed = 300;

    const string playerComponent = "Player";

    public void Shoot(Vector3 direction)
    {
        rig.AddForce(direction.normalized * speed);
    }

    private void Start()
    {
        Destroy(gameObject, 5);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Damage(1);
            // spawn effects, etc
            Destroy(gameObject);
        }
    }
}