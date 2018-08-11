using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follow player, on 
/// </summary>
public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(Vector3 recievingDir) {
        //SpawnOoze(transform.position);
    }

    public void SpawnOoze() {
        //
    }
}
