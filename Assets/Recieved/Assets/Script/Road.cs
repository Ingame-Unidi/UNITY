
using System.Collections;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Road : UdonSharpBehaviour
{
    // 현재 플레이어가 위치한 platform
    //public GameObject curPlayerPlatform;

    // sound 출력을 위한 platform 저장 변수
    public GameObject[] platforms;

    // road 활성화 여부 판단하는 child object
    // isActive가 setActive시 road 활성화로 판단
    public GameObject isActive;

    // 현재 위치한 platform의 index
    private int platformIndex = 0;

    // waitFewSec 구현용 변수
    private float interval = 1.5f;
    private float timer = 0.0f;
    private int playingIndex = 0;
    private bool isPlaying = false;

    //public void Start()
    //{
    //    int i = 0;
    //    foreach (Transform child in transform)
    //    {
    //        Debug.Log("child object " + i + ": " + child.name);
    //        Debug.Log(child.childCount);
    //        //Debug.Log("getchild test" + child.transform.GetChild(0));
    //        i++;
    //    }
    //}

    // 코루틴 사용 불가
    //IEnumerator WaitFewSec(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
    //}


    // player 위치 업데이트
    public void updatePlayerPos()
    {
        int cnt = 0;

        // player 위치 업데이트
        foreach (Transform child in transform)
        {
            // 첫번째 child인 isActive는 제외
            if (child.childCount == 0) continue;

            // 각 platform의 child(isActive)의 활성화 여부를 통해 현재 플레이어 위치 파악하기
            if (child.transform.GetChild(0).gameObject.activeSelf)
            {
                isActive.gameObject.SetActive(true);
                platformIndex = cnt;
                break;
            }
            else
            {
                isActive.gameObject.SetActive(false);
                cnt++;
            }
        }
    }

    // f key 입력 시
    public void FKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive.activeSelf)
            {
                Debug.Log(platformIndex);

                isPlaying = true;
                playingIndex = platformIndex;

                MakeSound();

                Debug.Log("Current player platform is " + gameObject.name + "/" + platforms[platformIndex].name);
                //curPlayerPlatform.GetComponent<AudioSource>().Play();
            }
        }
    }

    // playingIndex의 platform에서 소리내기
    public void MakeSound()
    {
        Debug.Log("playingIndex: " + playingIndex);

        // 현재 인덱스의 오브젝트의 AudioSource 컴포넌트를 가져옴
        AudioSource audioSource = platforms[playingIndex].GetComponent<AudioSource>();

        // 해당 플랫폼에 있는 MaterialChanger를 가져옴
        MaterialChanger materialChanger = platforms[playingIndex].GetComponent<MaterialChanger>();

        // 소리 재생과 메터리얼 변경을 동시에 실행
        if (audioSource != null && materialChanger != null)
        {
            // 소리 재생
            audioSource.Play();

            // 메터리얼 변경
            materialChanger.ChangeMaterials();
        }
        else
        {
            Debug.LogWarning($"Missing components on platform index {playingIndex}: AudioSource or MaterialChanger is null.");
        }

        // 다음 플랫폼으로 이동
        playingIndex++;

        // 종료 설정
        if (playingIndex >= platforms.Length)
        {
            isPlaying = false;
        }
    }


    public void Update()
    {
        updatePlayerPos();


        FKeyInput();


        if (isPlaying)
        {
            // 타이머를 증가시킵니다.
            timer += Time.deltaTime;

            // 타이머가 간격을 초과하면 AudioSource를 재생합니다.
            if (timer >= interval)
            {
                MakeSound();

                // 타이머 초기화
                timer = 0.0f;
            }
        }
    }
}
