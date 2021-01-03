﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathh : MonoBehaviour
{
    public Color lineColor;
    public List<Transform> nodes = new List<Transform>();
    
     void OnDrawGizmosSelected()
     {
        Gizmos.color = lineColor;

        Transform[] pathTransform = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        Debug.Log(nodes);

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if(pathTransform[i] != transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 previousNode=Vector3.zero;

            if (i > 0)
            {
              previousNode = nodes[i - 1].position;
            }
            else if (i== 0 && nodes.Count > 1)
            {
                previousNode = nodes[nodes.Count - 1].position;
            }


            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.5f);
        }

        
    }

}
