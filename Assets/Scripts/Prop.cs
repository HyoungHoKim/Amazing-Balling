using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int score = 5;
    public ParticleSystem explosionParticle; // 프롭이 파괴될 때 재생되는 particle
    public float hp = 10f;

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // 파티클 생성
            ParticleSystem instance = Instantiate(explosionParticle, transform.position, transform.rotation);

            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play();

            Destroy(instance.gameObject, instance.main.duration);
            gameObject.SetActive(false);
        }
    }
}
