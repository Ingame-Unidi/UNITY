
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class Cube : UdonSharpBehaviour
{
    // platform 활성화 여부 확인하는 child object
    // isActive가 setActive시 platform 활성화로 판단
    public GameObject isActive;

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player.IsValid())
        {
            if (player.isLocal)
            {
                // 테스트용 audio
                //AudioSource enterSound = GetComponent<AudioSource>();
                //enterSound.Play();

                // mesh renderer 켜기 (시각화를 위한 코드, 나중에 삭제해도 됨)
                MeshRenderer curPlayerPos = gameObject.GetComponent<MeshRenderer>();
                curPlayerPos.enabled = true;

                // 자식 컴포넌트의 setActive 여부로 플레이어의 위치 정보 파악하기
                transform.GetChild(0).gameObject.SetActive(true);

                // test용 log
                Debug.Log("Local Player collider ENTER!");
            }
        }
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (player.IsValid())
        {
            if (player.isLocal)
            {
                // 단순 시각화용 코드
                MeshRenderer curPlayerPos = gameObject.GetComponent<MeshRenderer>();
                curPlayerPos.enabled = false;

                // 자식 컴포넌트의 setActive 여부로 플레이어의 위치 정보 파악하기
                transform.GetChild(0).gameObject.SetActive(false);

                // test용 log
                Debug.Log("Local Player collider EXIT!");
            }
        }
    }
}