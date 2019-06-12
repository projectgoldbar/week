using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieState;

public class SpitZombieMove : MonoBehaviour
{
    public Vector3 targetPosition;

    public enum SpitState {Patrol , Chase , Attack};
    public SpitState state = SpitState.Patrol;

    public ZombiesComponent component;

    public BoxCollider mapSizeCollider;

    private float distance = 0;

    public GameObject Spit;



    private void Awake()
    {
        targetPositionSetting(FindPoint());
    }

    private void Start()
    {
        component.agent.destination = targetPosition;
        component.agent.stoppingDistance = 15;
    }

    private void targetPositionSetting(Vector3 pos)
    {
        targetPosition = pos;
    }


    private float FindTimer = 0;
    private Collider[] obj;
    float AttackTimer = 0;

    private int n = 20;
    private void Update()
    {
        distance = Vector3.Distance(component.player.transform.position, transform.position);

        component.animator.SetFloat("Distance", distance);

        if (state == SpitState.Patrol)
        {
            FindTimer += Time.deltaTime;

            if (FindTimer >= 1.5)
            {
                obj = Physics.OverlapSphere(transform.position, n, LayerMask.GetMask("Player"));

                if (obj.Length > 0)
                {
                    Debug.Log("Player감지");
                    component.agent.ResetPath();
                    targetPosition = component.player.transform.position;

                    state = SpitState.Chase;
                }
                FindTimer = 0;
            }

            if (10.0f >= component.agent.remainingDistance)
            {
                targetPositionSetting(FindPoint());
                // component.agent.ResetPath();
                NavPath();
            }
        }
        else if (state == SpitState.Chase)
        {
            targetPosition = component.player.transform.position;

            NavPath();

            if (distance <= component.agent.stoppingDistance)
            {
                //component.agent.updateRotation = false;
                state = SpitState.Attack;
            }
        }
        else
        {
            NavPath();
            targetPosition = component.player.transform.position;
            transform.LookAt(targetPosition);

            AttackTimer += Time.deltaTime;
            if (AttackTimer >= 1)
            {
                Debug.Log("공격");
              
                var ob = GameObject.Instantiate(Spit);
                ob.transform.position = transform.position + Vector3.up * 2 + Vector3.forward * 0.5f;
                ob.transform.rotation = transform.rotation;


                AttackTimer = 0;
            }

            if (distance > component.agent.stoppingDistance)
            {
                //component.agent.updateRotation = true;
                state = SpitState.Chase;
            }
        }
    }



    public void NavPath()
    {
        component.agent.ResetPath();
        component.agent.CalculatePath(targetPosition, component.path);
        component.agent.SetPath(component.path);
    }


    private IEnumerator stateProcessPatrol()
    {
        while (state == SpitState.Patrol)
        {
            yield return null;
            //Debug.Log("patrol");
            if (Input.GetKeyDown(KeyCode.A))
            {
                state = SpitState.Chase;
                StartCoroutine(stateProcessChase());
                yield break;
            }
        }
    }

    private IEnumerator stateProcessChase()
    {
        while (state == SpitState.Chase)
        {
            yield return null;
            //  Debug.Log("chase");
            if (Input.GetKeyDown(KeyCode.A))
            {
                state = SpitState.Patrol;
                StartCoroutine(stateProcessPatrol());
                yield break;
            }
        }
    }


    public Vector3 FindPoint()
    {
        var bounds = mapSizeCollider.bounds;// GetComponent<BoxCollider>().bounds;
        var min = bounds.min;
        var max = bounds.max; 

        for (int i = 0; i < 50; i++)
        {
            var x = Random.Range(min.x, max.x);
            var z = Random.Range(min.z, max.z);
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
        Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            targetPosition = rayStartPoint;
            Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                targetPosition = rayStartPoint;
                Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    targetPosition = rayStartPoint;
                    Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        targetPosition = rayStartPoint;
                        Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
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
