using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine;

public class Kickboard : UdonSharpBehaviour
{
    public GameObject wall;

    // 킥보드의 지정 위치
    // 해당 위치를 벗어나면 벽이 열림
    public GameObject designated_position;


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == designated_position)
        {
            wall.SetActive(false);

            Debug.Log("벽이 열렸습니다!");
        }
    }


    public override void OnPickup()
    {
        
    }

    public override void OnDrop()
    {
       
    }
}
