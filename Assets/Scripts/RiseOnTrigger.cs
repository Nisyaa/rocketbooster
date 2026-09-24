using UnityEngine;
using UnityEngine.Events;


public class RisingCrystal : MonoBehaviour
{
    [SerializeField] UnityEvent onRiseComplete;
    [SerializeField] float riseHeight = 6f;
    [SerializeField] float riseSpeed = 20f;
    Vector3 startPosition;
    Vector3 endPosition;
    bool hasRisen = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.up * riseHeight;
    }

    // Update is called once per frame
    void Update()
    {

        if (hasRisen && transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, riseSpeed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                onRiseComplete?.Invoke();
            }
        }

    }

    public void StartRising()
    {
        if (!hasRisen)
        {
            hasRisen = true;

        }
    }


}
