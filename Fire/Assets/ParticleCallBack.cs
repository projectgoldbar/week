using UnityEngine;

public class ParticleCallBack : MonoBehaviour
{
    public PlayerData playerData;
    void OnParticleSystemStopped()
    {
        playerData.clearParticle.Play();
        Time.timeScale = 1f;
    }
}
