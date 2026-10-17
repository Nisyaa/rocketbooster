using UnityEngine;
using UnityEngine.Events;


public class FallingOnTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onFallComplete;

    Rigidbody rb;

    bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }



    public void StartFalling()
    {
        if (!hasFallen)
        {
            rb.isKinematic = false;
            hasFallen = true;

        }
    }


}
