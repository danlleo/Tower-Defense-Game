using System;

namespace Services.Grid
{
    [Serializable]
    public record GridSystemSettings(uint width, uint length, float cellScale);
}

