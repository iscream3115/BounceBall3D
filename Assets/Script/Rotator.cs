using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] Vector3 RotDir;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotDir);
    }
}
