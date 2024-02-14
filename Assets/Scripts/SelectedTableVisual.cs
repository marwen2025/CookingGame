using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTableVisual : MonoBehaviour
{
    [SerializeField] private BaseTable baseTable;
    [SerializeField] private GameObject[] visualGameObjectarray;

    private void Start()
    {
        Player.Instance.OnSelectedTableChanged += Player_OnSelectedTableChanged;
    }

    private void Player_OnSelectedTableChanged(object sender, Player.OnSelectedTableChangedEventArgs e)
    {   
        if (e.selectedTable == baseTable)
        {
            Show();
        }
        else
        {
            Hide();    
        };
    }
    private void Show()
    {
        foreach (GameObject go in visualGameObjectarray)
        {
            go.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject go in visualGameObjectarray)
        {
            go.SetActive(false);
        }
    }
}
