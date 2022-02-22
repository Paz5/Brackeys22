using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private GameEvent resetEvent;
    
    [SerializeField] private FloatVariable cageSpawnDelay;
    [SerializeField] private FloatVariable repairSpawnDelay;
    [SerializeField] private FloatVariable mapSpawnDelay;

    [SerializeField] private FloatVariable cageTimeWindow;
    [SerializeField] private FloatVariable repairTimeWindow;
    [SerializeField] private FloatVariable mapTimeWindow;
    
    void Update()
    {
        
    }
}
