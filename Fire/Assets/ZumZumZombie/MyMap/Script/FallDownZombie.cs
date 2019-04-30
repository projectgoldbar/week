﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FallDownZombie : MonoBehaviour
{
    private ZombieType type = ZombieType.Falldown;
    private Animator anim;
    public float flySpd;
    public Transform target;
    public string jump = "Jumping";
    public float range = 5f;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private WaitForSeconds seconds = new WaitForSeconds(3);

    #region 포물선운동

    //이동할 벡터의 xyz
    private float tx;

    private float ty;
    private float tz;

    //속도. 시작인가?
    //private float v;

    //중력가속도.
    public float g = 19.8f;

    private float elapsed_time;
    public float max_height;

    //private float t;
    private Vector3 start_pos;

    private Vector3 end_pos;
    private float dat;  //도착점 도달 시간
    public Vector3 tpos;

    #endregion 포물선운동

    public Vector3 arrivepos = new Vector3(0, 0, 0);

    private void Start()
    {
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindObjectOfType<Player>().transform;
        StartCoroutine(CalculatePath());
        StartCoroutine(CheckDistanceAndLunch());
        // FlyToTarget(transform.position, FallDownPosition(), 9.8f, max_height);
    }

    private IEnumerator CalculatePath()
    {
        var a = Utility.Instance.playerTr.GetComponent<TestTarget>();
        while (true)
        {
            agent.ResetPath();
            agent.CalculatePath(Utility.Instance.playerTr.position, path);
            agent.SetPath(path);
            yield return seconds;
        }
    }

    private IEnumerator CheckDistanceAndLunch()
    {
        for (; ; )
        {
            //Debug.Log(Vector3.Distance(new Vector3(transform.position.x, 1.5f, transform.position.y), target.position));
            if (Vector3.Distance(new Vector3(transform.position.x, 1.5f, transform.position.z), target.position) < range)
            {
                Launch();
                yield break;
            }
            yield return null;
        }
    }

    private Vector3 setAmingPoint;

    private void Launch()
    {
        setAmingPoint = FallDownPosition();

        anim.speed = 0.6f;
        anim.SetBool(jump, true);
        //OnDrawRadius(setamingPoint);
        EnemyAttackUIManager.instance.Draw(type, 2f, setAmingPoint);

        FlyToTarget(transform.position, setAmingPoint, g, max_height);
    }

    private Vector3 FallDownPosition()
    {
        var positio = FindFarPoint(target.position, 10f, 20f);
        return positio;
    }

    private void FlyToTarget(Vector3 startPos, Vector3 endPos, float g, float max_height)
    //포물선 비행. 날아가며 타겟마크 알파 높이기. 적중 시 ReachedTarget실행.
    {
        start_pos = startPos;

        end_pos = endPos;

        this.g = g;

        this.max_height = max_height;

        var dh = endPos.y - startPos.y;

        var mh = max_height - startPos.y;
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
        tx = -(startPos.x - endPos.x) / dat;
        //최종적 y속도?
        tz = -(startPos.z - endPos.z) / dat;

        this.elapsed_time = 0;
        StartCoroutine(PositionChange());
    }

    private IEnumerator PositionChange()
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
            transform.position = tpos;

            //_skills.targetMarkColor = new Color(0f, elapsed_time / dat, 0f, 1f);

            //총 체공시간 계산치보다 비행시간이 길다면 탈출.
            if (elapsed_time >= dat)
            {
                //anim.SetBool(jump, false);
                ParticleManager.Instance.OutputEffect(type, new Vector3(setAmingPoint.x, setAmingPoint.y + 1f, setAmingPoint.z));
                gameObject.SetActive(false);

                yield break;
            }
            yield return null;
        }
    }

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 6f, float maxDistance = 10f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(target.rotation.eulerAngles.y * -1f, -target.rotation.eulerAngles.y + 90);
        float radian = (angle * Mathf.Deg2Rad) + 45f;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }
}