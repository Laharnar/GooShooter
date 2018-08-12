using System;
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
    public int oozeDmg=1;
    public float oozeDmgRate = 1;

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
        Pause();
        instance = this;

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    private void Pause() {
        Time.timeScale = 0;
    }

    public void ResumeTime() {
        Time.timeScale = 1;
    }

    internal static Block GetBlock(Vector3 position) {

        Vector3 snapPos = new Vector3(position.x - position.x % 1, 0, position.z - position.z % 1);
        for (int i = 0; i < GameManager.Instance.groundObjs.Count; i++) {
            Vector3 p = new Vector3(GameManager.Instance.groundObjs[i].transform.position.x, 0, GameManager.Instance.groundObjs[i].transform.position.z);
            if (p == snapPos) {
                return GameManager.Instance.groundObjs[i].GetComponent<Block>();
            }
        }
        return null;
    }
}
