namespace Be18_parser.Models;

/// <summary>A linear thermal bridge (line loss), such as a foundation or a window joint (<c>COLD_BRIDGE</c>). <see cref="BuildingElement.Area"/> holds the length in m and <see cref="BuildingElement.U"/> the linear loss in W/mK.</summary>
[Be18Entity("COLD_BRIDGE")]
public sealed class ThermalBridge : BuildingElement
{
}
