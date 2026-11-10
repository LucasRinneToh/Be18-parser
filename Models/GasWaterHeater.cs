namespace Be18_parser.Models;

/// <summary>A gas water heater (<c>HEATER_GAS</c>).</summary>
[Be18Entity("HEATER_GAS")]
public sealed class GasWaterHeater : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Fraction of the hot water heated in a separate gas water heater ("Andel af VBV i separat gasvandvarmer") (<c>dhw_frac</c>).</summary>
    [Be18Field("dhw_frac")]
    public double HotWaterFraction { get; set; }

    /// <summary>Heat loss from the hot water tank in W/K ("Varmetab fra varmtvandsbeholder") (<c>heat_loss</c>).</summary>
    [Be18Field("heat_loss")]
    public double HeatLoss { get; set; }

    /// <summary>b-factor for the room the heater is in ("b-faktor for opstillingsrum") (<c>b</c>).</summary>
    [Be18Field("b")]
    public double BFactor { get; set; }

    /// <summary>Efficiency ("Virkningsgrad") (<c>ny</c>).</summary>
    [Be18Field("ny")]
    public double Efficiency { get; set; }

    /// <summary>Pilot flame power in W ("Pilotflamme") (<c>pilot_flame</c>).</summary>
    [Be18Field("pilot_flame")]
    public double PilotFlame { get; set; }
}
