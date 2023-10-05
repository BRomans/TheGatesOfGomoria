using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour
{
    public GameObject projectilePrefab;  // Reference to the projectile prefab
    public Transform firePoint;           // Point where the projectile is spawned
    public float fireForce = 10f;         // Force applied to the projectile

    public float projectileLifeTime = 5f;

    private GameObject player;    // Reference to the player's transform
    
    private GameObject currentProjectile;    

    private Quaternion playerRotation;    

    public bool isBulletTimeActive = false; 

    private void Start()
    {
        // Find the player by tag or other means (e.g., using a reference if already assigned)
        player = GameObject.FindGameObjectWithTag("Player");
        currentProjectile = GameObject.FindGameObjectWithTag("Projectile");
        playerRotation = player.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShootAtPlayer();
        }
    }

    public void ShootAtPlayer()
    {

        // Calculate the direction towards the player
        currentProjectile.transform.position = firePoint.position;
        Vector3 direction = (player.transform.position - firePoint.position).normalized;



        // Create a new projectile from the prefab
        //currentProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        currentProjectile.SetActive(true);
        currentProjectile.transform.LookAt(player.transform.position);


        // Get the rigidbody of the projectile and apply force in the calculated direction
        Rigidbody rb = currentProjectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * fireForce, ForceMode.Impulse);

        // Destroy the projectile after a certain time (adjust as needed), instead of destroying it, we reset the bullet
        //Destroy(currentProjectile, projectileLifeTime);
        StartCoroutine(ResetBullet(projectileLifeTime));
    }

    private void Update()
    {
        // Check if there's an active projectile
        if (currentProjectile != null && isBulletTimeActive)
        {

            // Lock the player's movements (you can implement this in your player controller script)
            if (player != null)
            {
                // Rotate the player's camera to look at the projectile
                player.transform.LookAt(currentProjectile.transform.position);
                //Debug.Log("Locking player");
                //playerController.LockMovement(true);
            }
        }
        else
        {
            // Unlock the player's movements when there's no active projectile
            //PlayerController playerController = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                //Debug.Log("Unlocking player");
                //playerController.LockMovement(false);
            }
        }
    }

    private IEnumerator ResetBullet(float delayInSeconds)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        currentProjectile.SetActive(false);
        currentProjectile.transform.position = firePoint.position;
        currentProjectile.transform.rotation = Quaternion.identity;
        ResetObjectForces(currentProjectile.GetComponent<Rigidbody>());
        
    }

    private void ResetObjectForces(Rigidbody rb)
    {
        // Reset the object's velocity to zero to stop linear motion
        rb.velocity = Vector3.zero;

        // Reset the object's angular velocity to zero to stop rotation
        rb.angularVelocity = Vector3.zero;
    }
}
