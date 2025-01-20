
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GameManager : UdonSharpBehaviour
{
    private VRCPlayerApi localPlayer;
    public GameObject crossRoad_respawnPoint;
    public GameObject uiManager;

    // 횡단보도에서 player 사망 처리
    public void playerDiedOnCrossRoad()
    {
        if (localPlayer != null)
        {
            Debug.Log("Player died");

            // player respawn
            respawnPlayer();

            // 사망 ui 띄우기
            uiManager.GetComponent<UIManager>().ShowDeathUI();
        }
    }

    // player respawn
    public void respawnPlayer()
    {
        if (localPlayer != null)
        {
            Debug.Log("Player respawn");
            localPlayer.TeleportTo(crossRoad_respawnPoint.transform.position, crossRoad_respawnPoint.transform.rotation);
        }
    }

    void Start()
    {
        // LocalPlayer를 가져옵니다.
        localPlayer = Networking.LocalPlayer;

        // LocalPlayer가 유효한지 확인합니다.
        if (localPlayer != null)
        {
            Debug.Log("LocalPlayer found: " + localPlayer.displayName);
        }
        else
        {
            Debug.Log("LocalPlayer not found.");
        }
    }
}
