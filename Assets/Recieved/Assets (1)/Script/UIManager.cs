
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class UIManager : UdonSharpBehaviour
{
    public GameObject death_UI;
    public GameObject end_UI;

    // ui off를 위해 n초 간 기다리기 위한 변수
    private bool is_waiting_death_UI = false;
    private float timer_for_death_UI = 0.0f;
    private float waitTime_for_death_UI = 3.0f;

    private bool is_waiting_end_UI = false;
    private float timer_for_end_UI = 0.0f;
    private float waitTime_for_end_UI = 3.0f;


    public void ShowDeathUI()
    {
        Debug.Log("UI setActive true!");
        // 사망 UI를 활성화합니다.
        death_UI.SetActive(true);

        is_waiting_death_UI = true;
    }

    public void ShowEndUI()
    {
        Debug.Log("end UI setActive true!");
        end_UI.SetActive(true);
        is_waiting_end_UI = true;
    }

    void Start()
    {
        // UI를 비활성화된 상태로 시작합니다.
        death_UI.SetActive(false);

        end_UI.SetActive(false);
    }

    private void Update()
    {
        // death ui
        if (is_waiting_death_UI)
        {
            // 경과 시간 증가
            timer_for_death_UI += Time.deltaTime;

            // 경과 시간이 ui_waitTime보다 크거나 같다면 ui 비활성화
            if (timer_for_death_UI >= waitTime_for_death_UI)
            {
                death_UI.SetActive(false);
                // 필요에 따라 스크립트를 비활성화하여 더 이상 Update가 호출되지 않도록 함
                is_waiting_death_UI = false;
                timer_for_death_UI = 0.0f;
                Debug.Log("UI setAcitve false!");
            }
        }

        // end ui
        if (is_waiting_end_UI)
        {
            // 경과 시간 증가
            timer_for_end_UI += Time.deltaTime;

            // 경과 시간이 ui_waitTime보다 크거나 같다면 ui 비활성화
            if (timer_for_end_UI >= waitTime_for_end_UI)
            {
                end_UI.SetActive(false);
                // 필요에 따라 스크립트를 비활성화하여 더 이상 Update가 호출되지 않도록 함
                is_waiting_end_UI = false;
                timer_for_end_UI = 0.0f;
                Debug.Log("end UI setAcitve false!");
            }
        }
    }
}
