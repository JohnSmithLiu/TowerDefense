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
        timer += Time.deltaTime;
        if (enemies.Count > 0)
        {
            if (enemies[0] != null)
            {
                Vector3 targetPosition = enemies[0].transform.position;
                targetPosition.y = headPosition.position.y;
                headPosition.LookAt(targetPosition);
            }
            if (timer >= attackRate)
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
