using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDefinitions : MonoBehaviour
{
    [System.Serializable]
    public class CharacterStats
    {
        public enum StatusTypes
        {
            idle, walk, run, jump, dead, injured
        };

        public StatusTypes status;
        public float lastHitTime;
        public float TIME_BETWEEN_ENEMY_HIT = 2;
        public float TIME_WITH_HIT_EXPRESSION = 1;
    }

    CollectableAcquirer collectableAcquirerScript;
    GameObject gameController;
    public CharacterStats stats;
    // Start is called before the first frame update
    void Awake()
    {
        DefineCollectableAcquirer();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        stats = new CharacterStats();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    // Define how do collectables affect the character (or GameController) when taking them.
    void DefineCollectableAcquirer()
    {
        collectableAcquirerScript = this.GetComponent<CollectableAcquirer>();
        collectableAcquirerScript.onRedShardAcquire = () =>
        {
            ItemCollectionControl icc = gameController.GetComponent<ItemCollectionControl>();
            icc.SetRedShardCount(icc.GetRedShardCount() + 1);
        };
        collectableAcquirerScript.onBlueShardAcquire = () =>
        {
            ItemCollectionControl icc = gameController.GetComponent<ItemCollectionControl>();
            icc.SetBlueShardCount(icc.GetBlueShardCount() + 1);
        };
    }
}
