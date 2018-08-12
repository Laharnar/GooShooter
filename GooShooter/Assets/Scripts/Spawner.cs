using System.Collections;
using UnityEngine;
public class Spawner:MonoBehaviour {

    public Transform enemyPref;
    int activeRate = 0;
    public int[] requiredCounter = new int[3] {0, 100, 400 };
    public float[] spawnRates = new float[3] { 0.9f, 0.3f, 0.1f };

    private void Start() {
        Area a = GameObject.FindObjectOfType<Area>();
        if (a)
            ActivateArea(a);
        else Debug.Log("Test no area");
    }

    void ActivateArea(Area mapThatPlayerIsOn) {
        Debug.Log("Activatin area "+mapThatPlayerIsOn.name);
        SpawnPoint[] pts = mapThatPlayerIsOn.GetComponentsInChildren<SpawnPoint>();

        if (!mapThatPlayerIsOn.spawningActivated) {
            mapThatPlayerIsOn.spawningActivated = true;

            mapThatPlayerIsOn.StartCoroutine(SpawnWave(pts, CreateList(enemyPref, pts.Length)));
        }

        //SpawnWave(pts, enemyPref);

    }

    private Transform[] CreateList(Transform enemyPref, int length) {
        Transform[] t = new Transform[length];
        for (int i = 0; i < length; i++) {
            t[i] = enemyPref;
        }
        return t;
    }

    IEnumerator SpawnWave(SpawnPoint[] spawnPoints, Transform[] unitPrefs) {
        int counter = 0;
        float multiplier = spawnRates[activeRate];
        while (true) {
            for (int i = 0; i < spawnPoints.Length && i < unitPrefs.Length; i++) {
                yield return new WaitForSeconds(multiplier);
                Spawn(spawnPoints[i], unitPrefs[i]);
                counter++;
                if (activeRate+1 < requiredCounter.Length 
                    && counter == requiredCounter[activeRate+1]) {
                    activeRate++;
                    multiplier= spawnRates[activeRate];
                }
            }
        }
    }

    void Spawn(SpawnPoint pt, Transform pref) {
        Instantiate(pref, pt.transform.position, new Quaternion());
    }
}
