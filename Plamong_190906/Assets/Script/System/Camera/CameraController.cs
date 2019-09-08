using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라 참조
    [SerializeField]
    private Camera cam;
    [Header("Custom Screen Value")]
    // 픽셀 퍼펙트를 위한 해상도, 실제 해상도
    [SerializeField]
    private int pixelWidth;
    [SerializeField]
    private int pixelHeight;
    [SerializeField]
    private int screenWidth;
    [SerializeField]
    private int screenHeight;
    [SerializeField]
    private bool IsFullScreen;


    // 마우스 움직임 함수 대리자 선언
    private delegate void MoveCamera();
    // 마우스 움직임 함수 대리자
    private MoveCamera moveCamera;
    // 카메라 모드 열거형
    public enum CameraMode { Mouse, Player, Free };
    [Header("Camera Mode")]
    [SerializeField]
    private CameraMode cameraMode;

    [Header("Mouse mode setting")]
    // 카메라 제한 거리
    [SerializeField]
    private float maxMouseDistance;
    // 카메라가 플레이어를 추적하는 속도
    [SerializeField]
    private float cameraMoveSpeed;
    // 최초의 카메라 y 좌표
    [SerializeField]
    private float firstY;
    // 화면 중심 좌표
    [SerializeField]
    private Vector3 screenCenter;

    // 플레이어 위치 트랜스폼
    [SerializeField]
    private Vector3 playerPos;


    // 카메라가 추적하는 대상
    [Header("Target mode setting")]
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        cam = transform.GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        firstY = 10f;
        SetOrthographicSize();
        SetResoultion(screenWidth, screenHeight, IsFullScreen);
        moveCamera = new MoveCamera(MouseMode);
    }

    private void LateUpdate()
    {
        moveCamera();
    }

    // 카메라 orthographic 사이즈 조정
    void SetOrthographicSize()
    {
        float size = pixelWidth / ((((float)pixelWidth / pixelHeight) * 2f) * 32f);
        cam.orthographicSize = size;
    }
    // 화면 해상도 조절
    void SetResoultion(int width, int height, bool IsFull)
    {
        screenWidth = width;
        screenHeight = height;
        IsFullScreen = IsFull;
        screenCenter = new Vector3(Screen.width, 0f, Screen.height) * 0.5f;
        Screen.SetResolution(screenWidth, screenHeight, IsFullScreen);
    }
    // 카메라의 모드를 설정합니다.
    void SetCameraMode(CameraMode mode)
    {
        switch(mode)
        {
            case CameraMode.Mouse:
                moveCamera = new MoveCamera(MouseMode);
                break;
            case CameraMode.Player:
                moveCamera = new MoveCamera(TargetMode);
                break;
            case CameraMode.Free:
                moveCamera = new MoveCamera(FreeMode);
                break;
        }
    }
    // 마우스 중심으로 카메라를 제어합니다. 단, 플레이어로부터 일정 범위를 벗어나지 않습니다.
    void MouseMode()
    {
        // 플레이어의 위치 갱신
        playerPos = PlayerController.player.transform.position;
        //해상도 보정
        float screenCorrection =  (float)pixelWidth / screenWidth;
        // 마우스 좌표
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
        // 마우스 벡터 - 화면 중앙 벡터
        Vector3 direction = (mousePosition - screenCenter) * screenCorrection;
        // 총 벡터 크기
        //float Mag = Vector3.Magnitude(direction);
        float Mag = Vector3.Distance(mousePosition, screenCenter) * screenCorrection;
        // 범위 제한식
        if (Mag <= maxMouseDistance)
        {
            transform.position = Vector3.Lerp(playerPos, playerPos + direction, Time.fixedDeltaTime * cameraMoveSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(playerPos, playerPos + direction.normalized * maxMouseDistance, Time.fixedDeltaTime * cameraMoveSpeed);
        }
    }
    // (미구현)플레이어 중심으로 카메라를 제어합니다.
    void TargetMode()
    {

    }
    // (미구현)완전히 자유로운 시점입니다.
    void FreeMode()
    {

    }
}
