using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10; //�ƶ��ٶ�
    private Transform[] positions; //�ƶ�·��
    private int index = 0; //�ƶ�·����
    public int hp = 300;
    public GameObject dieEffectPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        positions = PathPoint.positions;
    }

    // Update is called once per frame
    void Update()
    {
        if (index < positions.Length)
        {
            Move();
        }
    }

    void Move()
    {
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if ( Vector3.Distance(positions[index].position, transform.position) < 0.2f )
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination() //����Ŀ�ĵأ�����Ŀ��
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.enemyCount--;
    }

    public void TakeDamage(int damage) //��������Ѫ
    {
        if (hp <= 0)
        {
            return;
        }
        else
        {
            hp -= damage;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(dieEffectPrefabs, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);
        Destroy(effect, 1.2f);
    }
}
