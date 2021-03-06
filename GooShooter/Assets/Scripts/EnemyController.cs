﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Follow player, on 
/// </summary>
public class EnemyController : MonoBehaviour
{

    public int health = 5;
    public int slimeDmg = 10;

    public Animator anim;
    public NavMeshAgent nav;
    public Collider coll;

    public ParticleSystem onHitPs;
    public ParticleSystem onDeathPs;
    public SkinnedMeshRenderer smr;

    public AudioSource hitSfx;
    public AudioSource deathSfx;

    // Use this for initialization
    void Start()
    {
        tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            nav.Stop();
            return;
        }
            
        if (GameManager.Instance.player != null)
            nav.destination = GameManager.Instance.player.transform.position;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (GameManager.Instance.player && collision.transform == GameManager.Instance.player.transform)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().Damage(slimeDmg);
            Death();
        }
    }
    private bool isAlive = true;
    public void Damage(int dmg)
    {
        if (!isAlive)
        {
            return;
        }
        health -= dmg;
        hitSfx.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
        hitSfx.Play();
        onHitPs.Play();
        if (health <= 0)
        {
            isAlive = false;
            Death();
        }

    }

    private void Death()
    {
        if (coll)
            coll.enabled = false;
        anim.SetTrigger("Death");

        Invoke("DelayedDeath", 1);
    }

    private void DelayedDeath()
    {
        onDeathPs.Play();
        SpawnOoze(transform.position);
        smr.enabled = false;
        deathSfx.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
        deathSfx.Play();
        Destroy(gameObject, 2);
    }

    public void SpawnOoze(Vector3 pos)
    {
        Block b = GameManager.GetBlock(transform.position);
        if (b)
        {
            if (!b.isSlimeActive)
                b.ToggleSlime(true);
        }

        /*
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up / 2, Vector3.down, out hit, Mathf.Infinity, 1<<LayerMask.NameToLayer("Ground"))) {
            Debug.Log(hit.transform);
            Transform groundCube = hit.transform;
            if (groundCube.parent != null)
                groundCube.parent.GetComponent<Block>().ToggleSlime(true);
            //Destroy(hit.transform.gameObject);
        }*/
        // Debug.Log(hit.transform);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position + Vector3.up / 2, Vector3.down));
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position + Vector3.up / 2, Vector3.down), out hit, Mathf.Infinity))
        {

        }
    }
}
