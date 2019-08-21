using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InterdimensionalTransport : MonoBehaviour
{
   public Material[] materials;

   void Start()
   {
       foreach (var mat in materials)
       {
           mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
       }
   }

   private void OnTriggerStay(Collider other)
   {
       if (other.name != "ARCoreCamera") return;



       if (transform.position.z > other.transform.position.z)
       {
           // Outside of other world
           Debug.Log("Outside of other world");
           foreach (var mat in materials)
           {
               mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
           }
       }
       else
       {
           // Inside other dimension
           Debug.Log("Inside Other World");
           foreach (var mat in materials)
           {
               mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
           }
       }
   }
   private void OnDestroy()
   {
       foreach (var mat in materials)
       {
           mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
       }
   }
   // Update is called once per frame
   void Update()
   {

   }
}
