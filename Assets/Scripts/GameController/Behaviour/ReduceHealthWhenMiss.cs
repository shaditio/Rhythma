using UnityEngine;

public class ReduceHealthWhenMiss : MonoBehaviour
{
    public int reducedAmount;
    public HealthManager healthManager;

    void Start()
    {
        if (healthManager == null)
        {
            this.healthManager = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MissBox")
        {
            this.healthManager.health -= this.reducedAmount;
            Destroy(this.gameObject);
        }
    }
}