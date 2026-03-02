using UnityEngine;

public class BreakableCollision : MonoBehaviour
{

    [SerializeField] GameObject EffectPrefab;
    [SerializeField] AudioClip BreakableSound;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(EffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null) Destroy(effect, ps.main.duration + ps.main.startLifetime.constantMax);

            AudioSource AS = other.gameObject.GetComponent<AudioSource>();

            AS.PlayOneShot(BreakableSound);

            Destroy(gameObject);
        }
    }

}
