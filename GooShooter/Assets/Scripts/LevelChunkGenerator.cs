using System.Collections.Generic;
using UnityEngine;
public class LevelChunkGenerator:MonoBehaviour {

    public float fillWithTexturesPerc = 1f;

    public Texture[] textures;
    public Transform[] levelDesignLib;
    public Transform[] groundCubesLib;
    public int chunkCount = 4;
    public int w=30, l=30;
    public float rndHeight = 0.2f;

    private void Start() {
        GenerateLevelPiece();
    }

    public GameObject GenerateLevelPiece() {
        GameObject levelPiece = new GameObject("Level");
        GenerateObstaclePiece(levelDesignLib).transform.parent = levelPiece.transform;
        GameObject groundParent = GenerateGroundArea(groundCubesLib, chunkCount, w, l, 2 * w / 3);
        groundParent.transform.parent = levelPiece.transform;

        GenerateProps(groundParent);
        return levelPiece;
    }

    private GameObject GenerateProps(GameObject groundParent) {
        GameObject g = new GameObject("Props");
        for (int i = 0; i < groundParent.transform.childCount; i++) {
            if (UnityEngine.Random.Range(0f, 1f) < 0.5f) {
                Transform prop = Instantiate(groundParent.transform.GetChild(i), new Vector3(0, 0.5f, 0), new Quaternion(), g.transform);
            }
        }
        return g;
    }


    public GameObject GenerateObstaclePiece(Transform[] obstaclePrefLib) {
        GameObject obstacles = new GameObject("Obstacles");
        if (obstaclePrefLib.Length == 0) return obstacles;
        int r = UnityEngine.Random.Range(0, obstaclePrefLib.Length);
        Instantiate(obstaclePrefLib[r]).parent = obstacles.transform;
        return obstacles;
    }

