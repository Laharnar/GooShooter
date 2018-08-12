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
    public int slimeDmg = 10;

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
        if (GameManager.Instance.player != null)
            nav.destination = GameManager.Instance.player.transform.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform == GameManager.Instance.player.transform)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().Damage(slimeDmg);
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
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down * 2), out hit))
        {
            Transform groundCube = hit.transform;
            groundCube.GetComponent<Block>().ToggleSlime(true);
        }
        anim.SetTrigger("Death");
        Destroy(gameObject, 1);
    }

    public void SpawnOoze()
    {
        //
    }
}
