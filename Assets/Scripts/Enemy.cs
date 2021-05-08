using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10; //敌人移动速度
    private Transform[] positions; //敌人移动轨迹
    private int index = 0;
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

    void ReachDestination() //到达终点，Player Hp-1
    {
        Player.Instances.hp --;
        if (Player.Instances.hp <= 0)
        {
            GameObject.Destroy(this.gameObject);
            GameManager.Instance.Failed();
        }
    }

    void OnDestroy()
    {
        EnemySpawner.enemyCount--;
    }

    public void TakeDamage(float damage) //受到伤害
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
