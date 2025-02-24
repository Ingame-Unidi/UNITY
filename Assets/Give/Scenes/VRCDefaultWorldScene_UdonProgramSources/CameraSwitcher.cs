

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using Cinemachine;

public class CameraSwitcher : UdonSharpBehaviour
{
    [Header("카메라 오브젝트 설정")]
    [Tooltip("원래 사용하던 카메라 (예: VRChat 플레이어 카메라)")]
    [SerializeField] private GameObject defaultCamera;
    
    [Tooltip("시네머신 연출용 카메라가 포함된 GameObject")]
    [SerializeField] private GameObject cineCameraObject;

    [Header("Cinemachine 구성 요소")]
    [Tooltip("시네머신 Dolly Cart (경로를 따라 이동하는 컴포넌트)")]
    [SerializeField] private CinemachineDollyCart dollyCart;

    [Header("경로 종료 기준")]
    [Tooltip("Dolly Cart의 m_Position 값이 이 값 이상이면 경로가 끝난 것으로 판단")]
    [SerializeField] private float pathEndThreshold = 100f; // Inspector에서 경로 길이에 맞게 설정

    [Header("플레이어 시작 위치")]
    [Tooltip("플레이어를 이동시킬 빈 오브젝트의 Transform (시네머신 연출 시작 시 사용)")]
    [SerializeField] private Transform startPosition;

    [Header("플레이어 종료 위치")]
    [Tooltip("경로 종료 시 플레이어를 이동시킬 빈 오브젝트의 Transform")]
    [SerializeField] private Transform endPosition;

    // 시네머신 연출 활성화 여부
    private bool isCineActive = false;

    void Start()
    {
        // 씬 시작 시 dollyCart를 비활성화하고, 시작 위치를 0으로 설정하여 자동으로 움직이지 않도록 합니다.
        if (dollyCart != null)
        {
            dollyCart.enabled = false;
            dollyCart.m_Position = 0f;
        }
    }

    void Update()
    {
        // 시네머신 연출이 활성 상태이고 dollyCart가 할당되어 있을 경우,
        // dollyCart.m_Position 값이 설정한 종료 임계값(pathEndThreshold) 이상이면 기본 카메라로 전환합니다.
        if (isCineActive && dollyCart != null)
        {
            if (dollyCart.m_Position >= pathEndThreshold)
            {
                SwitchToDefaultCamera();
            }
        }
    }

    /// <summary>
    /// 시네머신 연출을 시작합니다.
    /// 플레이어의 위치를 startPosition으로 이동시킨 후, dollyCart의 위치를 초기화 및 활성화하여 경로 이동을 시작합니다.
    /// </summary>
    public void StartCineCamera()
    {
        // 플레이어를 startPosition의 위치와 회전으로 이동시킵니다.
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (localPlayer != null && startPosition != null)
        {
            localPlayer.TeleportTo(startPosition.position, startPosition.rotation);
        }
        
        // 시네머신 카메라 오브젝트를 활성화하고, 기본 카메라는 비활성화합니다.
        if (cineCameraObject != null)
            cineCameraObject.SetActive(true);
        if (defaultCamera != null)
            defaultCamera.SetActive(false);

        // dollyCart의 위치를 0으로 초기화한 후 활성화하여 경로 이동을 시작합니다.
        if (dollyCart != null)
        {
            dollyCart.m_Position = 0f;
            dollyCart.enabled = true;
        }
        isCineActive = true;
    }

    /// <summary>
    /// 경로의 끝(설정한 임계값에 도달하면) 기본 카메라로 전환하고, 동시에 플레이어를 endPosition으로 이동시킵니다.
    /// </summary>
    private void SwitchToDefaultCamera()
    {
        // 플레이어를 endPosition의 위치와 회전으로 이동시킵니다.
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (localPlayer != null && endPosition != null)
        {
            localPlayer.TeleportTo(endPosition.position, endPosition.rotation);
        }
        
        // 시네머신 카메라 오브젝트를 비활성화하고, 기본 카메라를 활성화합니다.
        if (cineCameraObject != null)
            cineCameraObject.SetActive(false);
        if (defaultCamera != null)
            defaultCamera.SetActive(true);

        // dollyCart를 비활성화하여 움직임을 멈춥니다.
        if (dollyCart != null)
        {
            dollyCart.enabled = false;
        }
        isCineActive = false;
    }

    public override void OnPlayerTriggerEnter (VRCPlayerApi player) 
    {
        StartCineCamera();
        GetComponent<AudioSource>().Play();
    }
}


