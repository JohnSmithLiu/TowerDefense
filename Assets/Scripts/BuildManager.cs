using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public TurretData laserTurretDate;
    public TurretData missleTurretDate;
    public TurretData standardTurretDate;

    public TurretData selectedTurretDate; //当前选择的炮台

    private int money = 1000;
    public Text moneyText;
    public Animator moneyAnimator;

    void UpdateMoney(int offset = 0)
    {
        money += offset;
        moneyText.text = "$" + money;
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretDate = laserTurretDate;
        }
    }

    public void OnMissleSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretDate = missleTurretDate;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretDate = standardTurretDate;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //建造炮台
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    print("test1111");
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretDate != null && mapCube.turretFlag == null)
                    {
                        print("test");
                        if (money >= selectedTurretDate.cost)
                        {
                            //可以创建
                            UpdateMoney(-selectedTurretDate.cost);
                            mapCube.BuildTurret(selectedTurretDate.turretPrefab);
                        }
                        else
                        {
                            moneyAnimator.SetTrigger("MoneyFlash");
                            //金钱不足
                        }
                    }
                    else if (mapCube.turretFlag != null)
                    {
                        //可以升级
                    }
                }
            }
        }
    }
}
