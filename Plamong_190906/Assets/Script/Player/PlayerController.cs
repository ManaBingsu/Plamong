using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이벤트 처리자
    public delegate void EventHandler();
    public enum WeaponMode { Sword , Gun }
    public WeaponMode weaponMode;
    // 플레이어 싱글톤  
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
            weaponSpr = currentWeapon.weaponSpr.GetComponent<SpriteRenderer>();
            EvWeapon();
        }
    }
    // 무기
    [SerializeField]
    public SpriteRenderer weaponSpr;

    // 무기Info 참조
    [SerializeField]
    private WeaponInfo weaponInfo;
    // 플레이어의 데이터
    public PlayerData playerData;
    // (임시) 플레이어 행동 제어
    public bool IsActing;
    [Header("Sprite")]
    // 회전해야 할 총
    [SerializeField]
    protected List<Transform> weaponTransform;
    // 무기스프라이트 위치 보정
    [SerializeField]
    protected List<Transform> weaponPosTransform;
    [SerializeField]
    protected SpriteRenderer bodySprRend;
    [SerializeField]
    protected Sprite frontBodySpr;
    [SerializeField]
    protected Sprite backBodySpr;
    // 회전해야 할 총
    [SerializeField]
    protected SortingLayer sort;
    [Header("UI Value")]
    [SerializeField]
    protected Color dmgColor;


    private void Awake()
    {
        if (player == null)
            player = this;
    }

    private void Start()
    {
        LoadPlayerData();
        //데미지 이벤트 추가
        playerData.EvValueDurability += new PlayerData.EventValueHandler(DisplayDamage);
        //무기 스프라이트 렌더러 추가
        weaponSpr = currentWeapon.weaponSpr.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        // 임시 무기변경
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (IsActing)
                return;

            if(weaponMode == WeaponMode.Sword)
            {
                weaponMode = WeaponMode.Gun;
            }
            else
            {
                weaponMode = WeaponMode.Sword;
            }
            // 이전 무기값 초기화
            currentWeapon.isDelay = false;
            currentWeapon.gameObject.SetActive(false);
            // 무기 변경됨
            CurrentWeapon = weaponInfo.weaponList[(int)weaponMode];
            currentWeapon.gameObject.SetActive(true);
        }

        RotateWeapon((int)weaponMode);

        if (Input.GetMouseButton(0))
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
        // 임시
        if(Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0f, 0f, playerData.MoveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-playerData.MoveSpeed * Time.deltaTime, 0f, 0f);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0f, 0f, -playerData.MoveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(playerData.MoveSpeed * Time.deltaTime, 0f, 0f);
    }

    void GetDamage(int value)
    {
        playerData.Durability = playerData.Durability - value;
    }

    void DisplayDamage(int dmg)
    {
        Vector3 dmgPos = transform.position;
        dmgPos.y += 1f;
        UIDamagePooling.damagePulling.DisplayDamage(dmgPos, dmg, dmgColor, 16);
    }

    // 플레이어 공격 제어
    void MouseAttack()
    {
        if (IsActing)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << LayerMask.NameToLayer("Plane");  // Player 레이어만 충돌 체크함
        Vector3 targetPos;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.tag == "Ground")
        {
            // 오토마우스 방지
            //StartCoroutine(ActingDelay(currentWeapon.delay));
            // 타격점 지정
            targetPos = hit.point;
            // 노말 공격
            StartCoroutine(currentWeapon.MouseAttack1(playerData.Damage, transform, targetPos));
        }
    }

    IEnumerator ActingDelay(float time)
    {
        IsActing = true;
        yield return new WaitForSeconds(time);
        IsActing = false;
    }

    void RotateWeapon(int n)
    {
        if (IsActing)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Plane");  // Player 레이어만 충돌 체크함
        Vector3 targetPos;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.tag == "Ground")
        {
            targetPos = hit.point;
            Vector3 hitPos = new Vector3(targetPos.x, 0f, targetPos.y);
            Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);

            Vector3 v = (targetPos - myPos).normalized;

            float direction = Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;

            if(direction > 0 && direction < 180)
            {
                //weaponSpr.sortingOrder = -1;
                Vector3 pos = new Vector3(weaponPosTransform[(int)weaponMode].localPosition.x, 0.14f, 0.4f);
                weaponPosTransform[(int)weaponMode].transform.localPosition = pos;
                bodySprRend.sprite = backBodySpr;
            }
            else
            {
                //weaponSpr.sortingOrder = 1;
                Vector3 pos = new Vector3(weaponPosTransform[(int)weaponMode].localPosition.x, 0.37f, 0.17f);
                weaponPosTransform[(int)weaponMode].localPosition = pos;
                bodySprRend.sprite = frontBodySpr;
            }
            if (direction > 90 && direction <= 180 || direction > -180 && direction < -90)
            {
                Vector3 xFlip = new Vector3(-1f, 1f, 1f);
                transform.localScale = xFlip;
                weaponSpr.flipX = true;
                direction *= -1;
            }
            else
            {
                Vector3 xFlip = new Vector3(1f, 1f, 1f);
                transform.localScale = xFlip;
                weaponSpr.flipX = false;
            }

            weaponTransform[n].localRotation = Quaternion.Euler(new Vector3(90f, 0f, direction));
        }     
    }
}
