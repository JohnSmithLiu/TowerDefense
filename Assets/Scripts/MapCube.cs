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

    private new Renderer renderer;
    private Color defaultColor;
    public TurretData turretDate;

    public void BuildTurret(TurretData turretDate) //建造炮塔
    {
        this.turretDate = turretDate;
        isUpgrade = false;
        renderer.material.color = defaultColor;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeTurret()
    {
        Destroy(turretFlag);
        isUpgrade = true;
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
