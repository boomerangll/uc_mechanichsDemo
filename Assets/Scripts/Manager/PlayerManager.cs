using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    public GameObject player;                // The enemy prefab to be spawned.
    public Transform spawnLocation;         // An array of the spawn points this enemy can spawn from.

    void Start() {
        Spawn();
    }

    private void Spawn() {
        Instantiate(player, spawnLocation.position, spawnLocation.rotation);
    }
}
