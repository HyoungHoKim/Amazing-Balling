using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsProp;

    // particleSystem 컴포넌트와 AudioSource 컴포넌트가 붙어 있는 'PlasmaExplosionEffect' 오브젝트를 붙여줄 것
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f; // 주변에 뿌릴 수 있는 최대 데미지
    public float explosionForce = 1000f; // 폭발 반경 내로 주변에 뿌리는 힘
    public float lifeTime = 10f; // 불의 수명
    public float explosionRadius = 20f; // 폭발 반경

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime); // 자기 자신을 10초 뒤에 파괴
    }

    private void OnTriggerEnter(Collider other) {
        // 레이어가 Prop인 것만 가져옴 
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);


            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);

            targetProp.TakeDamage(damage);
        }

        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
         // Ball과 Prop의 거리
        Vector3 explosionToTarget = targetPosition - transform.position;
        float distance = explosionToTarget.magnitude;
        
        // 폭발 반경 중심과의 거리 
        float edgeToCenterDistance = explosionRadius - distance;

        // 중심에서 멀수록 데미지 경감
        float percentage = edgeToCenterDistance / explosionRadius;
        float damage = maxDamage * percentage;
        damage = Mathf.Max(0, damage);

        return damage;
    }
}
