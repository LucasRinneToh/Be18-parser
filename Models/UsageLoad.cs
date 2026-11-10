namespace Be18_parser.Models;

/// <summary>Internal heat gains in one zone of the building (<c>USAGE_LOAD</c>).</summary>
[Be18Entity("USAGE_LOAD")]
public sealed class UsageLoad : Be18Entity
{
    /// <summary>SfB classification code (<c>sfb</c>).</summary>
    [Be18Field("sfb")]
    public string Sfb { get; set; } = "";

    /// <summary>Floor area of the zone in m² (<c>area</c>).</summary>
    [Be18Field("area")]
    public double Area { get; set; }

    /// <summary>Heat gain from persons in W/m² (<c>ppers</c>).</summary>
    [Be18Field("ppers")]
    public double PersonsHeatGain { get; set; }

    /// <summary>Heat gain from appliances in W/m² (<c>papp</c>).</summary>
    [Be18Field("papp")]
    public double AppliancesHeatGain { get; set; }

    /// <summary>Heat gain from appliances at night in W/m² (<c>papp_night</c>).</summary>
    [Be18Field("papp_night")]
    public double AppliancesHeatGainNight { get; set; }

    /// <summary>Internal distribution used by some calculations; it cannot be set in the Be18 user interface (<c>part</c>).</summary>
    [Be18Field("part")]
    public double Part { get; set; }
}
