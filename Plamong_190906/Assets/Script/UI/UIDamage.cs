using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// fixedUpdate 캐싱
internal static class YieldInstructionCache
{
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
}

public class UIDamage : MonoBehaviour
{
    // fixedUpdate 로 코루틴 작동
    WaitForSeconds waitTime;
    //데미지 표기 텍스트
    [SerializeField]
    private Text dmgText;
    // 현재 실행되고 있는 코루틴
    public Coroutine cortn;

    public float upVelocity;

    private void Start()
    {
        waitTime = new WaitForSeconds(0.02f);
        gameObject.SetActive(false);
    }

    public IEnumerator DisplayDamage(Transform targetTransform, int value)
    {
        dmgText.text = value.ToString();
        Vector3 pos = Camera.main.WorldToScreenPoint(targetTransform.position);
        transform.position = pos;

        float speed = 32f;
        float time = 0f;

        while(time <= 0.5f)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(pos.x, pos.y + speed * Time.deltaTime, pos.z);
            speed += upVelocity;

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        gameObject.SetActive(false);
    }
}
