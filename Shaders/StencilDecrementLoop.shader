Shader "Unlit/Stencil Decrement Loop"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Geometry+1"}
        LOD 100

        ColorMask 0
        Zwrite Off

        Stencil
        {
            Ref 2
            Comp LEqual
            Pass Replace
        }

        Pass { }
    }
}
