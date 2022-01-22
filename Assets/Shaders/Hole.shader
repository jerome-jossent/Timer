Shader "Custom/Hole"
{//https://www.reddit.com/r/Unity3D/comments/g14m68/how_to_make_in_5_steps_realistic_looking_holes/
	SubShader
	{
		Tags { "Queue" = "Geometry-1" }
		Lighting Off
		Pass
		{
			ZWrite On
			ZTest LEqual
			ColorMask 0
		}
	}
}
