using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Follow player, on 
/// </summary>
public class EnemyController : MonoBehaviour
{

    public int health = 5;

    public Animator anim;
    public NavMeshAgent nav;

    // Use this for initialization
    void Start()
    {
        tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        nav.destination = GameManager.Instance.player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == GameManager.Instance.player.transform)
        {
            Death();
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Death();
        }
        //SpawnOoze(transform.position);
    }

    private void Death()
    {
        // TODO: spawn effects/death animation
        anim.SetTrigger("Death");
        Destroy(gameObject, 1);
        
    }

    public void SpawnOoze()
    {
        //
    }
}
