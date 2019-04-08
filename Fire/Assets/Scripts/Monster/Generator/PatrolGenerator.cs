public class PatrolGenerator : GeneratorBase
{
    private PatrolGenerator Patrol;

    private void OnEnable()

    {
        Process();
    }

    public override void Start()
    {
        Patrol = this;
        Patrol.enabled = false;
    }

    public override void Process()
    {
        base.Process();
        Process1();
    }

    private void Process1()
    {
        RandomAngle random = new RandomAngle();

        anim.SetBool("Walk", true);
        agent.stoppingDistance = 0;
        var randomPos = transform.position + random.RandomPosition();
        agent.destination = randomPos;

        Invoke("Process2", UnityEngine.Random.Range(2, 5));
    }

    private void Process2()
    {
        anim.SetBool("Walk", false);
        agent.stoppingDistance = 0;
        agent.destination = transform.position;
        Invoke("Process1", UnityEngine.Random.Range(2, 5));
    }
}