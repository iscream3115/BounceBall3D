using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    Vector3 StartPos;
    Vector3 EndPos;
    [SerializeField] float Speed;
    [SerializeField] Vector3 MoveVector;
    float moveFactor;


    void Start()
    {
        StartPos = transform.position;
        EndPos = StartPos + MoveVector;
    }

    // Update is called once per frame
    void Update()
    {
        moveFactor = Mathf.PingPong(Time.time * Speed,1.0f);
        transform.position = Vector3.Lerp(StartPos,EndPos,moveFactor);
    }
}
