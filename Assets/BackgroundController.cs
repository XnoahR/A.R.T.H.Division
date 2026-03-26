using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
   public List<BackgroundLayer> backgrounds = new List<BackgroundLayer>();
   public Camera mainCamera;
   Vector3 previousCameraPosition;
   Vector3 currentCameraPosition;

    void Start()
    {
        previousCameraPosition = mainCamera.transform.position;
        currentCameraPosition = mainCamera.transform.position;
    }

    void LateUpdate()
    {
        currentCameraPosition = mainCamera.transform.position;
        float cameraDelta = currentCameraPosition.x - previousCameraPosition.x;
        foreach(BackgroundLayer layer in backgrounds)
        {
            float moveAmount = cameraDelta * layer.parallaxFactor;
            Vector3 pos = layer.position.position;
            pos.x += moveAmount;
            layer.position.position = pos;
        }
        previousCameraPosition = currentCameraPosition;
    }

}

[Serializable]
public class BackgroundLayer
{
    public Transform position;
    public float parallaxFactor;
}
