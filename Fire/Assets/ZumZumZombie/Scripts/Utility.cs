using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

[DefaultExecutionOrder(-2000)]
public class Utility : Singleton<Utility>
{
    [Header("Player")]
    public Transform playerTr = null;

    public static int CameraViewMonster;

    public Slider PlayerHpBar = null;
    public Text PlayerHpText = null;

    private static int i = 0;

    public int CarKey = 1;

    public Color ColorChageForColorBlink(Color currentColor, Color changedColor)
    {
        Color returnColor = currentColor;
        var a = i % 2;

        if (a == 1)
        {
            returnColor = currentColor;
        }
        else
        {
            returnColor = changedColor;
        }
        i++;

        return returnColor;
    }

    public Color ChangeColor(Color color)
    {
        return color;
    }

    /// <summary>
    /// 백터를 각도로 변환
    /// </summary>
    /// <param name="dir">방향백터</param>
    /// <returns></returns>
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    //////////////////////////////////////////////////////////////////////////

    #region 포물선운동

    //public float flySpd;

    ////이동할 벡터의 xyz
    //private float tx;

    //private float ty;
    //private float tz;

    ////중력가속도.
    //public float g = 19.8f;

    //private float elapsed_time;
    //public float max_height;

    ////private float t;
    //private Vector3 start_pos;

    //private Vector3 end_pos;
    //private float dat;  //도착점 도달 시간
    //public Vector3 tpos;

    //public void FlyToTarget(Transform target, Vector3 targetstartPos, Vector3 targetendPos, float g = 19.8f, float max_height = 30f)
    //{
    //    start_pos = targetstartPos;

    //    end_pos = targetendPos;

    //    this.g = g;

    //    this.max_height = max_height;

    //    var dh = targetendPos.y - targetstartPos.y;

    //    var mh = max_height - targetstartPos.y;
    //    //에너지 보존법칙? 1/2mv^2=mgh. m을 양쪽에서 빼면 ty=2*g*h의 루트=v가 가능하다.
    //    ty = Mathf.Sqrt(2 * this.g * mh);

    //    //a==g
    //    float a = this.g;
    //    //b== -2 * ty(위에서 구한 속도?) 왜 -2를 곱하는지?
    //    float b = -2 * ty;
    //    //c== endpos가 startpos보다 얼마나 높은가 * 2
    //    float c = 2 * dh;

    //    //거=시*속, 시=거/속=?? 근의공식인가? 예상되는 체공시간?
    //    dat = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

    //    //최종적 x속도?
    //    tx = -(targetstartPos.x - targetendPos.x) / dat;
    //    //최종적 y속도?
    //    tz = -(targetstartPos.z - targetendPos.z) / dat;

    //    this.elapsed_time = 0;
    //    StartCoroutine(PositionChange(target));
    //}

    //private IEnumerator PositionChange(Transform target)
    //{
    //    while (true)
    //    {
    //        elapsed_time += Time.deltaTime * flySpd;

    //        //매프레임당 이동벡터의 x
    //        var tx = start_pos.x + this.tx * elapsed_time;
    //        //매프레임당 이동벡터의 y. x, z와 달리 중력에 따른 값을 빼준다. 가속도는 m/s^2이므로 위치는 가속도에 시간을 두번 곱해주면 거리를 구할 수 있음.
    //        var ty = start_pos.y + this.ty * elapsed_time - 0.5f * g * elapsed_time * elapsed_time;
    //        //매프레임당 이동벡터의 z
    //        var tz = start_pos.z + this.tz * elapsed_time;

    //        tpos = new Vector3(tx, ty, tz);

    //        //비행동안 날아가는 총알의 위치와 로테이션 변환.
    //        //transform.LookAt(tpos);
    //        target.position = tpos;

    //        //총 체공시간 계산치보다 비행시간이 길다면 탈출.
    //        if (elapsed_time >= dat)
    //        {
    //            target.GetComponent<MonsterState>().Shase_ChangeEvent?.Invoke();

    //            yield break;
    //        }
    //        yield return null;
    //    }
    //}

    #endregion 포물선운동

    private Vector3 FindEmptySpace(Vector3 pivot, float minDistance, float maxDistance)
    {
        Vector3 returnVector = new Vector3();
        while (true)
        {
            returnVector = FindFarPoint(pivot, minDistance, maxDistance);
            if (!SomethingOnPlace(returnVector))
            {
                break;
            }
        }
        return returnVector;
    }

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 90f, float maxDistance = 110f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0f, 360f);
        float radian = angle * Mathf.Deg2Rad;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }

    /// <summary>
    /// 피벗기준으로 rotation방향으로 distance만큼 떨어진 위치를 반환
    /// </summary>
    /// <param name="pivot"></param>
    /// <param name="rotation"></param>
    private Vector3 PivotPointSet(Vector3 pivot, Vector3 origin, Direction direction, float distance)
    {
        switch (direction)
        {
            case Direction.Left:
                pivot = origin;
                pivot.x -= distance;
                break;

            case Direction.Right:
                pivot = origin;
                pivot.x += distance;
                break;

            case Direction.Foward:
                pivot = origin;
                pivot.z += distance;

                break;

            case Direction.Back:
                pivot = origin;
                pivot.z -= distance;

                break;

            default:
                break;
        }
        return pivot;
    }

    /// <summary>
    /// 포인트 위치에 뭐가 있는지 확인하는 함수
    /// </summary>
    /// <param name="point">확인하고싶은 위치</param>
    /// <returns></returns>
    private bool SomethingOnPlace(Vector3 point)
    {
        Vector3 rayStartPoint = new Vector3(point.x, point.y + 80f, point.z);
        //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}

public class RandomAngle
{
    public float PatrolDistance = 10.0f;

    public float Value(float a, float b)
    {
        var value = Random.Range(a, b);
        return value;
    }

    public Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();

        var angle = Value(0.0f, 360.0f);

        pos.x = Mathf.Cos(angle) * PatrolDistance;
        pos.y = 0;
        pos.z = Mathf.Sin(angle) * PatrolDistance;

        return pos;
    }
}