using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    private Vector3 touchStart;

    [Header("카메라 이동 제한 설정")]
    public float minX; // 왼쪽 끝
    public float maxX; // 오른쪽 끝
    public float minY; // 아래쪽 끝
    public float maxY; // 위쪽 끝

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            touchStart = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;

            Vector3 currentPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 direction = touchStart - currentPos;
            Vector3 targetPos = new Vector3(transform.position.x + direction.x, transform.position.y, -10f);

            targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);

            transform.position = targetPos;
        }
    }
}
