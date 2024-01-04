#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

struct VertexShaderOutput
{
    float2 coords : TEXCOORD0;
    float4 color : COLOR0;
};

sampler2D TextureSampler;

float4 CircleFunction(VertexShaderOutput output) : COLOR 
{
    float4 textureColor = tex2D(TextureSampler, output.coords);
    float x = output.coords.x;
    float y = output.coords.y;
    float px = cos(0.523599f) * (x-0.5f) - sin(0.523599f) * (y-0.5f) + 0.5;
    float py = sin(0.523599f) * (x-0.5f) + cos(0.523599f) * (y-0.5f) + 0.5;
    float dx = px - 0.5f;
    float dy = py - 0.5f;

    if(dx * dx + dy * dy <= 0.25f)
        return textureColor;
    else
        return float4(0.0f, 0.0f, 0.0f, 0.0f);
}

technique CircleTechnique
{
    pass Pass
    {
        PixelShader = compile PS_SHADERMODEL CircleFunction(); 
    }
}