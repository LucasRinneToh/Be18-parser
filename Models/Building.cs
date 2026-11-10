namespace Be18_parser.Models;

/// <summary>The building and its main settings (<c>BUILDING</c>). Most other entities are reached from here.</summary>
[Be18Entity("BUILDING")]
public sealed class Building : Be18Entity
{
    /// <summary>Heated floor area in m² (<c>ae</c>).</summary>
    [Be18Field("ae")]
    public double HeatedFloorArea { get; set; }

    /// <summary>Setpoint for room heating in °C ("Rumopvarmning") (<c>th_heat</c>).</summary>
    [Be18Field("th_heat")]
    public double HeatingSetpoint { get; set; }

    /// <summary>Setpoint for venting (opening windows) in °C ("Udluftning") (<c>th_setp</c>).</summary>
    [Be18Field("th_setp")]
    public double VentingSetpoint { get; set; }

    /// <summary>Setpoint for night ventilation in °C ("Natteventilation") (<c>th_nv</c>).</summary>
    [Be18Field("th_nv")]
    public double NightVentilationSetpoint { get; set; }

    /// <summary>Setpoint for mechanical cooling in °C ("Mekanisk køling") (<c>th_cool</c>).</summary>
    [Be18Field("th_cool")]
    public double MechanicalCoolingSetpoint { get; set; }

    /// <summary>Heat capacity of the building in Wh/K per m² floor area ("Varmekapacitet") (<c>heat_cap</c>).</summary>
    [Be18Field("heat_cap")]
    public double HeatCapacity { get; set; }

    /// <summary>Design room temperature in °C ("Indetemperatur") (<c>dim_room_temp</c>).</summary>
    [Be18Field("dim_room_temp")]
    public double DesignRoomTemperature { get; set; }

    /// <summary>Normal usage time in hours per week ("Normal brugstid") (<c>tb</c>).</summary>
    [Be18Field("tb")]
    public double UsageTime { get; set; }

    /// <summary>Start of the usage period, hour of the day ("Start") (<c>start_time</c>).</summary>
    [Be18Field("start_time")]
    public double StartTime { get; set; }

    /// <summary>End of the usage period, hour of the day ("Slut") (<c>end_time</c>).</summary>
    [Be18Field("end_time")]
    public double EndTime { get; set; }

    /// <summary>Design outdoor temperature in °C ("Udetemperatur") (<c>tu</c>).</summary>
    [Be18Field("tu")]
    public double DesignOutdoorTemperature { get; set; }

    /// <summary>Whether the building has mechanical cooling (<c>mech_cooling</c>).</summary>
    [Be18Field("mech_cooling")]
    public bool HasMechanicalCooling { get; set; }

    /// <summary>Whether the building has direct electric room heating ("El-radiator") (<c>sup_heating</c>).</summary>
    [Be18Field("sup_heating")]
    public bool HasDirectElectricHeating { get; set; }

    /// <summary>Whether the building has a solar heating system ("Solvarme") (<c>solarpanel</c>).</summary>
    [Be18Field("solarpanel")]
    public bool HasSolarCollector { get; set; }

    /// <summary>Whether the building has a heat pump ("Varmepumpe") (<c>heatpump</c>).</summary>
    [Be18Field("heatpump")]
    public bool HasHeatPump { get; set; }

    /// <summary>Whether the building has solar cells ("Solceller") (<c>pv_panel</c>).</summary>
    [Be18Field("pv_panel")]
    public bool HasPhotovoltaics { get; set; }

    /// <summary>Rotation of the building in degrees ("Rotation") (<c>rotation</c>).</summary>
    [Be18Field("rotation")]
    public double Rotation { get; set; }

    /// <summary>Supplement to the energy frame for special conditions in kWh/m² per year ("Tillæg til energirammen for særlige betingelser") (<c>add_eframe</c>).</summary>
    [Be18Field("add_eframe")]
    public double EnergyFrameSupplement { get; set; }

    /// <summary>Whether the building has wood stoves, gas heaters or similar ("Brændeovne, gasvarmere etc.") (<c>sup_oven</c>).</summary>
    [Be18Field("sup_oven")]
    public bool HasStoves { get; set; }

    /// <summary>Whether the building has a heat distribution system ("Varmefordelingsanlæg") (<c>heat_dist</c>).</summary>
    [Be18Field("heat_dist")]
    public bool HasHeatDistributionSystem { get; set; }

    /// <summary>Number of dwelling units ("Antal boligenheder") (<c>dwellings</c>).</summary>
    [Be18Field("dwellings")]
    public int DwellingCount { get; set; }

    /// <summary>Whether the building has a wind turbine ("Vindmøller") (<c>windmill</c>).</summary>
    [Be18Field("windmill")]
    public bool HasWindTurbine { get; set; }

