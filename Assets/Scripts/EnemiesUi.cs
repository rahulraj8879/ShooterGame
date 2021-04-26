using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesUi : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        EnemyManager.instance.onChanged.AddListener(RefreshText);
    }
    void RefreshText()
    {
        text.text = " Remaining Enemies: " + EnemyManager.instance.enemies.Count;
    }

   
}
