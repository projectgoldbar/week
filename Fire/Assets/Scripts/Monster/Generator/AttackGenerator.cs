using System.Collections;
using UnityEngine;

public class AttackGenerator : GeneratorBase
{
    private void OnEnable()
    {
        Process();
    }

    public override void Process()
    {
        base.Process();

        anim.SetBool("Attack", true);
        agent.stoppingDistance = 0;
        agent.destination = transform.position;

        StartCoroutine(dir());
    }

    private IEnumerator dir()
    {
        for (; ; )
        {
            var direction = Ref.Instance.playerTr.position - transform.position;
            direction.y = 0;
            Quaternion rot = Quaternion.LookRotation(direction.normalized);

            transform.rotation = rot;

            yield return new WaitForSeconds(1f);
        }
    }
}