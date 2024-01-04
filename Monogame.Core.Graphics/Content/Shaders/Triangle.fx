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

float ax;
float ay;
float bx;
float by;
float cx;
float cy;
sampler2D TextureSampler;

float Sign(float px, float py, float v1x, float v1y, float v2x, float v2y)
{
    return (px - v2x) * (v1y - v2y) - (v1x - v2x) * (py - v2y);
}

float4 TriangleFunction(VertexShaderOutput output) : COLOR
{
    float4 textureColor = tex2D(TextureSampler, output.coords);
    float x = output.coords.x;
    float y = output.coords.y;

    float d1 = Sign(x, y, ax, ay, bx, by);
    float d2 = Sign(x, y, bx, by, cx, cy);
    float d3 = Sign(x, y, cx, cy, ax, ay);

    bool has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
    bool has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

    if(has_neg && has_pos)
        return float4(0.0f, 0.0f, 0.0f, 0.0f);
    else
        return textureColor;
}

technique TriangleTechnique
{
    pass Pass
    {
        PixelShader = compile PS_SHADERMODEL TriangleFunction(); 
    }
}