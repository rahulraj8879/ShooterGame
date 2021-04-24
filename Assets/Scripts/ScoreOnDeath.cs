using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    public int amount;
    private void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(GivePoints);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GivePoints()
    {
        ScoreManager.instance.amount += amount;
    }
   
}
