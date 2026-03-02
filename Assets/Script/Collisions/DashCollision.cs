using UnityEngine;

public class DashCollision : MonoBehaviour
{
    [SerializeField] float DashPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Movement.instance.ActiveDash(DashPower);
            Destroy(gameObject);
        }
    }

}
