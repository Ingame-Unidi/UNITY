using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CutsceneCollisionTrigger : UdonSharpBehaviour
{
    [Tooltip("연출을 제어할 RollCameraSwitcher 스크립트가 붙은 오브젝트")]
    [SerializeField] private RollCameraSwitcher cutsceneManager;

    // 이 오브젝트에는 Collider가 Trigger로 설정되어 있어야 합니다.
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player.isLocal && cutsceneManager != null)
        {
            cutsceneManager.StartRollCutscene();
        }
    }
}
