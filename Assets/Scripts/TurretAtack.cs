using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAtack : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    public float attackRate = 1; //炮塔攻击间隔
    private float timer = 1;
    public Transform firePosition; //炮塔攻击点
    public GameObject attackWeaponPrefab; //炮塔子弹类型
    public Transform headPosition;
    public bool isUseLaser = false;
    public float laserDamage = 1;
    public LineRenderer laserRenderer;
    public GameObject laserEffect;
    private void OnTriggerEnter(Collider col) //敌人进入攻击范围，开始攻击
    {
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col) //敌人离开攻击范围，更新敌人列表
    {
        if (col.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && enemies[0] != null)
        {
            Vector3 targetPosition = enemies[0].transform.position;
            targetPosition.y = headPosition.position.y;
            headPosition.LookAt(targetPosition);
        }
        if (isUseLaser)
        {
            if (enemies.Count > 0)
            {
                laserRenderer.enabled = true;
                laserEffect.SetActive(true);    
                if (enemies[0] == null)
                {
                    UpdateEnemy();
                }
                if (enemies.Count > 0)
                {
                    laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemies[0].transform.position });
                    enemies[0].GetComponent<Enemy>().TakeDamage(laserDamage * Time.deltaTime);
                    laserEffect.transform.position = enemies[0].transform.position;
                    Vector3 pos = transform.position;
                    pos.y = enemies[0].transform.position.y;
                    laserEffect.transform.LookAt(pos);
                }
            }
            else
            {
                laserRenderer.enabled = false;
                laserEffect.SetActive(false);
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (enemies.Count > 0 && timer >= attackRate)
            {
                timer = 0;
                Attack();
            }
        }
    }

    void Attack() //攻击敌人
    {
        if (enemies[0] == null)
        {
            UpdateEnemy();
        }
        if (enemies.Count <= 0)
        {
            timer = attackRate;
            return;
        }
        GameObject weapon = GameObject.Instantiate(attackWeaponPrefab, firePosition.position, firePosition.rotation);
        weapon.GetComponent<Weapon>().SetTarget(enemies[0].transform);
    }

    void UpdateEnemy() //更新敌人列表
    {
        for (int index = enemies.Count - 1; index >= 0; index--)
        {
            if (enemies[index] == null)
            {
                enemies.RemoveAt(index);
            }
        }
    }
}
