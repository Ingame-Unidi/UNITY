
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GameManager : UdonSharpBehaviour
{
    private VRCPlayerApi localPlayer;
    public GameObject crossRoad_respawnPoint;
    public GameObject uiManager;

    // driveway용 audio
    public AudioSource driveWayAudio;
    public AudioSource deadAudio;

    // button line색 변경용 param
    public GameObject crossRoad_button_line;
    public GameObject elevator_button_line;

    // 횡단보도에서 player 사망 처리
    public void playerDiedOnCrossRoad()
    {
        if (localPlayer != null)
        {
            Debug.Log("Player died");

            // 사망 ui 띄우기
            uiManager.GetComponent<UIManager>().ShowDeathUI();

            // 사망 audio 플레이
            deadAudio.Play();

            // 5초 후 리스폰을 위해 현재 시간 저장
            //respawnTime = Time.time + 5f;
            respawnPlayer();
        }
    }

    // n초 뒤 리스폰 기능, 나중에 필요하면 쓰기로
    //private void Update()
    //{
    //    // respawnTime이 설정되었고, 현재 시간이 넘어가면 리스폰 실행
    //    if (respawnTime > 0 && Time.time >= respawnTime)
    //    {
    //        respawnPlayer();
    //        respawnTime = -1f; // 다시 대기 상태로 설정
    //    }
    //}

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

        crossRoad_button_line.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green * Mathf.LinearToGammaSpace(500));
        elevator_button_line.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green * Mathf.LinearToGammaSpace(500));
    }
}
