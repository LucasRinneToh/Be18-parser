namespace Be18_parser.Models;

/// <summary>Circulation pump and pipes of the domestic hot water system (<c>DHW_PUMP_CIRC</c>).</summary>
[Be18Entity("DHW_PUMP_CIRC")]
public sealed class CirculationPump : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary><c>r_fac</c> (meaning not yet confirmed).</summary>
    [Be18Field("r_fac")]
    public double RFac { get; set; }

    /// <summary><c>n_cp</c> (meaning not yet confirmed).</summary>
    [Be18Field("n_cp")]
    public double NCp { get; set; }

    /// <summary>Power of the pump in W (<c>p_eff</c>).</summary>
    [Be18Field("p_eff")]
    public double PumpPower { get; set; }

    /// <summary><c>n_cp2</c> (meaning not yet confirmed).</summary>
    [Be18Field("n_cp2")]
    public double NCp2 { get; set; }

    /// <summary><c>p_eff2</c> (meaning not yet confirmed).</summary>
    [Be18Field("p_eff2")]
    public double PumpPower2 { get; set; }

    /// <summary>Whether the pipes use electric heat tracing (<c>el_tracing</c>).</summary>
    [Be18Field("el_tracing")]
    public bool HasElectricTracing { get; set; }

    /// <summary>Hot water distribution pipes (<c>has_tube</c>).</summary>
    [Be18Field("has_tube")]
    public List<HotWaterPipe> Pipes { get; set; } = [];
}
