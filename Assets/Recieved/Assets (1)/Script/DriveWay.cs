
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DriveWay : UdonSharpBehaviour
{
    // GameManager
    public GameManager GameManager;

    // driveWay 활성화 여부 체크용 object
    // activeSelf = true일 시, 횡단보도는 비활성화 상태
    // activeSelf = false일 시, 횡단보도는 활성화 상태
    public GameObject isActive;

    // player와 driveway 충돌 확인용 변수
    private bool isPlayerOnDriveWay = false;

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        Debug.Log("Player Enter the DriveWay!");

        isPlayerOnDriveWay = true;
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        Debug.Log("Player Exit the DriveWay!");

        isPlayerOnDriveWay = false;
    }

    private void Update()
    {
        if(isActive.activeSelf)
        {
            if (isPlayerOnDriveWay)
            {
                GameManager.playerDiedOnCrossRoad();
            }
        }
    }
}
