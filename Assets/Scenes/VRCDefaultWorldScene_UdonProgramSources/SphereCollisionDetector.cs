using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SphereCollisionDetector : UdonSharpBehaviour
{
    [Tooltip("연출 종료를 제어할 RollCameraSwitcher 스크립트가 붙은 오브젝트")]
    [SerializeField] private RollCameraSwitcher cutsceneManager;
    
    [Tooltip("충돌 감지를 원하는 게임 오브젝트 (예: 바닥 오브젝트)")]
    [SerializeField] private GameObject targetObject;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SphereCollisionDetector: OnCollisionEnter 호출됨");
        // 충돌한 오브젝트가 targetObject와 동일한지 확인
        if (collision.gameObject == targetObject)
        {
            Debug.Log("SphereCollisionDetector: targetObject와 충돌 감지됨");
            if (cutsceneManager != null)
            {
                cutsceneManager.EndRollCutscene();
            }
        }
    }
}
