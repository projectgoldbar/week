using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject Enemy;
    private Coroutine coroutine;
    private WaitForSeconds second;
    private ParticlePool particlePool;

    public float duration;
    private bool isUsed = false;

    private void Awake()
    {
        second = new WaitForSeconds(duration);
        particlePool = FindObjectOfType<ParticlePool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TrapOn();
    }

    private void TrapOn()
    {
        if (!isUsed)
        {
            var soundManager = SoundManager.Instance;
            coroutine = StartCoroutine(InstanceZombie());
            Camera.main.GetComponent<CameraFallow>().CameraShake(0.2f);
            var x = particlePool.GetParticle(particlePool.trapParticlePool);

            soundManager.PlayBGM(soundManager.trapBgm, soundManager.SoundDic["trap"], false, 0);

            x.transform.position = transform.position;
            x.transform.rotation = transform.rotation;
            x.SetActive(true);
            isUsed = true;
        }
    }

    private IEnumerator InstanceZombie()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return second;
            GameObject.Instantiate(Enemy, transform.position, Quaternion.identity);
            yield return second;
        }
        yield break;
    }
}