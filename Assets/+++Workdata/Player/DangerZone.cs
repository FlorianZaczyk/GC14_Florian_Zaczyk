using UnityEngine;

public class DangerZone : MonoBehaviour
{

    public float DamagePoints = 10f;
    private void OnTriggerEnter(Collider other)
    {
        Health H = other.GetComponent<Health>();

        if (H == null) return;
        
        H.HealthPoints -= DamagePoints * Time.deltaTime;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
