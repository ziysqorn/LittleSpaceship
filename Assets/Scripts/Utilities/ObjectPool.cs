using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    protected GameObject objPref;
    private int poolSize = 20;
    Queue<GameObject> objQueue = new Queue<GameObject>();

    public ObjectPool(in GameObject pref)
    {
        objPref = pref;
		if (objPref)
		{
			for (int i = 0; i < poolSize; ++i)
			{
				GameObject newObj = GameObject.Instantiate(objPref);
				newObj.SetActive(false);
				objQueue.Enqueue(newObj);
			}
		}
	}

    public GameObject GetObj(Vector3 position, Quaternion rotation)
    {
        if(objQueue.Count == 0 && objPref)
        {
			GameObject newObj = GameObject.Instantiate(objPref, position, rotation);
			newObj.SetActive(false);
			objQueue.Enqueue(newObj);
		}
        GameObject frontObj = objQueue.Dequeue();
        frontObj.SetActive(true);
        frontObj.transform.position = position;
        frontObj.transform.rotation = rotation;
        return frontObj;
    }

    public void RetrieveObj(in GameObject obj)
    {
        if (obj)
        {
			obj.SetActive(false);
			objQueue.Enqueue(obj);
        }
    }
}
