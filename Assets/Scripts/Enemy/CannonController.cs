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

    private void Start()
    {
        // Find the player by tag or other means (e.g., using a reference if already assigned)
        player = GameObject.FindGameObjectWithTag("Player");
        currentProjectile = GameObject.FindGameObjectWithTag("Projectile");
        currentProjectile.transform.position = firePoint.position;
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
        Vector3 direction = (player.transform.position - firePoint.position).normalized;



        // Create a new projectile from the prefab
        //currentProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        currentProjectile.SetActive(true);


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
        if (currentProjectile != null && currentProjectile.activeSelf)
        {
            // Rotate the player's camera to look at the projectile
            player.transform.LookAt(currentProjectile.transform.position);

            // Lock the player's movements (you can implement this in your player controller script)
            // For example, if you have a player controller script, set a flag to lock movements
            //PlayerController playerController = FindObjectOfType<PlayerController>();
            if (player != null)
            {
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
    }
}
