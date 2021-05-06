using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10; //移动速度
    private Transform[] positions; //移动路径
    private int index = 0; //移动路径点
    private float totalHp;
    public float hp = 300;
    public Slider hpSlider;
    public GameObject dieEffectPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        positions = PathPoint.positions;
        totalHp = hp;
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

    void ReachDestination() //到达目的地，销毁目标
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.enemyCount--;
    }

    public void TakeDamage(float damage) //被攻击掉血
    {
        if (hp <= 0)
        {
            return;
        }
        else
        {
            if (hp >= damage)
            {
                hp -= damage;
            }
            else
            {
                hp = 0;
            }
            hpSlider.value = hp / totalHp;
            if (hp == 0)
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
