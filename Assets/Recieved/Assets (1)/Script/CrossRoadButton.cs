
using System.Threading;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CrossRoadButton : UdonSharpBehaviour
{
    // 차도 오브젝트
    public GameObject driveWay;

    // driveWay 비활성화(= 횡단보도 활성화)까지 기다리는 시간
    private float deactive_waitTime = 6f;

    // driveWay 활성화까지 기다리는 시간
    private float active_waitTime = 24f;
    private float timer = 0.0f;

    // 버튼 활성화 여부
    private bool isButtonActive = false;

    // 초록불 여부
    private bool isGreen = false;

    public override void Interact()
    {
        Debug.Log("CrossRoadButton Clicked!");

        // 안내음 출력
        GetComponent<AudioSource>().Play();

        // 차소리 volume down
        driveWay.GetComponent<AudioSource>().volume = 0.05f;

        // update문 내부 함수 동작 시작
        isButtonActive = true;
    }

    private void Update()
    {
        // 버튼 활성화 시, driveWay의 비활성화(= 횡단보도 활성화)를 위해 6초 대기
        if (isButtonActive)
        {
            Debug.Log("isButtonActive");

            // 타이머를 증가시킵니다.
            timer += Time.deltaTime;

            // 타이머가 waitTIme을 초과하면 
            if (timer >= deactive_waitTime)
            {
                // 횡단보도 setActive(false)
                driveWay.transform.GetChild(0).gameObject.SetActive(false);
                isButtonActive = false;

                // 초록불
                isGreen = true;

                // 타이머 초기화
                timer = 0.0f;
            }
        }

        // 횡단보도 활성화 시간
        if (isGreen)
        {
            Debug.Log("isGreen!");

            timer += Time.deltaTime;

            if (timer >= active_waitTime)
            {
                // driveWay 활성화(= 횡단보도 비활성화)
                driveWay.transform.GetChild(0).gameObject.SetActive(true);

                // driveWay volume 롤백
                driveWay.GetComponent<AudioSource>().volume = 0.3f;

                isGreen = false;

                // 타이머 초기화
                timer = 0.0f;
            }
        }

    }
}
