using System.Collections;
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

    private System.Action minEvent;
    private System.Action middleEvent;
    private System.Action maxEvent;
    private System.Action processEvent;





    public enum WayZombieDistance {MinDistance , MiddleDistanceDown , MiddleDistanceUp, MaxDistance }
    
    private WayZombieDistance zombiedistance;

    public WayZombieDistance ZombieDistance
    {
        get { return zombiedistance; }
        set
        {
            zombiedistance = value;
            if (zombiedistance == WayZombieDistance.MinDistance)
            {
                minEvent?.Invoke();
            }
            else if (zombiedistance == WayZombieDistance.MiddleDistanceDown)
            {
                middleEvent?.Invoke();
            }
            else if (zombiedistance == WayZombieDistance.MaxDistance)
            {
                maxEvent?.Invoke();
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


    private void OnEnable()
    {
        minEvent += Min;
        middleEvent += Middle;
        maxEvent += Max;
        processEvent += Process;
    }

    private void Start()
    {
        controll.b_Moving = true;

       // StartCoroutine(enumerator());
    }

    public void CreateZombieSetting(int i, Transform tr = null, bool flag = true)
    {
        createWayZombie.WayZombieList[i].transform.SetParent(tr);
        createWayZombie.WayZombieList[i].GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = flag;
    }

    int count = 0;


    private void FixedUpdate()
    {
        Distance = Vector3.Distance(transform.position, Player.position);

        if (MinDistance > Distance)                                     ZombieDistance = WayZombieDistance.MinDistance;
        else if (MiddleDistance > Distance && MinDistance < Distance)   ZombieDistance = WayZombieDistance.MiddleDistanceDown;
        else if (MaxDistance < Distance)                                ZombieDistance = WayZombieDistance.MaxDistance;

        if (b_return)
        {
            processEvent?.Invoke();
        }
       
    }

    public void Process()
    {
        for (int i = 0; i < 10; i++)
        {

            //var view = Camera.main.WorldToViewportPoint(createWayZombie.WayZombieList[i].transform.position);

            if (Vector3.Distance(createWayZombie.WayZombieList[i].transform.position, zombies_position.DefaultDistance[i]) <= MiddleDistance )
                
                // || ((view.x < 0 && view.y < 0) && (view.x < 0 && view.y > 1)) ||
                //((view.x > 1 && view.y > 1) && (view.x > 1 && view.y < 0)))
            {
                //createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStop();
                createWayZombie.WayZombieList[i].transform.position = zombies_position.DefaultDistance[i];
                createWayZombie.WayZombieList[i].transform.rotation = createWayZombie.transform.rotation;
                createWayZombie.WayZombieList[i].transform.SetParent(createWayZombie.transform);
                createWayZombie.WayZombieList[i].gameObject.SetActive(false);

                ++count;
            }
            else
            {
                createWayZombie.WayZombieList[i].transform.position =
                    Vector3.Lerp(createWayZombie.WayZombieList[i].transform.position,
                                 zombies_position.DefaultDistance[i],
                                 Time.fixedDeltaTime * 0.5f);

                createWayZombie.WayZombieList[i].transform.LookAt(createWayZombie.transform.position);
            }
        }

        if (count >= 10)
        {
            count = 0;
            minEvent += Min;
            middleEvent += Middle;
            maxEvent += Max;
            //processEvent += Process;

            controll.timer = 0;
            controll.b_Moving = true;
            b_return = false;

        }

    }


    public void Min()
    {
        int max = createWayZombie.WayZombieList.Count;
        for (int i = 0; i < max; i++)
        {
            controll.b_Moving = false;
            createWayZombie.WayZombieList[i].transform.parent = null;
            createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStart();
            createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().target = Player.transform;
        }
        b_return = false;
        minEvent -= Min;
    }

    public void Middle()
    {
        int max = createWayZombie.WayZombieList.Count;
        for (int i = 0; i < max; i++)
        {
            if (!createWayZombie.WayZombieList[i].activeSelf)
                createWayZombie.WayZombieList[i].gameObject.SetActive(true);
        }
        middleEvent -= Middle;
    }

    public void Max()
    {
        int max = createWayZombie.WayZombieList.Count;
        for (int i = 0; i < max; i++)
        {
            createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().target = zombies_position.ZombiesWayPosition[i];
            //createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStop();
        }
        b_return = true;
        maxEvent -= Max;
    }


    private IEnumerator enumerator()
    {
        while (true)
        {
            yield return null;
            Distance = Vector3.Distance(transform.position, Player.position);

            if (MinDistance > Distance) ZombieDistance = WayZombieDistance.MinDistance;
            else if (MiddleDistance > Distance && MinDistance < Distance) ZombieDistance = WayZombieDistance.MiddleDistanceDown;
            else if (MaxDistance < Distance) ZombieDistance = WayZombieDistance.MaxDistance;

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
                    if (Vector3.Distance(createWayZombie.WayZombieList[i].transform.position, zombies_position.DefaultDistance[i]) <= MiddleDistance + 10.0f)
                    {
                        createWayZombie.WayZombieList[i].transform.position = zombies_position.DefaultDistance[i];
                        //createWayZombie.WayZombieList[i].transform.rotation = createWayZombie.transform.rotation;
                        //createWayZombie.WayZombieList[i].transform.parent = createWayZombie.transform;
                        ++count;
                    }
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

}