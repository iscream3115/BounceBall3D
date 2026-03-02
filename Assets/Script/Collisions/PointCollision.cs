using UnityEngine;


public class PointCollision : MonoBehaviour
{
    [SerializeField] GameObject itemEffectPrefab;
    [SerializeField] AudioClip ItemSound;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            GameManageScript.instance.PointCollected();

            GameObject effect = Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null) Destroy(effect, ps.main.duration + ps.main.startLifetime.constantMax);

            AudioSource AS = other.gameObject.GetComponent<AudioSource>();

            AS.PlayOneShot(ItemSound);

            Destroy(gameObject);

        }

    }

}
