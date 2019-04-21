using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pomulseon : MonoBehaviour
{
    // Start is called before the first frame update

    #region 포물선운동

    public float flySpd;

    //이동할 벡터의 xyz
    private float tx;

    private float ty;
    private float tz;

    //중력가속도.
    private float g = 19.8f;

    private float elapsed_time;
    private float Max_height;

    //private float t;
    private Vector3 start_pos;

    private Vector3 end_pos;
    private float dat;  //도착점 도달 시간
    private Vector3 tpos;

    #endregion 포물선운동

    public void FlyToTarget(Transform target, Vector3 targetstartPos, Vector3 targetendPos, Action<Transform> action = null, float g = 19.8f, float max_height = 50.0f)
    {
        start_pos = targetstartPos;

        end_pos = targetendPos;

        this.g = g;

        // this.Max_height = max_height;

        var dh = targetendPos.y - targetstartPos.y;

        var mh = max_height - targetstartPos.y;
        //에너지 보존법칙? 1/2mv^2=mgh. m을 양쪽에서 빼면 ty=2*g*h의 루트=v가 가능하다.
        ty = Mathf.Sqrt(2 * this.g * mh);

        //a==g
        float a = this.g;
        //b== -2 * ty(위에서 구한 속도?) 왜 -2를 곱하는지?
        float b = -2 * ty;
        //c== endpos가 startpos보다 얼마나 높은가 * 2
        float c = 2 * dh;

        //거=시*속, 시=거/속=?? 근의공식인가? 예상되는 체공시간?
        dat = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        //최종적 x속도?
        tx = -(targetstartPos.x - targetendPos.x) / dat;
        //최종적 y속도?
        tz = -(targetstartPos.z - targetendPos.z) / dat;

        this.elapsed_time = 0;
        StartCoroutine(PositionChange(target, action));
    }

    private IEnumerator PositionChange(Transform target, Action<Transform> action = null)
    {
        while (true)
        {
            elapsed_time += Time.deltaTime * flySpd;

            //매프레임당 이동벡터의 x
            var tx = start_pos.x + this.tx * elapsed_time;
            //매프레임당 이동벡터의 y. x, z와 달리 중력에 따른 값을 빼준다. 가속도는 m/s^2이므로 위치는 가속도에 시간을 두번 곱해주면 거리를 구할 수 있음.
            var ty = start_pos.y + this.ty * elapsed_time - 0.5f * g * elapsed_time * elapsed_time;
            //매프레임당 이동벡터의 z
            var tz = start_pos.z + this.tz * elapsed_time;

            tpos = new Vector3(tx, ty, tz);

            //비행동안 날아가는 총알의 위치와 로테이션 변환.
            //transform.LookAt(tpos);
            target.transform.position = tpos;

            //총 체공시간 계산치보다 비행시간이 길다면 탈출.
            if (elapsed_time + 0.03f >= dat)
            {
                action?.Invoke(target);
                yield break;
            }
            yield return null;
        }
    }

    //public IEnumerator PlayerStandup(Player player, float time = 1.1f)
    //{
    //    player.Anim.Play("StanUp");

    //    while (true)
    //    {
    //        if (player.Anim.GetCurrentAnimatorStateInfo(0).IsName("StanUp") &&
    //            player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
    //        {
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}

    public IEnumerator MonsterStanUpStateChange(Transform monster, StateIndex state, float time = 1.1f)
    {
        // yield return new WaitForSeconds(0.4f);

        var anim = monster.GetComponent<MonsterUnit>().Anim;
        var MonsterState = monster.GetComponent<MonsterState>();

        anim.Play("StanUp");

        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("StanUp") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
            {
                MonsterState.ChangeState(state);
                yield break;
            }

            yield return null;
        }
    }
}