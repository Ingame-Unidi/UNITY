using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class RollCameraSwitcher : UdonSharpBehaviour
{
    [Header("카메라 설정")]
    [Tooltip("원래 사용하던 카메라 (예: VRChat 플레이어 카메라)")]
    [SerializeField] private Camera defaultCamera;
    [Tooltip("연출용 카메라 (예: 구 내부 카메라)")]
    [SerializeField] private Camera cineCameraObject;

    [Header("구 오브젝트 설정")]
    [Tooltip("굴러갈 구 오브젝트 (Rigidbody와 Collider가 있어야 함)")]
    [SerializeField] private GameObject rollingSphere;
    [Tooltip("구를 스폰할 위치 (계단 위에 배치)")]
    [SerializeField] private Transform sphereSpawnPosition;

    [Header("플레이어 위치 설정")]
    [Tooltip("(옵션) 연출 시작 시 플레이어를 이동시킬 시작 위치")]
    [SerializeField] private Transform startPosition;
    [Tooltip("연출 종료 시 플레이어를 이동시킬 위치")]
    [SerializeField] private Transform endPosition;

    


    // 연출 활성화 여부
    private bool isRollActive = false;

    void Start()
    {
        // 연출 시작 전, 구 오브젝트와 연출용 카메라는 비활성화합니다.
        if (rollingSphere != null)
        {
            rollingSphere.SetActive(false);
        }
        if (cineCameraObject != null)
        {
            cineCameraObject.enabled = false;
        }
    }

    /// <summary>
    /// 연출을 시작합니다.
    /// 플레이어를 startPosition으로 이동시키고, 기본 카메라는 비활성화, 연출용 카메라는 활성화합니다.
    /// </summary>
    public void StartRollCutscene()
    {
        GetComponent<MaterialChanger>().ChangeMaterials();
        GetComponent<AudioSource>().Play();
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        // 플레이어를 시작 위치로 이동 (옵션)
        if (localPlayer != null && startPosition != null)
        {
            localPlayer.TeleportTo(startPosition.position, startPosition.rotation);
        }

        // 기본 카메라는 비활성화하고 연출용 카메라는 활성화합니다.
        if (defaultCamera != null)
            defaultCamera.enabled = false;
        if (cineCameraObject != null)
            cineCameraObject.enabled = true;

        // 구 오브젝트를 스폰 위치로 이동시키고 활성화한 후, Rigidbody 초기화
        if (rollingSphere != null && sphereSpawnPosition != null)
        {
            rollingSphere.transform.position = sphereSpawnPosition.position;
            rollingSphere.transform.rotation = sphereSpawnPosition.rotation;
            rollingSphere.SetActive(true);

            Rigidbody rb = rollingSphere.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        isRollActive = true;
    }

    /// <summary>
    /// 연출 종료 시 호출됩니다.
    /// 기본 카메라를 활성화하고, 연출용 카메라와 구를 비활성화합니다.
    /// 플레이어를 endPosition으로 이동시킵니다.
    /// </summary>
    public void EndRollCutscene()
    {
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (localPlayer != null && endPosition != null)
        {
            localPlayer.TeleportTo(endPosition.position, endPosition.rotation);
        }
        
        if (rollingSphere != null)
            rollingSphere.SetActive(false);
        if (cineCameraObject != null)
            cineCameraObject.enabled = false;
        if (defaultCamera != null)
            defaultCamera.enabled = true;

        isRollActive = false;
    }

}
