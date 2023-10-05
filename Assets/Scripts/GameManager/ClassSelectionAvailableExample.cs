using Gtec.UnityInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Gtec.UnityInterface.BCIManager;

public class ClassSelectionAvailableExample : MonoBehaviour
{
    private uint _selectedClass = 0;
    private bool _update = false;
    public string gateTag = "Gate";

    public CVEPFlashController3D _flashController;
    private Dictionary<int, Renderer> _selectedObjects;

    private GameObject player;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        //attach to class selection available event
        BCIManager.Instance.ClassSelectionAvailable += OnClassSelectionAvailable;

        _selectedObjects = new Dictionary<int, Renderer>();
        List<CVEPFlashObject3D> applicationObjects = _flashController.ApplicationObjects;
        foreach(CVEPFlashObject3D applicationObject in applicationObjects)
        {
            Renderer[] renderers = applicationObject.GameObject.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                if(renderer.name.Equals("selected"))
                {
                    _selectedObjects.Add(applicationObject.ClassId, renderer);
                }
            }
        }
       

        // disable selected objects by default
        foreach(KeyValuePair<int, Renderer> kvp in _selectedObjects)
        {
            Debug.Log(kvp);
            kvp.Value.gameObject.SetActive(false);
        }
    }

    void OnApplicationQuit()
    {
        //detach from class selection available event
        BCIManager.Instance.ClassSelectionAvailable -= OnClassSelectionAvailable;
    }

    void Update()
    {
        //TODO ADD YOUR CODE HERE
        if(_update)
        {
            GameObject player = GameObject.FindWithTag("Player");
            switch (_selectedClass)
            {
                
                case 0:
                    break;
                case 1:
                    try 
                    {
                        GameObject gateObject = GameObject.FindWithTag(gateTag);
                        
                        if(gateObject != null)
                        {
                            Gate nearestGate = gateObject.GetComponent<Gate>();
                        
                            // You found the object, you can now work with it
                            Debug.Log("Found object with tag: " + nearestGate);
                        
                            nearestGate.Open(player.transform.position);
                        }
                    } 
                    catch (Exception e)
                    {
                        Debug.Log(e + " No Gate object found");
                    }
                    break;
                case 2:
                    
                    break;
                case 3:
                    break;
                case 4:
                    GameObject currentProjectile = GameObject.FindGameObjectWithTag("Projectile");
                     // Calculate the direction opposite to the player's facing direction
                    Vector3 oppositeDirection = -player.transform.forward;

                    // Determine a random direction (left or right)
                    Vector3 randomDirection = (UnityEngine.Random.Range(0, 2) == 0) ? Quaternion.Euler(0, 45f, 0) * oppositeDirection : Quaternion.Euler(0, -45f, 0) * oppositeDirection;

                    // Call the Bounce method to make the projectile bounce
                    currentProjectile.GetComponent<BounceProjectile>().Bounce(randomDirection, 1f); // Adjust the force as needed
                    GameObject bulletTime = GameObject.FindGameObjectWithTag("BulletTime");
                    // bulletTime.GetComponent<BulletTimeZone>().ResetBulletTime();
                    break;
                case 5:
                    break;
                case 6:
                    break;

            }
            // disable previously selected objects
            foreach(KeyValuePair<int, Renderer> kvp in _selectedObjects)
            {
                kvp.Value.gameObject.SetActive(false);
            }

            // select currently selected object; select none if zero class was selected
            if(_selectedClass > 0 && _selectedClass <= 4)
            {
                _selectedObjects[(int) _selectedClass].gameObject.SetActive(true);
                
            }
            _update = false;
        } 
    }

    /// <summary>
    /// This event is called whenever a new class selection is available. Th
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClassSelectionAvailable(object sender, EventArgs e)
    {
        ClassSelectionAvailableEventArgs ea = (ClassSelectionAvailableEventArgs)e;
       _selectedClass = ea.Class;
        _update = true;
        if (ea.Class != 0)
        {
            Debug.Log(string.Format("Selected class: {0}", ea.Class));
        }
    }
}
