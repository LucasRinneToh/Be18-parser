namespace Be18_parser.Models;

/// <summary>A design temperature of the heat distribution system (<c>DIM_TEMP</c>). <see cref="Be18Entity.Name"/> holds a free text description.</summary>
[Be18Entity("DIM_TEMP")]
public sealed class DesignTemperature : Be18Entity
{
    /// <summary>Design temperature in °C ("Dimensionerende") (<c>temp</c>).</summary>
    [Be18Field("temp")]
    public double Temperature { get; set; }
}
