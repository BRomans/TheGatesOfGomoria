using UnityEngine;

public class InputManager : MonoBehaviour
{

    private GameObject player;
    private GameObject currentProjectile;  
 
    private void Start()
    {
        currentProjectile = GameObject.FindGameObjectWithTag("Projectile");
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(currentProjectile);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Left mouse button click
        {
             // Calculate the direction opposite to the player's facing direction
            Vector3 oppositeDirection = -player.transform.forward;

            // Determine a random direction (left or right)
            Vector3 randomDirection = (Random.Range(0, 2) == 0) ? Quaternion.Euler(0, 45f, 0) * oppositeDirection : Quaternion.Euler(0, -45f, 0) * oppositeDirection;

            // Call the Bounce method to make the projectile bounce
            currentProjectile.GetComponent<BounceProjectile>().Bounce(randomDirection, 1f); // Adjust the force as needed
            GameObject bulletTime = GameObject.FindGameObjectWithTag("BulletTime");
            bulletTime.GetComponent<BulletTimeZone>().ResetBulletTime();
        }
    }
}