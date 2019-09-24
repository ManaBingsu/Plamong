using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamage : MonoBehaviour
{
    //데미지 표기 텍스트
    [SerializeField]
    private Text dmgText;
    // 현재 실행되고 있는 코루틴
    public Coroutine cortn;

    public float upVelocity;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator DisplayDamage(Vector3 firstPos, int value)
    {
        dmgText.text = value.ToString();
        Vector3 pos = Camera.main.WorldToScreenPoint(firstPos);
        transform.position = pos;

        float speed = 32f;
        float time = 0f;

        while(time <= 0.5f)
        {
            time += Time.deltaTime;
            pos = Camera.main.WorldToScreenPoint(firstPos);
            transform.position = new Vector3(pos.x, pos.y + speed * Time.deltaTime, pos.z);

            speed += upVelocity;

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
