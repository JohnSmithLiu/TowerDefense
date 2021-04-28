using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAtack : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    public float attackRate = 1; //����Ƶ��
    private float timer = 1;
    public Transform firePosition; //����Ŀ��λ��
    public GameObject attackWeaponPrefab; //�ڵ�����
    public Transform headPosition;
    public bool isUseLaser = false;
    public float laserDamage = 70;
    public LineRenderer laserRenderer;
    private void OnTriggerEnter(Collider col) //�����˽��빥����Χ
    {
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col) //�������뿪������Χ
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
                if (enemies[0] == null)
                {
                    UpdateEnemy();
                }
                if (enemies.Count > 0)
                {
                    laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemies[0].transform.position });
                }
            }
            else
            {
                laserRenderer.enabled = false;
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

    void Attack() //��������
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

    void UpdateEnemy() //���µ�����Ϣ
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
