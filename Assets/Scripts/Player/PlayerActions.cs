using UnityEngine;
using TMPro;


public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro UseText;

    [SerializeField]
    private Transform Camera;

    [SerializeField]
    private float MaxUseDistance = 5f;

    [SerializeField]
    private LayerMask UseLayers;

    [SerializeField]
    private bool MovingEnabled = true;

   // private FirstPersonController controller;

    public void OnUse()
    {
        if(Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            Debug.Log("OnUse - Raycast");

            if(hit.collider.TryGetComponent<Gate>(out Gate gate))
            {
                Debug.Log("OnUse - Hit Gate");
                if(gate.IsOpen)
                {
                    gate.Close();
                }
                else 
                {
                    gate.Open(transform.position);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
       // controller = player.GetComponent<FirstPersonController>();
    }   

    // Update is called once per frame
    private void Update()
    {

        if (!MovingEnabled)
        {
            // Implement your normal player movement code here
            // For example, WASD or touch input
            //controller.SetActive(false);
            
        }

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers) 
        && hit.collider.TryGetComponent<Gate>(out Gate gate))
        {
            if(gate.IsOpen)
            {
                UseText.SetText("Close \"E\"");
            }
            else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.1f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        } 
        else 
        {
            if(UseText != null)
            {
                UseText.gameObject.SetActive(false);
            }
        }
    }
}
