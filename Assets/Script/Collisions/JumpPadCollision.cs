using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] float JumpPower;

    [SerializeField] AudioClip JumpPadSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource AS = other.gameObject.GetComponent<AudioSource>();
            AS.PlayOneShot(JumpPadSound);
            
            Movement.instance.ActiveJumpPad(gameObject.transform.up, JumpPower);

            

        }

    }
}
