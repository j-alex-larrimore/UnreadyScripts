using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClassData  {

    //class level, skill 1, skill 2, skill 3, talent 1, talent 2
    public int[] v = new int[6];
    
   public ClassData()
    {
        v[0] = 0;
        v[1] = 1;
        v[2] = 0;
        v[3] = 0;
        v[4] = 0;
        v[5] = 0;
    }

    public ClassData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ClassData>(jsonString);
    }

}
