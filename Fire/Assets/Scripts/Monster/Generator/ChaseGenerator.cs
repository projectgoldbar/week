using System.Collections;
using UnityEngine;

public class ChaseGenerator : GeneratorBase
{
    public void OnEnable()
    {
        Process();
    }

    public override void Process()
    {
        base.Process();
        anim.SetBool("Attack", false);
        anim.SetBool("Walk", true);
        agent.stoppingDistance = 2;
        StartCoroutine(AgentSetPosition());
    }

    private IEnumerator AgentSetPosition()
    {
        for (; ; )
        {
            agent.destination = Ref.Instance.playerTr.position;
            yield return new WaitForSeconds(0.05f);
        }
    }
}