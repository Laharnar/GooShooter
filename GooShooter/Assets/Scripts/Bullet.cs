using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Rigidbody rig;

    public float flySpeed = 10f;
    Vector3 flyDir;
    public int damage = 1;

    const string playerComponent = "Player";

    public void Init(Vector3 flyDir) {
        this.flyDir = flyDir.normalized;
    }

    // Update is called once per frame
    void Update () {
        rig.MovePosition(transform.position + flyDir * flySpeed);
	}

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Player") {
            // GameplayManager.player.Damage(damage);
            // spawn effects, etc
            Destroy(gameObject);
        }
    }
}