    /// <summary>Setpoint for room heating in °C when the building is a warehouse (<c>th_store</c>).</summary>
    [Be18Field("th_store")]
    public double WarehouseHeatingSetpoint { get; set; }

    /// <summary>Design room temperature in °C when the building is a warehouse (<c>dim_store_temp</c>).</summary>
    [Be18Field("dim_store_temp")]
    public double WarehouseDesignRoomTemperature { get; set; }

    /// <summary>Fraction of the floor area with mechanical cooling ("Mekanisk køling, andel af etageareal") (<c>cooling_frac</c>).</summary>
    [Be18Field("cooling_frac")]
    public double MechanicalCoolingFraction { get; set; }

    /// <summary><c>ae2</c> (meaning not yet confirmed).</summary>
    [Be18Field("ae2")]
    public double Ae2 { get; set; }

    /// <summary>Heated basement area in m², heated to 15 °C or more ("Kælder: opvarmet fra og med 15 grader") (<c>ae3</c>).</summary>
    [Be18Field("ae3")]
    public double HeatedBasementArea { get; set; }

    /// <summary>Building type ("Bygningstype") (<c>has_btype</c>).</summary>
    [Be18Field("has_btype")]
    public BuildingType BuildingType { get; set; }

    /// <summary>Basic heat supply ("Forsyning") (<c>basic_heat_supply</c>).</summary>
    [Be18Field("basic_heat_supply")]
    public HeatSupply BasicHeatSupply { get; set; }

    /// <summary>Calculation condition (<c>calc_condition</c>).</summary>
    [Be18Field("calc_condition")]
    public CalculationCondition CalculationCondition { get; set; }

    /// <summary>Windows and doors (<c>has_transp_const</c>).</summary>
    [Be18Field("has_transp_const")]
    public List<TransparentConstruction> TransparentConstructions { get; set; } = [];

    /// <summary>Walls, roofs, floors and other opaque constructions (<c>has_opaque_const</c>).</summary>
    [Be18Field("has_opaque_const")]
    public List<OpaqueConstruction> OpaqueConstructions { get; set; } = [];

    /// <summary>Unheated spaces; no example file has any yet (<c>has_unheated_space</c>).</summary>
    [Be18Field("has_unheated_space")]
    public List<Be18Entity> UnheatedSpaces { get; set; } = [];

    /// <summary>Ventilation zones (<c>has_ventilation</c>).</summary>
    [Be18Field("has_ventilation")]
    public List<VentilationZone> VentilationZones { get; set; } = [];

    /// <summary>Internal heat gains and special equipment (<c>has_usage</c>).</summary>
    [Be18Field("has_usage")]
    public Usage? Usage { get; set; }

    /// <summary>Lighting; only used for non-residential buildings in the examples (<c>has_lighting</c>).</summary>
    [Be18Field("has_lighting")]
    public Lighting? Lighting { get; set; }

    /// <summary>Linear thermal bridges (line losses) (<c>has_cold_bridge</c>).</summary>
    [Be18Field("has_cold_bridge")]
    public List<ThermalBridge> ThermalBridges { get; set; } = [];

    /// <summary>Heat producing systems (<c>has_heating_systems</c>).</summary>
    [Be18Field("has_heating_systems")]
    public HeatingSystems? HeatingSystems { get; set; }

    /// <summary>Mechanical cooling (<c>has_cooling</c>).</summary>
    [Be18Field("has_cooling")]
    public Cooling? Cooling { get; set; }

    /// <summary>Heat distribution system (<c>has_heat_distribution</c>).</summary>
    [Be18Field("has_heat_distribution")]
    public HeatDistribution? HeatDistribution { get; set; }

    /// <summary>Domestic hot water system (<c>has_hot_water</c>).</summary>
    [Be18Field("has_hot_water")]
    public DomesticHotWater? HotWater { get; set; }

    /// <summary>Gas water heater (<c>has_gas_heater</c>).</summary>
    [Be18Field("has_gas_heater")]
    public GasWaterHeater? GasWaterHeater { get; set; }

    /// <summary>Electric water heater (<c>has_el_heater</c>).</summary>
    [Be18Field("has_el_heater")]
    public ElectricWaterHeater? ElectricWaterHeater { get; set; }

    /// <summary>Owner and address details (<c>has_descript</c>).</summary>
    [Be18Field("has_descript")]
    public BuildingDescription? Description { get; set; }

    /// <summary>Comments; no example file sets them yet (<c>has_comments</c>).</summary>
    [Be18Field("has_comments")]
    public Be18Entity? Comments { get; set; }

    /// <summary>Room used for the summer comfort (overheating) check (<c>has_heated_room</c>).</summary>
    [Be18Field("has_heated_room")]
    public SummerComfortRoom? SummerComfortRoom { get; set; }
}
