using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController mainCam;
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
    //게임 화면 상의 카메라 크기
    //height = 2f * cam.orthographicSize;
    //width = height * cam.aspect;
    private float camHeight;
    private float camWidth;
    // 미니맵 사이즈
    [SerializeField]
    private float minimapSize;

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
    public float firstY;
    // 카메라가 z로 이동할때 제한 거리
    [SerializeField]
    private float zLimit;
    // Freemode 시야 옮김 예민도
    [SerializeField]
    private float freeModeSensivity;
    // Freemode 시야 옮김 범위
    [SerializeField]
    private float freeModeRange;
    // 화면 중심 좌표
    private Vector3 screenCenter;   

    [Header("Reference")]
    // 플레이어 위치 벡터
    [SerializeField]
    private Vector3 playerPos;
    // 평지 Render
    [SerializeField]
    private Renderer plane;
    // 평지 Transform
    [SerializeField]
    private Transform planeTransform;
    [SerializeField]
    private Camera minimapCam;
    [SerializeField]
    private Transform minimapMarker;


    // 카메라가 추적하는 대상
    [Header("Target mode setting")]
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        cam = transform.GetComponentInChildren<Camera>();
        if (mainCam == null)
            mainCam = GetComponent<CameraController>();
    }

    private void Start()
    {
        firstY = 10f;
        SetOrthographicSize();
        SetResoultion(screenWidth, screenHeight, IsFullScreen);
        moveCamera = new MoveCamera(MouseMode);
    }

    private void Update()
    {
        // 카메라 변경, 임시
        if (Input.GetKeyUp(KeyCode.F1))
            SetCameraMode(CameraMode.Mouse);
        if (Input.GetKeyUp(KeyCode.F2))
            SetCameraMode(CameraMode.Player);
        if (Input.GetKeyUp(KeyCode.F3))
            SetCameraMode(CameraMode.Free);
    }

    private void LateUpdate()
    {
        // 반드시 카메라 움직인 후 보더아웃 확인할것 서순 중요
        moveCamera();
        MoveToMinimapPos();
        LimitBorderOut();
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
        // 게임 해상도
        screenWidth = width;
        screenHeight = height;
        IsFullScreen = IsFull;
        // 게임 내 카메라와 관련된 값들
        screenCenter = new Vector3(Screen.width, 0f, Screen.height) * 0.5f;
        // 미니맵 크기 재조정
        SetMinimapSize();
        // 미니맵 마커 크기 재조정
        SetMarkerSize();

        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

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
                transform.position = PlayerController.player.transform.position;
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
    // 플레이어 중심으로 카메라를 제어합니다.
    void TargetMode()
    {
        // 플레이어의 위치 갱신
        playerPos = PlayerController.player.transform.position;
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.fixedDeltaTime * cameraMoveSpeed * 3f);
    }
    // 완전히 자유로운 시점입니다.
    void FreeMode()
    {
        // 만약 카메라 끝 반경에 닿을시 카메라 이동
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mousePos.x > 1 - freeModeRange)
        {
            transform.Translate(Vector3.right * freeModeSensivity * Time.deltaTime);
        }
        else if(mousePos.x < freeModeRange)
        {
            transform.Translate(Vector3.left * freeModeSensivity * Time.deltaTime);
        }

        if(mousePos.y > 1 - freeModeRange)
        {
            transform.Translate(Vector3.forward * freeModeSensivity * Time.deltaTime);
        }
        else if (mousePos.y < freeModeRange)
        {
            transform.Translate(Vector3.back * freeModeSensivity * Time.deltaTime);
        }
    }
    // 카메라 반경을 제한합니다
    void LimitBorderOut()
    {
        // 만약 제한이 안될 경우 플레이어를 따라다닌다.
        float camXPos = transform.position.x;
        float camZPos = transform.position.z;
        // 오른쪽
        if (transform.position.x + camWidth > plane.bounds.size.x / 2f)
        {
            camXPos = plane.bounds.size.x / 2f - cam.orthographicSize * cam.aspect;
        }
        // 왼쪽
        else if (transform.position.x - camWidth < -plane.bounds.size.x / 2f)
        {
            camXPos = -plane.bounds.size.x / 2f + cam.orthographicSize * cam.aspect;
        }
        // 위쪽, 카메라가 45도 꺾여있어서 일일이 수정할수바께 없다..
        if (transform.position.z + camHeight > zLimit)
        {
            camZPos = zLimit - cam.orthographicSize;
        }
        // 아래쪽, 카메라가 45도 꺾여있어서 일일이 수정할수바께 없다..
        else if (transform.position.z - camHeight < -zLimit)
        {
            camZPos = -zLimit + cam.orthographicSize;
        }

        Vector3 limitPos = new Vector3(camXPos, transform.position.y, camZPos);
        transform.position = limitPos;
    }
    // 미니맵 클릭 시 그쪽으로 시야전환
    void MoveToMinimapPos()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = minimapCam.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Mark");  // Mark 레이어만 충돌 체크함
            Vector3 targetPos;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.tag == "Ground")
            {
                targetPos = hit.point;
                targetPos.y = 0f;
                transform.position = Vector3.Lerp(transform.position, targetPos, 1f);
            }
        }
    }
    // 미니맵의 크기를 지원하는 최대 해상도에 따라 보정합니다. (현재 1920)
    void SetMinimapSize()
    {
        float size = minimapSize * ((float)screenWidth / 1920f);
        minimapCam.pixelRect = new Rect(screenWidth - (32f * ((float)screenWidth / 1920f)) - size, 
                                        screenHeight - (32f * ((float)screenWidth / 1920f)) - size, 
                                        size, size);
    }
    // 미니맵에 표시되는 카메라 마커 크기 비율 조정, 기준은 16:9  3:1
    void SetMarkerSize()
    {
        Vector3 markScale = minimapMarker.localScale;
        Vector3 scale = new Vector3(((float)screenWidth / screenHeight) * (9f / 16f), 1f, ((float)screenHeight / screenWidth) * (16f / 9f));
        minimapMarker.localScale = new Vector3(markScale.x * scale.x, 1f, markScale.z * scale.z);

    }
}
