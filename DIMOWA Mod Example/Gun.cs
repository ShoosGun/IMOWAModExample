using System;
using System.Collections.Generic;
using System.Text;
using CAMOWA;
using UnityEngine;

namespace DIMOWA_Mod_Example
{
    public class Gun
    {
        [IMOWAModInnit("Gun", 1/*Solar System*/, 2)]
        public static void ModInnit(string startingPoint)
        {
            //1 - Import .obj (the gun mesh)
            ObjImporter importer = new ObjImporter();
            Mesh gunMesh = importer.ImportFile(@"Gun\gun.obj");

            //2 - Find the stick
            // 1 - stick(StickAndTherm/polySurface3)  ;  2 - small stick(StickAndTherm/polySurface2)  ; 3 - even smaller stick(StickAndTherm/polySurface4)
            GameObject.Find("StickAndTherm/polySurface2").GetComponent<MeshRenderer>().enabled = false; // (Small) Stick goes puff
            GameObject.Find("StickAndTherm/polySurface4").GetComponent<MeshRenderer>().enabled = false; // (Small) Stick goes puff
            //3 - Change the stick to a gun
            GameObject.Find("StickAndTherm/polySurface3").GetComponent<MeshFilter>().mesh = gunMesh;

            GameObject.Find("RocketScientist/FrontierVillager_default_idle").AddComponent<FunnyAnimation>();
        }

    }
}
