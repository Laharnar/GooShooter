using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Follow player, on 
/// </summary>
public class EnemyController : MonoBehaviour {

    public int health = 5;

    public Animator anim;
    public NavMeshAgent nav;

	// Use this for initialization
	void Start () {
        tag = "Enemy";

        anim.Play("Walk");
	}
	
	// Update is called once per frame
	void Update () {
        //nav.destination = GameplayManager.m.player.position;
    }

    private void OnCollisionEnter(Collision collision) {
        /*if (collision.transform == GameplayManager.m.player.transform) {
            Death();
        }*/
    }

    public void Damage(int dmg, Vector3 recievingDir) {
        health -= dmg;
        if (health <=0) {
            Death();
        }
        //SpawnOoze(transform.position);
    }

    private void Death() {
        // TODO: spawn effects/death animation
        Destroy(gameObject);
    }

    public void SpawnOoze() {
        //
    }
}
