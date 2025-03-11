using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// enum 타입을 클래스 외부로 이동
public enum State
{
    Idle,
    Increasing,
    Holding,
    Decreasing
}

public class MaterialChanger : UdonSharpBehaviour
{
    public GameObject[] targetObjects; // 색을 변경할 오브젝트 목록
    public Material defaultMaterial; // 기본 메터리얼
    public Material emissionMaterial; // 빛이 나는 메터리얼
    public float intensityIncrease = 2.0f; // 밝기 증가량
    public float increaseDuration = 2.0f; // 서서히 밝아지는 시간
    public float holdDuration = 1.0f; // 밝기 유지 시간
    public float decreaseDuration = 2.0f; // 서서히 밝기가 줄어드는 시간

    private bool isActive = false; // 밝기 조절 중 여부
    private float elapsedTime = 0f; // 경과 시간
    private State currentState = State.Idle; // 현재 상태

    void Start()
    {
        // 초기 메터리얼을 기본 메터리얼로 설정
        foreach (var obj in targetObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = defaultMaterial;
            }
        }
    }

    void Update()
    {
        if (isActive)
        {
            elapsedTime += Time.deltaTime;

            switch (currentState)
            {
                case State.Increasing:
                    // 서서히 밝기를 증가시킵니다.
                    float targetIntensity = Mathf.Lerp(0f, intensityIncrease, elapsedTime / increaseDuration);
                    foreach (var obj in targetObjects)
                    {
                        Renderer renderer = obj.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material.SetColor("_EmissionColor", Color.yellow * Mathf.LinearToGammaSpace(targetIntensity));
                        }
                    }

                    if (elapsedTime >= increaseDuration)
                    {
                        currentState = State.Holding;
                        elapsedTime = 0f;
                    }
                    break;

                case State.Holding:
                    // 밝기를 유지합니다.
                    foreach (var obj in targetObjects)
                    {
                        Renderer renderer = obj.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material.SetColor("_EmissionColor", Color.yellow * Mathf.LinearToGammaSpace(intensityIncrease));
                        }
                    }

                    if (elapsedTime >= holdDuration)
                    {
                        currentState = State.Decreasing;
                        elapsedTime = 0f;
                    }
                    break;

                case State.Decreasing:
                    // 서서히 밝기를 감소시킵니다.
                    targetIntensity = Mathf.Lerp(intensityIncrease, 0f, elapsedTime / decreaseDuration);
                    foreach (var obj in targetObjects)
                    {
                        Renderer renderer = obj.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material.SetColor("_EmissionColor", Color.yellow * Mathf.LinearToGammaSpace(targetIntensity));
                        }
                    }

                    if (elapsedTime >= decreaseDuration)
                    {
                        // 밝기 감소 완료 후 기본 메터리얼로 복원
                        foreach (var obj in targetObjects)
                        {
                            Renderer renderer = obj.GetComponent<Renderer>();
                            if (renderer != null)
                            {
                                renderer.material = defaultMaterial;
                            }
                        }

                        currentState = State.Idle;
                        isActive = false;
                        elapsedTime = 0f;
                    }
                    break;
            }
        }
    }

    public void ChangeMaterials()
    {
        if (targetObjects == null || targetObjects.Length == 0) return;

        // Emission 메터리얼로 전환
        foreach (var obj in targetObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = emissionMaterial;

                // 초기 Emission 값을 설정
                emissionMaterial.SetColor("_EmissionColor", Color.black);
            }
        }

        isActive = true;
        elapsedTime = 0f;
        currentState = State.Increasing;
    }
}
