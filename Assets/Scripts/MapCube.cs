using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretFlag;
    public GameObject buildEffect;

    private new Renderer renderer;
    private Color defaultColor;

    public void BuildTurret(GameObject turretPrefab) //建造炮塔
    {
        renderer.material.color = defaultColor;
        turretFlag = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
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
}
