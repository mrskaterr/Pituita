using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PendriveMission : MissionObject,IInteractable
{
   [Networked] public int Rand {get;set;}
   [SerializeField] private int Max=2;
   [SerializeField] Transform Pendrive;
    private int i=0;
    void Start()
    {
    }


    protected override void OnInteract(GameObject @object)
    {
        if(i<Max)
            Rand=Random.Range(i,Max);
        if(Rand==Max-1)
        {
            Done();
            Pendrive.SetParent(@object.GetComponent<Equipment>().itemHolder);
        }
        i++;
    }
    void Done()
    {
        Pendrive.gameObject.SetActive(true);
        Debug.Log("Szafka");
        mission.NextStep();
    }
}
