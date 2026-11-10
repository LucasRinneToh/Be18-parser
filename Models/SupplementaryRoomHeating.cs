namespace Be18_parser.Models;

/// <summary>Supplementary room heating: direct electric heating, and wood stoves, gas heaters or similar (<c>SUP_ROOM_HEAT</c>).</summary>
[Be18Entity("SUP_ROOM_HEAT")]
public sealed class SupplementaryRoomHeating : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Fraction of the floor area heated by direct electric heating ("Direkte el til rumopvarmning, andel af etageareal") (<c>a_frac_heat</c>).</summary>
    [Be18Field("a_frac_heat")]
    public double DirectElectricHeatingFraction { get; set; }

    /// <summary>Probably the description of the stoves ("Brændeovne, gasstrålevarmere og lign., beskrivelse"); not yet confirmed (<c>doc</c>).</summary>
    [Be18Field("doc")]
    public string Doc { get; set; } = "";

    /// <summary>Fraction of the floor area heated by stoves or similar ("Brændeovne, andel af etageareal") (<c>a_frac</c>).</summary>
    [Be18Field("a_frac")]
    public double StoveFloorAreaFraction { get; set; }

    /// <summary>Efficiency of the stoves ("Virkningsgrad") (<c>ny</c>).</summary>
    [Be18Field("ny")]
    public double StoveEfficiency { get; set; }

    /// <summary>Air flow needed by the stoves in m³/s ("Luftstrømsbehov") (<c>air_flow</c>).</summary>
    [Be18Field("air_flow")]
    public double StoveAirFlow { get; set; }
}
