using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed = 1f;
    [SerializeField] float startDelay = 0f;

    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;
    float startTime;


    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;

    }

    void Update()
    {
        if (Time.time < startTime)
        {
            return;
        }
        movementFactor = Mathf.PingPong((Time.time - startTime) * speed, 1);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);


    }
    void OnEnable()
    {
        startTime = Time.time + startDelay;

    }
}
