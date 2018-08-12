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
        transform.forward = flyDir;
    }

    // Update is called once per frame
    void FixedUpdate () {
        rig.MovePosition(transform.position + transform.forward * flySpeed*Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<EnemyController>().Damage(damage);
            // spawn effects, etc
            Destroy(gameObject);
        }
    }
}