    public GameObject GenerateGroundArea(Transform[] cubePrefs, int pieces, int w, int l, float maxDist) {
        // v 2
        // pick num of different pieces, then generate floor from that number of points
        if (cubePrefs.Length < 2) return new GameObject("No ground generated.");

        Vector2 center = new Vector2(w/2, l/2);

        int differentPieces = pieces;//Random.Range(1, cubePrefs.Length);
        // generate vectors
        Vector3 offset = Vector3.zero;
        Vector3[,] generatedPositions = new Vector3[w, l];
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < l; j++) {
                generatedPositions[i, j] = (new Vector3(i, j)+offset);
            }
        }

        // choose n random vectors
        Vector3[] rndPositions = new Vector3[differentPieces];
        for (int i = 0; i < differentPieces; i++) {
            rndPositions[i] = generatedPositions[Random.Range(0, generatedPositions.GetLength(0)),
                Random.Range(0, generatedPositions.GetLength(1))];
        }

        // flood cubes from those positions, choose id for cube
        int[,] finalMap = new int[w, l];
        Queue<Vector3> floodQue = new Queue<Vector3>();
        // assign starting values
        for (int i = 0; i < differentPieces; i++) {
            floodQue.Enqueue(rndPositions[i]);
            finalMap[(int)rndPositions[i].x, (int)rndPositions[i].y] = (i % cubePrefs.Length + 1);
        }
        // flood those values
        // Note: 0 means unassigned
        int lastI = 0;
        while (floodQue.Count > 0) {
            Vector3 p = floodQue.Dequeue();
            int x = (int)p.x;
            int y = (int)p.y;
            
            int i = Random.Range(0, cubePrefs.Length);
            if (i == lastI) {
                i = (lastI + 1)%cubePrefs.Length;
            }
            lastI = i;
            if (i == 0)
            if (x + 1 < w && finalMap[x + 1, y] == 0 && Random.Range(0, 1) < 0.7f) {
                finalMap[x + 1, y] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x+1, y]);
            }
            if (i == 1)
            if (x-1 >= 0 && finalMap[x - 1, y] == 0 && Random.Range(0, 1) < 0.7f) {
                finalMap[x - 1, y] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x-1, y]);
            }
            if (i == 2)
            if (y+ 1 < l && finalMap[x, y + 1] == 0 && Random.Range(0, 1) < 0.7f) {
                finalMap[x, y + 1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x, y+1]);
            }
            if (i == 3)
            if (y - 1 >= 0 && finalMap[x, y - 1] == 0 && Random.Range(0, 1) < 0.7f) {
                finalMap[x, y - 1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x, y-1]);
            }



            if (x + 1 < w && y + 1 < l && finalMap[x + 1, y + 1] == 0 && Random.Range(0, 1) < 0.3f) {
                finalMap[x + 1, y+1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x + 1, y+1]);
            }
            if (x + 1 < w && y - 1 >= 0 && finalMap[x + 1, y - 1] == 0 && Random.Range(0, 1) < 0.3f) {
                finalMap[x + 1, y -1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x + 1, y -1]);
            }
            if (x - 1 >= 0 && y + 1 < l && finalMap[x - 1, y + 1] == 0 && Random.Range(0, 1) < 0.3f) {
                finalMap[x - 1, y + 1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x - 1, y + 1]);
            }
            if (x - 1 >= 0 && y - 1 >= 0 && finalMap[x - 1, y - 1] == 0 && Random.Range(0, 1) < 0.3f) {
                finalMap[x - 1, y - 1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x - 1, y - 1]);
            }
        }
        /*float[] randomWeights = new float[cubePrefs.Length];
        float min = 1;
        for (int i = (int)Random.Range(0f, 1f) * cubePrefs.Length; i < cubePrefs.Length; i++) {
            randomWeights[i] = Random.Range(min, 1);
            min = randomWeights[i];
        }
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < w; j++) {
                int randI = Random.Range(0, cubePrefs.Length);
                finalMap[i, j] = (int)(finalMap[i, j] + randI+1)/2;
                //bool spawn = Random.Range(0, 1) < randomWeights[i];
                //if (spawn) {
                //Transform t = Instantiate(cubePrefs[randI], new Vector3(i, j), new Quaternion(), ground.transform);
                //}
            }
        }*/
        // create map in a shape of a circle
        for (int i = 0; i < finalMap.GetLength(0); i++) {
            for (int j = 0; j < finalMap.GetLength(1); j++) {
                if (Vector3.Distance(center, generatedPositions[i, j]) > maxDist)
                    finalMap[i, j] = -1;
            }
        }

        Queue<Vector3> edgeCoordinates = new Queue<Vector3>();
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < l; j++) {
                // find edge cases up and down
                if ((j-1 > 0 && finalMap[i, j-1] == -1 
                    && finalMap[i, j - 1] != finalMap[i, j])
                    || (i-1 > 0 && finalMap[i-1, j] == -1
                    && finalMap[i -1, j] != finalMap[i, j]))
                    edgeCoordinates.Enqueue(new Vector3(i, j));
            }
        }
        // extend the circle into uneven shapes
        while (edgeCoordinates.Count > 0) {
            Vector3 v = edgeCoordinates.Dequeue();
            int x = (int)v.x;
            int y = (int)v.y;

            //finalMap[(int)v.x+1, (int)v.y] = finalMap[(int) v.x, (int)v.y];
            //finalMap[(int)v.x, (int)v.y+1] = finalMap[(int) v.x, (int)v.y];
            //finalMap[(int)v.x-1, (int)v.y] = finalMap[(int) v.x, (int)v.y];
            //finalMap[(int)v.x, (int)v.y-1] = finalMap[(int) v.x, (int)v.y];
            int required = -1;
            if (x + 1 < w && finalMap[x + 1, y] == required && Random.Range(0, 1) < 0.3f) {
                finalMap[x + 1, y] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x + 1, y]);
            }
            if (x - 1 >= 0 && finalMap[x - 1, y] == required && Random.Range(0, 1) < 0.3f) {
                finalMap[x - 1, y] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x - 1, y]);
            }
            if (y + 1 < w && finalMap[x, y + 1] == required && Random.Range(0, 1) < 0.3f) {
                finalMap[x, y + 1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x, y + 1]);
            }
            if (y - 1 >= 0 && finalMap[x, y - 1] == required && Random.Range(0, 1) < 0.3f) {
                finalMap[x, y - 1] = finalMap[x, y];
                floodQue.Enqueue(generatedPositions[x, y - 1]);
            }
        }

        GameObject ground = new GameObject("Ground");
        for (int i = 0; i < finalMap.GetLength(0); i++) {
            for (int j = 0; j < finalMap.GetLength(1); j++) {
                int x = (int)generatedPositions[i, j].x;
                int y = (int)generatedPositions[i, j].y;
                if (finalMap[x, y] == -1)
                    continue;
                if (finalMap[x, y] == 0)
                    finalMap[x, y] = 1;
                Quaternion rot = Quaternion.Euler(0, Random.Range(0, 8)*90, 0);
                Transform t = Instantiate(cubePrefs[finalMap[x, y]-1], new Vector3(i,Random.Range(0, rndHeight), j), rot, ground.transform);
            }
        }

        return ground;
        /* // v 1
        float[] randomWeights = new float[cubePrefs.Length];
        float min = 1;
        for (int i = (int)Random.Range(0f, 1f)*cubePrefs.Length; i < cubePrefs.Length; i++) {
            randomWeights[i] = Random.Range(min, 1);
            min = randomWeights[i];
        }

        GameObject ground= new GameObject("Ground");
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < w; j++) {
                int randI = Random.Range(0, cubePrefs.Length);
                finalQue[i, j] = randI;
                //bool spawn = Random.Range(0, 1) < randomWeights[i];
                //if (spawn) {
                    Transform t = Instantiate(cubePrefs[randI], new Vector3(i, j), new Quaternion(), ground.transform);
                //}
            }
        }
        
        return ground;*/
    }
}