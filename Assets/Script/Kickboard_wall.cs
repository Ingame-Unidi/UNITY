
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Kickboard_wall : UdonSharpBehaviour
{
    public AudioSource bumpAudio;

    public override void OnPlayerTriggerEnter(VRCPlayerApi playerApi)
    {
        Debug.Log("trigger enter!");

        bumpAudio.Play();
    }
}
