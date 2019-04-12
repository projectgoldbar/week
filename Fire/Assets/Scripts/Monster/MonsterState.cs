using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public NavMeshAgent Agent;
    public MonoBehaviour mono;
    // public State StateBase = null;

    [Header("안넣어도됨 상태머신에서 집어넣음.")]
    public GeneratorBase StateBase = null;

    private PatrolGenerator Patrol;
    private ChaseGenerator Chase;
    private AttackGenerator Attack;

    private List<GeneratorBase> generators = new List<GeneratorBase>();

    private void OnEnable()
    {
        ChangeState(StateIndex.PATROL);
    }

    private void Awake()
    {
        StateSetting();
        ListAddSetting();
    }

    private void StateSetting()
    {
        Patrol = GetComponent<PatrolGenerator>();
        Chase = GetComponent<ChaseGenerator>();
        Attack = GetComponent<AttackGenerator>();
    }

    private void ListAddSetting()
    {
        generators.Add(Patrol);
        generators.Add(Chase);
        generators.Add(Attack);
    }

    public void ChangeState(StateIndex nextState)
    {
        if (StateBase != null)
            StateBase.Exit();

        StateBase = CreateStateInstance(nextState);

        StateBase.Initiate();
    }

    public GeneratorBase CreateStateInstance(StateIndex NextState)
    {
        GeneratorBase generator = null;

        switch (NextState)
        {
            case StateIndex.PATROL:
                generator = Generator_activation(generator, Patrol);
                Generator_Inactive(Patrol);
                break;

            case StateIndex.CHASE:
                generator = Generator_activation(generator, Chase);
                Generator_Inactive(Chase);
                Agent.avoidancePriority = 50;
                break;

            case StateIndex.ATTACK:
                generator = Generator_activation(generator, Attack);
                Generator_Inactive(Attack);
                Agent.avoidancePriority = 51;
                break;
        }
        return generator;
    }

    private GeneratorBase Generator_activation(GeneratorBase generator, GeneratorBase Base)
    {
        generator = Base;
        generator.enabled = true;

        return generator;
    }

    private void Generator_Inactive(GeneratorBase Base)
    {
        var list = generators.FindAll(x => x != Base);

        foreach (var item in list)
        {
            item.enabled = false;
        }
    }
}