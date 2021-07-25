using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace BattleCity
{
    public class Cell : MonoBehaviour
    {
        private List<GameObject> cellList = new List<GameObject>();
        private List<GameObject> pointList = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i <= 15; i++)
            {
                cellList.Add(transform.GetChild(i).gameObject);
            }
            for (int i = 16; i <= 40; i++)
            {
                pointList.Add(transform.GetChild(i).gameObject);
                
            }
        }

        public void TakeDamage(GameObject gameObject, string bulletTag)
        {
            var instanceID =  pointList.IndexOf(gameObject);
            switch (bulletTag)
            {
                case "Up" :
                    if ((instanceID - 2) % 5 == 0)
                    {
                        var rowId = ((instanceID - 2) / 5);
                        for (int i = 0; i <= 3; i++)
                        {
                            Destroy(cellList.Find(x => cellList.IndexOf(x) == (rowId * 4) + i));
                        }
                        if (rowId == 3)
                        {
                            Destroy(transform.gameObject);
                        }
                    }
                    break;
                case "Down" : 
                    if ((instanceID - 2) % 5 == 0)
                    {
                        var rowId = ((instanceID - 2) / 5);
                        for (int i = 4; i >= 1; i--)
                        {
                            Destroy(cellList.Find(x => cellList.IndexOf(x) == (rowId * 4) - i));
                        }
                        if (rowId == 1)
                        {
                            Destroy(transform.gameObject);
                        }
                    }
                    break;
                case "Left" : 
                    if (instanceID >= 10 && instanceID <= 14)
                    {
                        var columnId = instanceID - 10;
                        for (int i = 0; i <= 3; i++)
                        {
                            Destroy(cellList.Find(x => cellList.IndexOf(x) == (columnId - 1) + i * 4));
                        }
                        if (columnId == 1)
                        {
                            Destroy(transform.gameObject);
                        }
                    }
                    break;
                case "Right" : 
                    if (instanceID >= 10 && instanceID <= 14)
                    {
                        var columnId = instanceID - 10;
                        for (int i = 0; i <= 3; i++)
                        {
                            Destroy(cellList.Find(x => cellList.IndexOf(x) == columnId  + i * 4));
                        }
                        if (columnId == 3)
                        {
                            Destroy(transform.gameObject);
                        }
                    }
                    break;
            }
            pointList.Remove(pointList.Find(x => x.GetInstanceID() == instanceID));// добавить уничтожение поинтов
            Destroy(gameObject);//
        }
    }
}