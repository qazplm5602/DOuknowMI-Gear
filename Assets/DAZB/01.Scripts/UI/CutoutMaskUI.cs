using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

// 아래 영상 참고
// https://www.youtube.com/watch?v=XJJl19N2KFM

public class CutoutMaskUI : Image
{
    public override Material materialForRendering {
        get {
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
}
