using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFit : MonoBehaviour
{
    public float targetMapWidth = 20f; // 맵의 양 끝 가로 거리(직접 수치 조정 필요)

    void Start()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        Camera cam = GetComponent<Camera>();

        // 1. 현재 화면의 비율(Aspect Ratio) 계산
        float screenAspect = (float)Screen.width / (float)Screen.height;

        // 2. 맵 가로 길이를 기준으로 카메라 높이 계산
        // 공식: orthographicSize = (TargetWidth / 2) / AspectRatio
        cam.orthographicSize = (targetMapWidth / 2f) / screenAspect;
    }
}
