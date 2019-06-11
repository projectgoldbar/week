using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieState;

public class WayZombiesChaseNReturn : MonoBehaviour
{
    public Transform Player;

    [SerializeField]
    private float Distance = 0;

    public float MinDistance = 0;
    public float MiddleDistance = 0;
    public float MaxDistance = 0;

    public Zombies_Position zombies_position;
    public CreateWayZombie createWayZombie;
    public Z_WayPointControll controll;

    public bool b_return;

    public enum WayZombieDistance {MinDistance , MiddleDistanceDown , MiddleDistanceUp, MaxDistance }
    
    private WayZombieDistance zombiedistance;

    public WayZombieDistance ZombieDistance
    {
        get { return zombiedistance; }
        set
        {
            int max = createWayZombie.WayZombieList.Count;
            zombiedistance = value;
            if (zombiedistance == WayZombieDistance.MinDistance)
            {
                for (int i = 0; i < max; i++)
                {
                    controll.b_Moving = false;
                    createWayZombie.WayZombieList[i].transform.parent = null;
                    createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().target = Player.transform;
                    createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStart();
                }
                b_return = false;
            }
            else if (zombiedistance == WayZombieDistance.MiddleDistanceDown)
            {
                for (int i = 0; i < max; i++)
                {
                    if (!createWayZombie.WayZombieList[i].activeSelf)
                        createWayZombie.WayZombieList[i].gameObject.SetActive(true);
                }
            }
           
            else if (zombiedistance == WayZombieDistance.MaxDistance)
            {
                for (int i = 0; i < max; i++)
                {
                    createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().target = zombies_position.ZombiesWayPosition[i];
                   // createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStop();
                }
                b_return = true;
            }
        }
    }


    private bool b_Chase;
    public bool MyChase
    {
        get { return b_Chase; }
        set
        {
            b_Chase = value;
            int max = createWayZombie.WayZombieList.Count;

            if (b_Chase)
            {
                for (int i = 0; i < max; i++)
                {
                    createWayZombie.WayZombieList[i].GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                    createWayZombie.WayZombieList[i].transform.parent = null;
                    createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStart();
                    controll.b_Moving = false;
                    b_return = false;
                }
            }
            else
            {
                for (int i = 0; i < max; i++)
                {
                    createWayZombie.WayZombieList[i].GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

                    //createWayZombie.WayZombieList[i].transform.position = zombies_position.DefaultDistance[i];
                    //createWayZombie.WayZombieList[i].transform.rotation = createWayZombie.transform.rotation;
                    //createWayZombie.WayZombieList[i].transform.parent = createWayZombie.transform;

                    createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStop();
                    
                    b_return = true;
                }
            }
        }
    }

    private void Start()
    {
        controll.b_Moving = true;
    }


    public void CreateZombieSetting(int i, Transform tr = null, bool flag = true)
    {
        createWayZombie.WayZombieList[i].transform.SetParent(tr);
        createWayZombie.WayZombieList[i].GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = flag;
    }

    int count = 0;
    private void Update()
    {
        Distance = Vector3.Distance(transform.position, Player.position);

        if      (MinDistance > Distance)                                        ZombieDistance = WayZombieDistance.MinDistance;
        else if (MiddleDistance > Distance && MinDistance < Distance)           ZombieDistance = WayZombieDistance.MiddleDistanceDown;
        else if (MaxDistance < Distance)                                        ZombieDistance = WayZombieDistance.MaxDistance;

        if (b_return)
        {
            if (count >= 10)
            {
                controll.b_Moving = true;
                b_return = false;
                count = 0;
            }

            for (int i = 0; i < 10; i++)
            {
                //createWayZombie.WayZombieList[i].transform.position = Vector3.Lerp(createWayZombie.WayZombieList[i].transform.position,
                //                                                                   zombies_position.DefaultDistance[i],
                //                                                                   Time.deltaTime * 0.2f);

                //createWayZombie.WayZombieList[i].transform.LookAt(createWayZombie.transform);

                if (Vector3.Distance(createWayZombie.WayZombieList[i].transform.position, zombies_position.DefaultDistance[i]) <= MiddleDistance)
                {
                    createWayZombie.WayZombieList[i].transform.position = zombies_position.DefaultDistance[i];
                    createWayZombie.WayZombieList[i].transform.rotation = createWayZombie.transform.rotation;
                    createWayZombie.WayZombieList[i].transform.parent = createWayZombie.transform;
                    ++count;

                    if (createWayZombie.WayZombieList[i].activeSelf)
                    {
                        createWayZombie.WayZombieList[i].SetActive(false);
                    }
                }
            }

            
        }

    }


    #region 기즈모
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MinDistance);
        //////////////////////////////////////////////////////////
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, MiddleDistance);
        //////////////////////////////////////////////////////////
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
    #endregion
}