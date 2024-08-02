void Loop_Tiles_float(float2 UV, float2 Tiling, float3 GridObjectSize, float2 CurrentTilePosition, out float4 Out)
{
    float4 result = float4(0, 0, 0, 0); // Initialize Out to zero
    float3 yellowColor = float3(1, 1, 0);
    
    for (int x = 0; x < GridObjectSize.x; ++x)
    for (int z = 0; z < GridObjectSize.z; ++z)
    {
        float2 iterationTilePosition = float2(CurrentTilePosition.x + x, CurrentTilePosition.y + z);
        float2 offset = iterationTilePosition * Tiling;
        float2 offsetOneMinus = 1 - offset;
        float2 ceiledUV = ceil(UV);
        float2 tilingAndOffset = ceiledUV * Tiling + offsetOneMinus;
        float range = distance(yellowColor, float3(tilingAndOffset, 0.0));
        float maskValue = saturate(1 - (range - 0) / max(0, 1e-5));
        result += maskValue;
    }

    Out = result;
}