using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FloorTrigger : UdonSharpBehaviour
{
    public RollingSphere activateSphere;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == activateSphere.sphereObject)
        {
            activateSphere.OnSphereHitFloor();
        }
    }
}
