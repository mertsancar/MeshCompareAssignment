using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshCompare : MonoBehaviour
{

    public Transform obj1;
    public Transform obj2;
    private List<Transform> obj1List = new List<Transform>();
    private List<Transform> obj2List = new List<Transform>();
    // private List<ArrayList> obj1ArrayList = new List<ArrayList>();
    // private List<ArrayList> obj2ArrayList = new List<ArrayList>();
    private IDictionary<string, Transform>  obj1withUniqueIdDict = new Dictionary<string, Transform>();
    private IDictionary<string, Transform> obj2withUniqueIdDict = new Dictionary<string, Transform>();
    private List<Transform> newElements = new List<Transform>();
    private List<Transform> deletedElements = new List<Transform>();
    private List<Transform> revisedElements = new List<Transform>();
    void Start()
    {
        getAllChilds(obj1, obj1List);
        getAllChilds(obj2, obj2List);
        setUniqueIDList(obj1List, obj1withUniqueIdDict);
        setUniqueIDList(obj2List, obj2withUniqueIdDict);
        compareLists(obj1withUniqueIdDict, obj2withUniqueIdDict);

        Debug.Log("ALL ITEM IN RevisedBasicproject: " + obj1List.Count());
        Debug.Log("ALL ITEM IN RevisedBasicproject_4: " + obj2List.Count());

        Debug.Log("NEW ELEMENTS IN RevisedBasicproject_4: "+newElements.Count());
        Debug.Log("DELETED ELEMENTS IN RevisedBasicproject_4: "+deletedElements.Count());
        Debug.Log("REVISED ELEMENTS IN RevisedBasicproject_4: "+revisedElements.Count());

        foreach (Transform item in newElements)
        {
            Debug.Log("NEW: " + item.gameObject.name);
        }

        foreach (Transform item in deletedElements)
        {
            Debug.Log("DELETED: " + item.gameObject.name);
        }

        foreach (Transform item in revisedElements)
        {
            Debug.Log("REVISED: " + item.gameObject.name);
        }
        

    }

    void Update()
    {
        
    }

    void getAllChilds(Transform obj, List<Transform> goList){

        if (obj.childCount > 0)
        {
            for (int i = 0; i < obj.childCount; i++)
            {
                getAllChilds(obj.GetChild(i), goList);
                goList.Add(obj.GetChild(i));
            }   
        }
    }

    void setUniqueIDList(List<Transform> goList, IDictionary<string, Transform>  goDictwithUniqueID){

        foreach (Transform item in goList)
        {
            try
            {
                string uniqueID = item.name.Substring(item.name.Length - 20); 
                goDictwithUniqueID.Add(uniqueID, item);
            }
            catch (System.Exception)
            {
                continue;
            }
        }
    }

    void compareLists(IDictionary<string, Transform>obj1withUniqueIdDict, IDictionary<string, Transform> obj2withUniqueIdDict){
        foreach (string obj2uniqueID in obj2withUniqueIdDict.Keys)
        {
            foreach (string obj1uniqueID in obj1withUniqueIdDict.Keys)
            {
                if (obj1uniqueID == obj2uniqueID)
                {
                    if (obj1withUniqueIdDict[obj1uniqueID].position != obj2withUniqueIdDict[obj2uniqueID].position || obj1withUniqueIdDict[obj1uniqueID].rotation != obj2withUniqueIdDict[obj2uniqueID].rotation)
                    {
                        revisedElements.Add(obj2withUniqueIdDict[obj2uniqueID]);
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                
                if (!obj2withUniqueIdDict.Keys.Contains(obj1uniqueID))
                {
                    if (!deletedElements.Contains(obj1withUniqueIdDict[obj1uniqueID]))
                    {
                        deletedElements.Add(obj1withUniqueIdDict[obj1uniqueID]);
                    }
                }

                if(!obj1withUniqueIdDict.Keys.Contains(obj2uniqueID))
                {   
                    if (!newElements.Contains(obj2withUniqueIdDict[obj2uniqueID]))
                    {
                        newElements.Add(obj2withUniqueIdDict[obj2uniqueID]);   
                    }
                }
            } 
        }

    }



}
