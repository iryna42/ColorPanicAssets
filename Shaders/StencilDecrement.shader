Shader "Unlit/Stencil Decrement"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Geometry"}
        LOD 100

        ColorMask 0
        Zwrite Off

        Stencil
        {
            Ref 0
            Comp Always
            Pass DecrWrap
        }

        Pass { }
    }
}
