using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 변수 //
    // 마우스 움직임 함수 대리자 선언
    private delegate void MoveCamera();
    // 마우스 움직임 함수 대리자
    private MoveCamera moveCamera;
    // 카메라 모드 열거형
    private enum CameraMode { Mouse, Player, Free };
    [SerializeField]
    private CameraMode cameraMode;
    // 카메라가 추적하는 대상
    [SerializeField]
    private Transform target;

    // 함수 //
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
    // (미구현)마우스 중심으로 카메라를 제어합니다. 단, 플레이어로부터 일정 범위를 벗어나지 않습니다.
    void MouseMode()
    {

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
