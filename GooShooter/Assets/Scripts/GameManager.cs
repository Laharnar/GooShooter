using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject player;

    public Material[] slimeMaterials;

    public List<Transform> groundObjs = new List<Transform>();

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    private void Awake()
    {
        instance = this;

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }


}
