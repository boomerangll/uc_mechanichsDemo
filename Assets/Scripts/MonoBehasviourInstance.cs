using UnityEngine;

public class MonoBehasviourInstance : MonoBehaviour {
    public static MonoBehasviourInstance instance;

    public void Awake () {
        instance = this;
    }
}
