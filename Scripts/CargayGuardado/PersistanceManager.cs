using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceManager : MonoBehaviour
{
    public List<PersistentScriptableObjects> saveObjects;

    private void OnEnable()
    {
        for (int i = 0; i < saveObjects.Count; i++)
        {
            var so = saveObjects[i];
            so.Load();
        } 
    }
    private void OnDisable()
    {
        for (int i = 0; i < saveObjects.Count; i++)
        {
            var so = saveObjects[i];
            so.Save();
        }
    }
}
