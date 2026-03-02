using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    [SerializeField] float launchForce = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement pc = other.GetComponent<Movement>();
            if (pc != null)
            {
                Vector3 launchDirection = transform.forward.normalized;
                pc.StartLaunch(launchDirection * launchForce);
            }
        }
    }
}
