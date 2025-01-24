using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody rb;
    private float size;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
    }
    
    private void Update()
    {
        if (rb.isKinematic) return;
        
        size = transform.localScale.x;

        if (size < 1.5f)
        {
            rb.useGravity = false;
            rb.AddForce(Vector3.up * 2.0f, ForceMode.Acceleration);
        }
        else if (size < 3.0f)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        rb.constraints =  RigidbodyConstraints.FreezeRotation;
    }
}