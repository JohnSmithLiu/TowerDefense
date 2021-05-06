using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretFlag;
    public GameObject buildEffect;
    public bool isUpgrade;
    public TurretData turretDate;
    public int cost;

    private new Renderer renderer;
    private Color defaultColor;

    public void BuildTurret(TurretData turretDate) //建造炮塔
    {
        this.turretDate = turretDate;
        isUpgrade = false;
        renderer.material.color = defaultColor;
        cost = (int)(turretDate.cost * 0.8f);
        turretFlag = GameObject.Instantiate(turretDate.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    private void OnMouseEnter()
    {
        if (turretFlag == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.red;
        }
    }
    private void OnMouseExit()
    {
        renderer.material.color = defaultColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        defaultColor = renderer.material.color;
        cost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeTurret()
    {
        Destroy(turretFlag);
        isUpgrade = true;
        cost += (int)(turretDate.costUpgrade * 0.8f);
        turretFlag = GameObject.Instantiate(turretDate.turretUpgradePrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    public void DestoryTurret()
    {
        Destroy(turretFlag);
        turretFlag = null;
        isUpgrade = false;
    }
}
