using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class RollingSphere : UdonSharpBehaviour
{
    [Header("구 설정")]
    public GameObject sphereObject;

    [Header("카메라 설정")]
    [SerializeField] private GameObject defaultCamera;
    [SerializeField] private GameObject cineCameraObject;

    private bool isRolling = false;

    void Start()
    {
        sphereObject.SetActive(false);
        cineCameraObject.SetActive(false);
        defaultCamera.SetActive(true);
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        Debug.Log("플레이어가 트리거에 들어옴!"); // 디버깅 로그 추가
        if (player.isLocal && !isRolling) 
        {
            Debug.Log("카메라 전환 실행됨"); // 디버깅 로그 추가

            if (sphereObject != null && cineCameraObject != null && defaultCamera != null)
            {
                sphereObject.SetActive(true);
                sphereObject.transform.position = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;

                Rigidbody rb = sphereObject.GetComponent<Rigidbody>();

                // 🔹 카메라 전환
                defaultCamera.SetActive(false);
                cineCameraObject.SetActive(true);
                Debug.Log("카메라가 연출용으로 전환됨"); // 디버깅 로그 추가

                isRolling = true;
            }
        }
    }

    public void OnSphereHitFloor()
    {
        Debug.Log("구가 바닥에 닿음, 카메라 원래대로!"); // 디버깅 로그 추가

        isRolling = false;
        if (sphereObject != null) sphereObject.SetActive(false);

        // 🔹 카메라 원래대로 복구
        cineCameraObject.SetActive(false);
        defaultCamera.SetActive(true);
        Debug.Log("기본 카메라가 다시 활성화됨"); // 디버깅 로그 추가
    }
}
