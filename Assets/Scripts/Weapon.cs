using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1; //�����˺�
    public float speed = 20; //�����ٶ�
    private Transform target; //����Ŀ��
    public GameObject explosionEffectPrefab; //�ӵ���ը��Ч

    public void SetTarget(Transform _target) //���ù���Ŀ��
    {
        target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //��Ŀ�귽���˶�
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col) //��Ŀ����ײ�󴥷���Ч�͵�Ѫ
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }

    void Die()
    {
        GameObject explosionEffect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(explosionEffect, 1);
    }
}
