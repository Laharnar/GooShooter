using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
    public GameObject player;

    public Material[] slimeMaterials;

    public List<Transform> objs = new List<Transform>();

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

	private void Awake()
	{
		instance = this;

        if (player== null) {
            player = GameObject.FindWithTag("Player");
        }
	}
	
   
}
