using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine;

public class Kickboard : UdonSharpBehaviour
{
    public override void OnPickup()
    {
        // 플레이어가 오브젝트를 잡기 시작할 때 실행
        Debug.Log("오브젝트가 잡혔습니다.");
    }

    public override void OnDrop()
    {
        // 플레이어가 오브젝트를 놓았을 때 실행
        Debug.Log("오브젝트가 놓였습니다.");
    }
}
