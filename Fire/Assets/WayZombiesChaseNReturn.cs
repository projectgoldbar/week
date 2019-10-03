using System.Collections;
using UnityEngine;
using ZombieState;
using System.Collections.Generic;

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
    private System.Action ReturnEvent;
    private System.Action Noactivation;

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

    private void OnEnable()
    {
        minEvent += Min;
        middleEvent += Middle;
        maxEvent += Max;
        ReturnEvent += returnProcess;

    }

    private void Start()
    {
        controll.b_Moving = true;
    }

    int count = 0;


    private void FixedUpdate()
    {
        Distance = Vector3.Distance(transform.position, Player.position);

        if (MinDistance > Distance)                                     ZombieDistance = WayZombieDistance.MinDistance;
        else if (MiddleDistance > Distance && MinDistance < Distance)   ZombieDistance = WayZombieDistance.MiddleDistanceDown;
        else if (MaxDistance < Distance)                                ZombieDistance = WayZombieDistance.MaxDistance;

        

    }

    public void returnProcess()
    {
        Debug.Log("좀비들 정지");
        for (int j = 0; j < createWayZombie.WayZombieList.Count; j++)
        {
            if (createWayZombie.WayZombieList[j].gameObject.activeSelf)
            createWayZombie.WayZombieList[j].GetComponent<ZombieState.Zombie_Moving>().target = zombies_position.ZombiesWayPosition[j];
        }
        StartCoroutine(gototheHome());
    }

    IEnumerator FallAnim(ZombieState.ZombiesComponent Z)
    {
        Z.animator.SetTrigger("Fall");
        if(Z.animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            Z.animator.speed = Random.Range(0.05f, 1.0f);

        yield return null;
    }



    public IEnumerator GoHome(GameObject ob,int index)
    {
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(ob.transform.position);
        if (targetScreenPos.x > Screen.width+1.0f || targetScreenPos.x < -1.0f || targetScreenPos.y > Screen.height+1.0f || targetScreenPos.y < -1.0f)
        {

            ob.transform.position = zombies_position.DefaultDistance[index];
            ob.transform.rotation = createWayZombie.transform.rotation;
            ob.gameObject.SetActive(false);
            ++count;
            Debug.Log($"복귀한좀비{count}");
        }
        yield return null;
    }

    public IEnumerator gototheHome()
    {
        while (!controll.b_Moving)
        {
            for (int j = 0; j < createWayZombie.WayZombieList.Count; j++)
            {
                if(createWayZombie.WayZombieList[j].activeSelf)
                StartCoroutine(GoHome(createWayZombie.WayZombieList[j], j));
            }
            if (count >= 10)
            {
                count = 0;
                controll.b_Moving = true;
                yield break;
            }

            yield return null;
        }
    }


    public void Min()
    {
        int max = createWayZombie.WayZombieList.Count;
        for (int i = 0; i < max; i++)
        {
            controll.b_Moving = false;
            createWayZombie.WayZombieList[i].transform.parent = null;
            createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().target = Player.transform;
        }
        b_return = false;
        Noactivation -= activation;
        maxEvent += Max;
        minEvent -= Min;
    }

    public void Middle()
    {
        int max = createWayZombie.WayZombieList.Count;
        for (int i = 0; i < max; i++)
        {
            if (!createWayZombie.WayZombieList[i].activeSelf)
            {
                createWayZombie.WayZombieList[i].transform.position = zombies_position.DefaultDistance[i];
                createWayZombie.WayZombieList[i].gameObject.SetActive(true);

                //createWayZombie.WayZombieList[i].GetComponent<Zombie_Moving>().MoveStart();
                createWayZombie.WayZombieList[i].GetComponent<ZombieState.Zombie_Moving>().target = zombies_position.ZombiesWayPosition[i];
            }
        }
        Noactivation += activation;
        minEvent += Min;
        middleEvent -= Middle;
    }

    public void Max()
    {
        ReturnEvent?.Invoke();
        Noactivation?.Invoke();
        b_return = true;
        
        middleEvent += Middle;
        maxEvent -= Max;
    }


    public void activation()
    {
        int max = createWayZombie.WayZombieList.Count;
        for (int i = 0; i < max; i++)
        {
            if (createWayZombie.WayZombieList[i].activeSelf)
                createWayZombie.WayZombieList[i].gameObject.SetActive(false);
        }
        Noactivation -= activation;
    }
}