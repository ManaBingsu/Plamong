using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    public BulletPulling bulletPulling;
    
    // 플레이어의 데이터
    public PlayerData playerData;

    private void Awake()
    {
        if (player == null)
            player = this;
    }

    private void Start()
    {
        LoadPlayerData();
        transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        Move();

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
        Debug.Log(layerMask);
        Vector3 targetPos;
        if (Physics.Raycast(ray, out hit, 100f, layerMask) && hit.collider.tag == "Ground")
        {
            targetPos = hit.point;

            bulletPulling.ShotBullet(targetPos, 30f, BulletInfo.ShotType.Straight, BulletInfo.SpriteType.Straight);
        }

    }
}
