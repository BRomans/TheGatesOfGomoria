using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToCamera : MonoBehaviour
{

    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;

        if (mainCamera != null)
        {
            attachCamera(mainCamera);
        }
        else
        {
            Debug.LogWarning("Main camera not found in the scene.");
        }
    }

    void Update()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
            attachCamera(mainCamera);
        }
    }

    void attachCamera(Camera mainCamera)
    {
        try 
            {
                // Find the Canvas component on the same GameObject
                Canvas canvas = GetComponent<Canvas>();

                if (canvas != null)
                {
                    // Attach the main camera to the Canvas's Render Camera
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.worldCamera = mainCamera;

                    Debug.Log("Attached main camera to Canvas.");
                }
                else
                {
                    Debug.LogWarning("Canvas component not found on the GameObject.");
                }
            } catch (Exception e) {
                {
                    Debug.LogWarning(e);
                }
            }
    }
}

