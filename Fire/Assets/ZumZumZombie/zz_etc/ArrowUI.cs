using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    public float arrowSpeed;
    public Transform currentLocation;
    public Transform target;

    private void Update()
    {
        ArrowHint();
    }

    private void ArrowHint()
    {
        //뷰포트 위치로 변경됨 화면 안에 들어왔으면 0~1
        //화면 밖이면 1보다 높다.

        Vector3 pos = Camera.main.WorldToViewportPoint(target.position);
        // Debug.Log("처음-Pos : " + pos);

        //도착위치가 카메라 안에 들어왔다면
        if ((pos.x < 1.0f && pos.x > 0.0f) && (pos.y < 1.0f && pos.y > 0.0f))
        {
            //도착위치로 위치 변경한다.
            transform.position = Camera.main.ViewportToWorldPoint(pos);
            return;
        }

        pos *= 2.0f;
        //Debug.Log("pos *= 2.0f : " + pos);
        pos = new Vector3(pos.x - 1, pos.y - 1, pos.z - 1);
        //Debug.Log("pos -1 : " + pos);
        if (Mathf.Abs(pos.x) > Mathf.Abs(pos.y))
        {
            pos.y = pos.y / Mathf.Abs(pos.x);
            //  Debug.Log("pos.y / Mathf.Abs(pos.x) : " + pos);
            if (pos.x > 0.9)
                pos.x = 0.9f;
            else if (pos.x < -0.9)
                pos.x = -0.9f;
        }
        else
        {
            pos.x = pos.x / Mathf.Abs(pos.y);
            //Debug.Log("pos.x / Mathf.Abs(pos.y) : " + pos);
            if (pos.y > 0.9)
                pos.y = 0.9f;
            else if (pos.y < -0.9)
                pos.y = -0.9f;
        }
        pos = new Vector3(pos.x + 1, pos.y + 1, pos.z + 1);
        //Debug.Log("pos+1 : " + pos);
        pos /= 2.0f;
        //Debug.Log("pos/=2 : " + pos);
        transform.position =
                        Vector3.Lerp(transform.position,
                        Camera.main.ViewportToWorldPoint(pos),
                        Time.deltaTime * arrowSpeed);
        var direction = (target.position - transform.position).normalized;
        direction.y = 0;
        var a = Quaternion.LookRotation(direction);
        transform.rotation = a;
    }
}