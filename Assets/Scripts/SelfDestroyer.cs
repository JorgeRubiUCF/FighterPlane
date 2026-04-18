using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float destructionTime;
    void Start()
    {
        Destroy(this.gameObject, destructionTime);
    }

}
