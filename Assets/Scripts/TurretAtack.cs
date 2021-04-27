using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAtack : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    public float attackRate = 1; //¹¥»÷ÆµÂÊ
    private float timer = 1;
    public Transform firePosition; //¹¥»÷Ä¿±êÎ»ÖÃ
    public GameObject attackWeaponPrefab; //ÅÚµ¯ÀàÐÍ
    public Transform headPosition;
    private void OnTriggerEnter(Collider col) //¼ì²âµÐÈË½øÈë¹¥»÷·¶Î§
    {
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col) //¼ì²âµÐÈËÀë¿ª¹¥»÷·¶Î§
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

    void Attack() //¹¥»÷µÐÈË
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

    void UpdateEnemy() //¸üÐÂµÐÈËÐÅÏ¢
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
