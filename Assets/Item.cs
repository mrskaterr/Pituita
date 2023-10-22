using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MissionObject,IInteractable
{
    [SerializeField] Transform defaultPartent;
    [SerializeField] EnumItem.Item item;
    Collider coll;
    private const string interactableLayerName = "Interactable";
    private const string notvisible = "Default";
    private int index;
    void Start()
    {
        coll=transform.GetComponent<Collider>();
        index=(int)item;
        
    }

    void Update()
    {
        if(transform.parent==defaultPartent){}

        else if(transform.GetComponentInParent<Morph>() 
        && transform.GetComponentInParent<Morph>().index==-1)
        {
            transform.position=transform.parent.position;
            this.gameObject.layer=LayerMask.NameToLayer(interactableLayerName);
        }
        else
        {
            SetedefaultPartent();
        }
    }
    protected override void OnInteract(GameObject @object)
    {
        if(@object.GetComponent<Equipment>().itemHolder.childCount==0)
        {
            coll.enabled=false;
            
            @object.GetComponent<Equipment>().Add(transform);
        }
        
    }
    public int Index()
    {
        return index;
    }
    public void SetedefaultPartent()
    {
        coll.enabled=true;
        transform.SetParent(defaultPartent);
        this.gameObject.layer=LayerMask.NameToLayer(interactableLayerName);
    }

}
