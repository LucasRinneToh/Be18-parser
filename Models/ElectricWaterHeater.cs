namespace Be18_parser.Models;

/// <summary>An electric water heater (<c>HEATER_EL</c>).</summary>
[Be18Entity("HEATER_EL")]
public sealed class ElectricWaterHeater : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Fraction of the hot water heated in a separate electric water heater ("Andel af VBV i separat elvandvarmer") (<c>dhw_frac</c>).</summary>
    [Be18Field("dhw_frac")]
    public double HotWaterFraction { get; set; }

    /// <summary>Heat loss from the hot water tank in W/K ("Varmetab fra varmtvandsbeholder") (<c>heat_loss</c>).</summary>
    [Be18Field("heat_loss")]
    public double HeatLoss { get; set; }

    /// <summary>b-factor for the room the heater is in ("b-faktor for opstillingsrum") (<c>b</c>).</summary>
    [Be18Field("b")]
    public double BFactor { get; set; }
}
