using UnityEngine;

public class BounceProjectile : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Bounce(Vector3 direction, float force)
    {
        // Add an impulse force to the projectile in the specified direction
        Debug.Log("Bounce it!");
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
}
