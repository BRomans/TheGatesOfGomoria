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
    void Start()
    {
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
            
            switch (_selectedClass)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    
                    break;
                case 3:
                    break;
                case 4:
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
            if(_selectedClass > 0 && _selectedClass < 4)
            {
                _selectedObjects[(int) _selectedClass].gameObject.SetActive(true);
                try 
                {
                    Gate nearestGate = GameObject.FindWithTag(gateTag).GetComponent<Gate>();
                    GameObject player = GameObject.FindWithTag("Player");
                    // You found the object, you can now work with it
                    Debug.Log("Found object with tag: " + nearestGate);
                    nearestGate.Open(player.transform.position);
                } 
                catch (Exception e)
                {
                    Debug.Log(e + " No Gate object found");
                }
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