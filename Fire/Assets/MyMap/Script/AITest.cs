using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public MonsterDataBase monsterDataBase;

    public Transform target;

    private void Start()
    {
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //StartCoroutine(Invoking2());
        //LookAt();
        StartCoroutine(Invoking2());
    }

    private void SetpathObject(NavMeshAgent agent, NavMeshPath path)
    {
        agent.CalculatePath(target.position, path);
        agent.SetPath(path);
    }

    private IEnumerator Sequence()
    {
        NavMeshHit hit;
        bool isStop = false;
        float distance;
        while (!isStop)
        {
            for (int i = 0; i < monsterDataBase.monsterList.Count; i++)
            {
                var direction = (target.position - monsterDataBase.monsterList[i].position).normalized;
                direction.y = 0f;
                var a = Quaternion.LookRotation(direction);
                var agent = monsterDataBase.monsterList[i].GetComponent<NavMeshAgent>();
                monsterDataBase.monsterList[i].rotation = a;
                agent.velocity = agent.transform.forward * 10f;
            }
            yield return null;
        }
    }

    private IEnumerator Invoking()
    {
        for (int i = 0; i < 500; i++)
        {
            TestCode();
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Invoking2()
    {
        for (; ; )
        {
            CheckDistance();
            SetPath();
            yield return new WaitForSeconds(1f);
        }
    }

    private bool CheckDistance()
    {
        bool answer = true;
        var monsterlist = monsterDataBase.monsterDataList;
        for (int i = 0; i < monsterlist.Count; i++)
        {
            monsterlist[i].distanceToPlayer = Vector3.Distance(monsterlist[i].transform.position, target.position);
            //Debug.Log(monsterlist[i].distanceToPlayer);
        }
        return answer;
    }

    private void CheckWall()
    {
        NavMeshHit hit;
        var list = monsterDataBase.monsterList;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetComponent<NavMeshAgent>().Raycast(target.position, out hit))
            {
                //hit.
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestCode();
        }
        //LookAt();
    }

    private void TestCode()
    {
        NavMeshPath navpath = new NavMeshPath();
        var monsterList = monsterDataBase.monsterList;
        for (int i = 0; i < monsterList.Count; i++)
        {
            var navMeshA = monsterList[i].GetComponent<NavMeshAgent>();
            navMeshA.CalculatePath(target.position, navpath);
            navMeshA.SetPath(navpath);
            navMeshA.avoidancePriority--;
        }

        //for (int i = 0; i < navpath.corners.Length; i++)
        //{
        //    Debug.Log(navpath.corners[i]);
        //}
    }

    private void LookAt(Transform something)
    {
        //NavMeshPath navpath = new NavMeshPath();
        //var monsterList = monsterDataBase.monsterList;
        //for (int i = 0; i < monsterList.Count; i++)
        //{
        //  var a = monsterList[i].GetComponent<NavMeshAgent>();
        //var agent = something.GetComponent<NavMeshAgent>();
        something.LookAt(target);
        //agent.velocity = agent.transform.forward * 3f;
        //}
    }

    private void SetPath()
    {
        var monsterlist = monsterDataBase.monsterDataList;

        //var b = SetPath2(monsterlist[0].transform.gameObject.GetComponent<NavMeshAgent>());
        for (int i = 0; i < monsterlist.Count; i++)
        {
            if (monsterlist[i].distanceToPlayer < 50f)
            {
                monsterlist[i].transform.GetComponent<NavMeshAgent>().SetPath(SetPath2(monsterlist[i].transform.GetComponent<NavMeshAgent>()));
            }
        }
    }

    private NavMeshPath SetPath2(NavMeshAgent monster)
    {
        NavMeshPath navpath = new NavMeshPath();
        var monsterList = monsterDataBase.monsterList;

        monster.CalculatePath(target.position, navpath);
        return navpath;
        //monster.SetPath(navpath);
        //for (int i = 0; i < navpath.corners.Length; i++)
        //{
        //    Debug.Log(navpath.corners[i]);
        //}
    }
}