using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1; //炮弹伤害
    public float speed = 20; //炮弹移动速度
    private Transform target; //攻击目标
    public GameObject explosionEffectPrefab; //攻击特效

    public void SetTarget(Transform _target) //设置攻击目标
    {
        target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col) //攻击命中，触发攻击特效
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
