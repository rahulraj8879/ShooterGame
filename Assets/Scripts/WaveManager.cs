using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public List<WaveSpawner> waves;
    public UnityEvent onChanged;


    public void AddWave(WaveSpawner wave)
    {
        waves.Add(wave);
        onChanged.Invoke();
    }
    public void Removewave(WaveSpawner wave)
    {
        waves.Remove(wave);
        onChanged.Invoke();
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated Wave Manager, ignoring this one", gameObject);
        }
    }
    // Start is called before the first frame update
   
}
