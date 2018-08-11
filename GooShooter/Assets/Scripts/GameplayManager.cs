using UnityEngine;
public class GameplayManager :MonoBehaviour {
    public static GameplayManager m;

    public Transform player;

    private void Awake() {
        m = this;
    }
}
