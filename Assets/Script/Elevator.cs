
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Elevator : UdonSharpBehaviour
{
    public AudioSource elevatorSound;
    public AudioSource endingSound;

    public UIManager UIManager;

    public override void Interact()
    {
        Debug.Log("Game End");

        // 기존 음악 중지 및 엔딩 음악 플레이
        elevatorSound.Stop();
        endingSound.Play();

        UIManager.ShowEndUI();
    }
}
