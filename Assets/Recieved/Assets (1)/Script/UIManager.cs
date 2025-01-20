
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class UIManager : UdonSharpBehaviour
{
    public GameObject deathUI;

    // ui off를 위해 n초 간 기다리기 위한 변수
    private bool isWaiting = false;
    private float timer = 0.0f;
    private float ui_waitTime = 3.0f;

    public void ShowDeathUI()
    {
        Debug.Log("UI setActive true!");
        // 사망 UI를 활성화합니다.
        deathUI.SetActive(true);

        isWaiting = true;
    }

    void Start()
    {
        // UI를 비활성화된 상태로 시작합니다.
        deathUI.SetActive(false);
    }

    private void Update()
    {
        if (isWaiting)
        {
            // 경과 시간 증가
            timer += Time.deltaTime;

            // 경과 시간이 ui_waitTime보다 크거나 같다면 ui 비활성화
            if (timer >= ui_waitTime)
            {
                deathUI.SetActive(false);
                // 필요에 따라 스크립트를 비활성화하여 더 이상 Update가 호출되지 않도록 함
                isWaiting = false;
                timer = 0.0f;
                Debug.Log("UI setAcitve false!");
            }
        }
    }
}
