﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Follow player, on 
/// </summary>
public class EnemyController : MonoBehaviour {

    public int health = 5;
    public int slimeDmg = 10;

    public Animator anim;
    public NavMeshAgent nav;

	// Use this for initialization
	void Start () {
        tag = "Enemy";

        anim.Play("Walk");
	}
	
	// Update is called once per frame
	void Update () {
        nav.destination = GameManager.Instance.player.transform.position;
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.transform == GameManager.Instance.player.transform) {
            GameManager.Instance.player.GetComponent<PlayerController>().Damage(slimeDmg);
            Death();
        }
    }

    public void Damage(int dmg) {
        health -= dmg;
        if (health <=0) {
            Death();
        }
        //SpawnOoze(transform.position);
    }

    private void Death() {
        // TODO: spawn effects/death animation
        RaycastHit hit;
        if(Physics.Raycast(new Ray(transform.position, Vector3.down*2), out hit)) {
            Transform groundCube = hit.transform;
            groundCube.GetComponent<Block>().ToggleSlime (true);
        }
        Destroy(gameObject);

    }

    public void SpawnOoze() {
        //
    }
}
