using System;
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
    public Collider coll;


    // Use this for initialization
    void Start() {
        tag = "Enemy";
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.player != null)
            nav.destination = GameManager.Instance.player.transform.position;
    }


    private void OnTriggerEnter(Collider collision) {
        if (GameManager.Instance.player && collision.transform == GameManager.Instance.player.transform) {
            GameManager.Instance.player.GetComponent<PlayerController>().Damage(slimeDmg);
            Death();
        }
    }

    public void Damage(int dmg) {
        health -= dmg;

        Vector3 snapPos = new Vector3(transform.position.x-transform.position.x%1, 0, transform.position.z-transform.position.z%1);
        for (int i = 0; i < GameManager.Instance.objs.Count; i++) {
            Vector3 p = new Vector3(GameManager.Instance.objs[i].transform.position.x, 0, GameManager.Instance.objs[i].transform.position.z);
            if (p == snapPos) {
                GameManager.Instance.objs[i].GetComponent<Block>().ToggleSlime(true);
            }
        }

        //SpawnOoze(transform.position);
        if (health <= 0) {
            Death();
        }
    }

    private void Death() {
        if (coll)
        coll.enabled = false;
        anim.SetTrigger("Death");
        Destroy(gameObject, 1);
    }

    public void SpawnOoze(Vector3 pos) {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up / 2, Vector3.down, out hit, Mathf.Infinity, 1<<LayerMask.NameToLayer("Ground"))) {
            Debug.Log(hit.transform);
            Transform groundCube = hit.transform;
            if (groundCube.parent != null)
                groundCube.parent.GetComponent<Block>().ToggleSlime(true);
            //Destroy(hit.transform.gameObject);
        }
        // Debug.Log(hit.transform);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawRay(new Ray(transform.position + Vector3.up / 2, Vector3.down));
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position + Vector3.up / 2, Vector3.down), out hit, Mathf.Infinity)) {

        }
    }
}
