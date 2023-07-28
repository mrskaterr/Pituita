using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PendriveMission : MissionObject,IInteractable
{
   [Networked] public int Rand {get;set;}
   [SerializeField] private int Max=2;
   [SerializeField] Collider nextMission;
   [SerializeField] GameObject Pendrive;
    private int i=0;
    public bool isDone=false;
    protected override void OnInteract()
    {
        if(i<Max)
            Rand=Random.Range(i,Max);
        if(Rand==Max-1)
            Done();
        i++;
    }
    void Done()
    {
        Debug.Log("Szafka");
        isDone=true;
        transform.GetComponent<Collider>().enabled=false;
        nextMission.enabled=true;
        mission.NextStep();
    }
}
