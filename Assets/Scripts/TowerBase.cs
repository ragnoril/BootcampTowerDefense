using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{

    public GameObject BuildTowerPanelPrefab;

    public Transform UIPanel;

    private void Start()
    {
        foreach(TowerSO tower in GameManager.Instance.TowerList)
        {
            GameObject go = Instantiate(BuildTowerPanelPrefab);
            go.transform.SetParent(UIPanel);
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            Vector3 pos = go.GetComponent<RectTransform>().localPosition;
            pos.z = 0f;
            go.GetComponent<RectTransform>().localPosition = pos;
            BuildTowerPanel towerPanel = go.GetComponent<BuildTowerPanel>();
            towerPanel.Base = this;
            towerPanel.SetData(tower);
        }

        GameManager.Instance.Events.OnBaseSelected += BaseSelected;
        GameManager.Instance.Events.OnTowerSelected += TowerSelected;

    }

    private void OnDestroy()
    {
        GameManager.Instance.Events.OnBaseSelected -= BaseSelected;
        GameManager.Instance.Events.OnTowerSelected -= TowerSelected;
    }

    private void TowerSelected(Tower obj)
    {
        UIPanel.gameObject.SetActive(false);
    }

    private void BaseSelected(TowerBase towerBase)
    {
        if (towerBase != this)
        {
            UIPanel.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        UIPanel.gameObject.SetActive(true);
        GameManager.Instance.Events.TowerBaseSelected(this);
    }




}
