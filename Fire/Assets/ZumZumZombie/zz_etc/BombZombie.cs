using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BombZombie : MonoBehaviour
{
    public Transform target;

    public float thinkingTime = 1f;
    public Transform playerData;
    public ParticlePool particlePool;

    private WayPoint wayPoint;
    private WaitForSeconds waitForOneSeconds;
    private WaitForSeconds waitFor02Seconds;
    private NavMeshAgent agent;
    private Coroutine checkingNextMove;

    private void Awake()
    {
        wayPoint = FindObjectOfType<WayPoint>();
        playerData = FindObjectOfType<PlayerData>().transform;
        agent = GetComponent<NavMeshAgent>();
        waitForOneSeconds = new WaitForSeconds(5f);
        particlePool = FindObjectOfType<ParticlePool>();
    }

    private void Start()
    {
        checkingNextMove = StartCoroutine(CheckingNextMove());
    }

    private IEnumerator CheckingNextMove()
    {
        while (true)
        {
            yield return null;
            Debug.Log(23);
            if (Vector3.Distance(playerData.position, transform.position) < 15f)
            {
                StartCoroutine(BombSeq());
                StopCoroutine(checkingNextMove);
                yield break;
            }
            else
            {
                SuchPlayer();
                agent.SetDestination(target.position);
                yield return waitForOneSeconds;
            }
        }
    }

    private IEnumerator BombSeq()
    {
        for (int i = 0; i < 45f; i++)
        {
            transform.localScale *= 1 + (i * 0.05f) * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector3(1, 1, 1);
        var a = particlePool.GetParticle(particlePool.nukeParticlePool);
        a.transform.position = transform.position;
        a.SetActive(true);
        gameObject.SetActive(false);
        Camera.main.gameObject.GetComponent<CameraFallow>().CameraShake(0.5f);
        yield break;
    }

    private void SuchWayPoint()
    {
        target = wayPoint.waypoints[Random.Range(0, wayPoint.waypoints.Length)];
    }

    private void SuchPlayer()
    {
        target = playerData;
    }
}