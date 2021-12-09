Shader "Unlit/Stencil Increment"
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
        Blend SrcAlpha OneMinusSrcAlpha

        Stencil
        {
            Ref 2
            Comp NotEqual
            Pass IncrSat
            Fail Zero
        }

        Pass { }
    }
}
