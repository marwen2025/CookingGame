using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] CuttingTable cuttingTable;
    [SerializeField] private Image barImage;



    private void Start()
    {
        cuttingTable.OnProgressChanged += CuttingTable_OnProgressChanged;
        barImage.fillAmount = 0f;
        hide();
    }

    private void CuttingTable_OnProgressChanged(object sender, CuttingTable.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized==1f ) 
            {
                hide();
        }
        else {
            show(); 
        }
    }
    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }
}
