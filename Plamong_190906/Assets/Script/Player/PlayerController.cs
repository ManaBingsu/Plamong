using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이벤트 처리자
    public delegate void EventHandler();

    public enum WeaponMode { Sword , Gun }

    public WeaponMode weaponMode;
    
    public static PlayerController player;
    // 현재 착용하고 있는 무기와 이벤트
    public event EventHandler EvWeapon;
    [SerializeField]
    private AbsWeapon currentWeapon;
    public AbsWeapon CurrentWeapon
    {
        get { return CurrentWeapon; }
        set
        {
            currentWeapon = value;
            EvWeapon();
        }
    }

    [SerializeField]
    private List<AbsWeapon> weaponList;

    // 플레이어의 데이터
    public PlayerData playerData;
    // (임시) 플레이어 행동 제어
    public bool IsActing;

    private void Awake()
    {
        if (player == null)
            player = this;
    }

    private void Start()
    {
        LoadPlayerData();
        //transform.position = new Vector3(0, 0, 4);
    }

    private void Update()
    {
        Move();

        // 임시 무기변경
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if(weaponMode == WeaponMode.Sword)
            {
                weaponMode = WeaponMode.Gun;
            }
            else
            {
                weaponMode = WeaponMode.Sword;
            }

            CurrentWeapon = weaponList[(int)weaponMode];
        }

        if(Input.GetMouseButtonDown(0))
        {
            MouseAttack();
        }
    }

    // 플레이어의 데이터를 게임매니저로부터 불러와 clone화 시킴
    void LoadPlayerData()
    {
        playerData = Instantiate(GameManager.gameManager.playerInfoData) as PlayerData;
    }
    // 플레이어 움직임 제어
    void Move()
    {
        // 임시 행동 제한
        if (IsActing)
            return;

        if(Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0f, 0f, playerData.MoveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-playerData.MoveSpeed * Time.deltaTime, 0f, 0f);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0f, 0f, -playerData.MoveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(playerData.MoveSpeed * Time.deltaTime, 0f, 0f);
    }
    // 플레이어 공격 제어
    void MouseAttack()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << LayerMask.NameToLayer("Plane");  // Player 레이어만 충돌 체크함
        Vector3 targetPos;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.tag == "Ground")
        {
            targetPos = hit.point;

            // 원거리 공격
            StartCoroutine(currentWeapon.MouseAttack1(targetPos));
            // 근거리 공격
        }
    }


}
