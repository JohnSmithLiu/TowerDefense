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
    public GameObject upgradeControl;
    public Button buttonUpgrade;

    private TurretData selectedTurretDate; //当前选择的炮台
    private MapCube selectedMapCube; //当前选择的方块
    private Animator upgradeAnimator;
    private bool laserSelect = false;
    private bool missleSelect = false;
    private bool standardSelect = false;

    public Text moneyText;
    public Animator moneyAnimator;

    void UpdateMoney(int offset = 0)
    {
        Player.Instances.money += offset;
        moneyText.text = "$" + Player.Instances.money;
    }

    public void OnLaserSelected(bool isOn)
    {
        laserSelect = !laserSelect;
        if (laserSelect)
        {
            selectedTurretDate = laserTurretDate;
        }
        else
        {
            selectedTurretDate = null;
        }
    }

    public void OnMissleSelected(bool isOn)
    {
        missleSelect = !missleSelect;
        if (missleSelect)
        {
            selectedTurretDate = missleTurretDate;
        }
        else
        {
            selectedTurretDate = null;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        standardSelect = !standardSelect;
        if (standardSelect)
        {
            selectedTurretDate = standardTurretDate;
        }
        else
        {
            selectedTurretDate = null;
        }
    }

    void ShowUI(Vector3 position, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUI");
        upgradeControl.SetActive(false);
        position.y = 3.5f;
        upgradeControl.transform.position = position;
        upgradeControl.SetActive(true);
        buttonUpgrade.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUI()
    {
        upgradeAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        upgradeControl.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (Player.Instances.money >= selectedMapCube.turretDate.costUpgrade)
        {
            UpdateMoney(-selectedMapCube.turretDate.costUpgrade);
            selectedMapCube.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("MoneyFlash");
        }
        StartCoroutine(HideUI());
    }

    public void OnDestoryButtonDown()
    {
        UpdateMoney(selectedMapCube.cost);
        selectedMapCube.DestoryTurret();
        StartCoroutine(HideUI());
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedTurretDate = null;
        upgradeAnimator = upgradeControl.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //鼠标是否触碰到MapCube
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretDate != null && mapCube.turretFlag == null)
                    {
                        if (Player.Instances.money >= selectedTurretDate.cost)
                        {
                            //可以建造炮台
                            UpdateMoney(-selectedTurretDate.cost);
                            mapCube.BuildTurret(selectedTurretDate);
                        }
                        else
                        {
                            //金钱不足
                            moneyAnimator.SetTrigger("MoneyFlash");
                        }
                    }
                    else if (mapCube.turretFlag != null)
                    {
                        //升级炮台判断
                        if (mapCube == selectedMapCube && upgradeControl.activeInHierarchy)
                        {
                            StartCoroutine(HideUI());
                        }
                        else
                        {
                            ShowUI(mapCube.transform.position, mapCube.isUpgrade);
                        }
                        selectedMapCube = mapCube;
                    }
                }
            }
        }
    }
}
