using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectionControl : MonoBehaviour
{
    private int redShardCount = 0;
    private int blueShardCount = 0;
    public Text uiRedCrystalNumber;
    public Text uiBlueCrystalNumber;
    // Start is called before the first frame update
    void Start()
    {
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRedShardCount(int n)
    {
        redShardCount = n;
        updateUI();
    }

    public int GetRedShardCount()
    {
        return redShardCount;
    }

    public void SetBlueShardCount(int n)
    {
        blueShardCount = n;
        updateUI();
    }

    public int GetBlueShardCount()
    {
        return blueShardCount;
    }

    private void updateUI()
    {
        uiRedCrystalNumber.text = redShardCount.ToString();
        uiBlueCrystalNumber.text = blueShardCount.ToString();
    }
}
