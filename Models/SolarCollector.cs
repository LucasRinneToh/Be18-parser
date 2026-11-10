namespace Be18_parser.Models;

/// <summary>A solar heating system (<c>SOLAR_COLLECTOR</c>).</summary>
[Be18Entity("SOLAR_COLLECTOR")]
public sealed class SolarCollector : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Total collector area in m² ("Samlet solfangerareal") (<c>area</c>).</summary>
    [Be18Field("area")]
    public double Area { get; set; }

    /// <summary>Orientation as Danish compass letters, e.g. <c>s</c> (<c>orient</c>).</summary>
    [Be18Field("orient")]
    public string Orientation { get; set; } = "";

    /// <summary>Slope in degrees from horizontal ("Hældning") (<c>slope</c>).</summary>
    [Be18Field("slope")]
    public double Slope { get; set; }

    /// <summary>Horizon angle in degrees ("Horisont afskæring") (<c>horizon</c>).</summary>
    [Be18Field("horizon")]
    public double Horizon { get; set; }

    /// <summary>First order heat loss coefficient in W/m²K ("1. ordens varmetabskoefficient") (<c>heat_loss_coef</c>).</summary>
    [Be18Field("heat_loss_coef")]
    public double HeatLossCoefficient1 { get; set; }

    /// <summary>Total pipe length of the collector circuit in m ("Samlet rørlængde") (<c>tube_length</c>).</summary>
    [Be18Field("tube_length")]
    public double PipeLength { get; set; }

    /// <summary>Linear heat loss of the pipes in W/mK ("Varmetab, rør") (<c>heat_loss</c>).</summary>
    [Be18Field("heat_loss")]
    public double PipeLinearLoss { get; set; }

    /// <summary>Efficiency factor of the heat exchanger ("Varmevekslers effektivitetsfaktor") (<c>ny_loop</c>).</summary>
    [Be18Field("ny_loop")]
    public double HeatExchangerEfficiencyFactor { get; set; }

    /// <summary>Start efficiency of the collector ("Start effektivitet") (<c>ny0</c>).</summary>
    [Be18Field("ny0")]
    public double StartEfficiency { get; set; }

    /// <summary>Power of the pump in W ("Pumpeeffekt") (<c>pump</c>).</summary>
    [Be18Field("pump")]
    public double PumpPower { get; set; }

    /// <summary>Standby power of the automatics in W ("Automatik, standby effekt") (<c>stand_by</c>).</summary>
    [Be18Field("stand_by")]
    public double AutomaticsStandbyPower { get; set; }

    /// <summary>Left side shading angle in degrees ("Skygge venstre") (<c>sidefin_left</c>).</summary>
    [Be18Field("sidefin_left")]
    public double SideFinLeft { get; set; }

    /// <summary>Right side shading angle in degrees ("Skygge højre") (<c>sidefin_right</c>).</summary>
    [Be18Field("sidefin_right")]
    public double SideFinRight { get; set; }

    /// <summary>Second order heat loss coefficient in W/m²K² ("2. ordens varmetabskoefficient") (<c>heat_loss_coef2</c>).</summary>
    [Be18Field("heat_loss_coef2")]
    public double HeatLossCoefficient2 { get; set; }

    /// <summary>Incidence angle dependency ("Vinkelafhængighed") (<c>ang_fac</c>).</summary>
    [Be18Field("ang_fac")]
    public double IncidenceAngleFactor { get; set; }

    /// <summary>What the system supplies ("Type") (<c>has_type</c>).</summary>
    [Be18Field("has_type")]
    public SolarCollectorType Type { get; set; }
}
