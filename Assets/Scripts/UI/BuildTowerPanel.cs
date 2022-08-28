using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildTowerPanel : MonoBehaviour
{
    public TMP_Text ButtonText;
    public TMP_Text CostText;

    public Button BuildButton;

    public TowerBase Base;
    public TowerSO TowerData;

    public void Build()
    {
        if (GameManager.Instance.Credits >= TowerData.Cost)
        {
            GameManager.Instance.Events.CreditSpent(TowerData.Cost);
            GameObject go = Instantiate(TowerData.Prefab, Base.transform.position, Quaternion.identity);
            Tower tower = go.GetComponent<Tower>();
            tower.TowerData = TowerData;
            Destroy(Base.gameObject);
            GameManager.Instance.Events.TowerBuilt(tower);
        }
    }

    public void SetData(TowerSO tower)
    {
        TowerData = tower;
        ButtonText.text = TowerData.TowerName;
        CostText.text = "$ " + TowerData.Cost;
    }
}
