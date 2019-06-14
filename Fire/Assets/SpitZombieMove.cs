using System;
using System.Collections;
using UnityEngine;
using ZombieState;

public class SpitZombieMove : MonoBehaviour
{
    
    public ZombiesComponent component;
    public BoxCollider mapSizeCollider;
    public GameObject Spit;
    public Vector3 targetPosition;

    public enum state {Patrol , Chase }
    public state s = state.Patrol;


    
    private float distance = 0;
    private float FindTimer = 0;
    private float AttackTimer = 0;

    private int n = 10;


    #region 이벤트 (path계속찍음)
    public Action Path;
    #endregion

    #region 이벤트 플레이어 감지
    public Action Sence;
    #endregion

    #region 이벤트 플레이어 공격
    public Action Attack;
    #endregion


    //public Action AttackMotionEvent;


    private void OnEnable()
    {
        Path += NavPath;
        Sence += playerSence;
        targetPositionSetting(FindPoint());
    }

    private void Update()
    {

        distance = Vector3.Distance(component.player.transform.position, transform.position);

        Sence?.Invoke();
        Attack?.Invoke();
    }

    public void NavPath()
    {
        component.agent.ResetPath();
        component.agent.CalculatePath(targetPosition, component.path);
        component.agent.SetPath(component.path);
    }

    public void playerSence()
    {
        s = state.Patrol;
        FindTimer += Time.deltaTime;

        //value가 5보다 크면 달리는모션
        //value가 5보다 작으면 공격모션
        component.animator.SetFloat("Distance", 6);

        Path?.Invoke();
        if (FindTimer >= 0.5f)
        {
            var obj = Physics.OverlapSphere(transform.position, 10.0f, LayerMask.GetMask("Player"));
            if (obj.Length > 0)
            {
                Debug.Log("Player감지");
                component.agent.ResetPath();
                SpitPatrolNChase(false, transform.position);
            }
        }
        if (10.0f >= component.agent.remainingDistance)
        {
            targetPositionSetting(FindPoint());
        }
    }

    public Vector3 Dir(Vector3 a, Vector3 b)
    {
        return (a - b).normalized;
    }


    public void playerAttack()
    {
        s = state.Chase;

        var dis = Dir(component.player.position , transform.position);
        Quaternion rot = Quaternion.LookRotation(dis);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime );

        if (5.0f <= distance)
        {
            SpitPatrolNChase(true, component.player.transform.position);
        }
    }

    


    public void SpitPatrolNChase(bool flag , Vector3 Targetposition)
    {
        ResetTimer();
        AgentUpdate(flag);
        targetPositionSetting(component.player.transform.position);
        if (flag)
        {
            Attack -= playerAttack;
            Sence += playerSence;
        }
        else
        {
            Attack += playerAttack;
            Sence -= playerSence;
        }
    }



    public void ResetTimer()
    {
        FindTimer = 0;
        AttackTimer = 0;
    }

    public void CreateSpit()
    {
        var ob = spitPoolManager.Instance.GetSpitObj();
        ob.SetActive(true);
        ob.transform.position = transform.position + Vector3.up * 2 + Vector3.forward * 0.5f;
        ob.transform.rotation = transform.rotation;
    }

    public void AgentUpdate(bool flag)
    {
        //component.agent.updatePosition = flag;
        component.agent.updateRotation = flag;
    }

    public void targetPositionSetting(Vector3 pos)
    {
        targetPosition = pos;
    }

    public Vector3 FindPoint()
    {
        var bounds = mapSizeCollider.bounds;// GetComponent<BoxCollider>().bounds;
        var min = bounds.min;
        var max = bounds.max; 

        for (int i = 0; i < 50; i++)
        {
            var x = UnityEngine.Random.Range(min.x, max.x);
            var z = UnityEngine.Random.Range(min.z, max.z);
            Vector3 targetVector = new Vector3(x, 1.7f, z);
            if (!SomethingOnPlace(targetVector))
            {
                return targetVector;
            }
        }
        return new Vector3(1000.0f, 1.7f, 1000.0f);
    }

    private bool SomethingOnPlace(Vector3 point)
    {
        Vector3 rayStartPoint = new Vector3(point.x, point.y + 80f, point.z);
        //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            //targetPosition = rayStartPoint;
            //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                //targetPosition = rayStartPoint;
                //Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    //targetPosition = rayStartPoint;
                   // Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        //targetPosition = rayStartPoint;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, n);


        Gizmos.color = Color.black;
        Gizmos.DrawSphere(targetPosition, 5);

    }
}