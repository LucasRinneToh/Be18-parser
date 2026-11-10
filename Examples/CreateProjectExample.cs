using Be18_parser.Models;

namespace Be18_parser.Examples;

/// <summary>Creates a new project from the models and saves it as a Be18 file.</summary>
/// <remarks>
/// The project mirrors the structure of the files Be writes: a building with envelope,
/// ventilation, internal gains, heat supply, heat distribution and hot water. The values are
/// typical for a small detached house. The project has not been run through the Be engine yet,
/// so Be may require more data than this.
/// </remarks>
public static class CreateProjectExample
{
    public static void Run(string outputPath)
    {
        // Every new entity takes the next rid from a shared counter. Reset it so this project is
        // numbered from #1, and create the document first so its header entity gets #1, as in
        // files written by Be.
        Be18Entity.ResetRidCounter();
        var document = new Be18Document();
        document.Header.Name = "Be18-parser example";

        var building = new Building
        {
            Name = "Example: detached house 150 m²",
            BuildingType = BuildingType.DetachedHouse,
            BasicHeatSupply = HeatSupply.DistrictHeating,
            CalculationCondition = CalculationCondition.Standard,
            HeatedFloorArea = 150,
            DwellingCount = 1,

            // Temperatures and usage, using the default values of Be.
            HeatingSetpoint = 20,
            VentingSetpoint = 23,
            NightVentilationSetpoint = 24,
            MechanicalCoolingSetpoint = 25,
            DesignRoomTemperature = 20,
            DesignOutdoorTemperature = -12,
            WarehouseHeatingSetpoint = 15,
            WarehouseDesignRoomTemperature = 15,
            HeatCapacity = 120,
            UsageTime = 168,
            StartTime = 0,
            EndTime = 24,
            HasHeatDistributionSystem = true,
        };

        AddEnvelope(building);
        AddVentilationAndGains(building);
        AddHeatSupply(building);

        // A document only needs the building; everything reachable from it is saved.
        document.Building = building;
        document.Save(outputPath);
        Console.WriteLine($"Saved {outputPath} with {Be18Document.Load(outputPath).Entities.Count} entities.");
    }

    private static void AddEnvelope(Building building)
    {
        // One shading entity can be shared by several windows.
        var shading = new Shading { Name = "Average shading", Horizon = 15, Overhang = 15, WindowReveal = 10 };

        building.TransparentConstructions.Add(Window("Windows towards south", "s", 4, 2.0, shading));
        building.TransparentConstructions.Add(Window("Windows towards east", "ø", 2, 1.5, shading));
        building.TransparentConstructions.Add(Window("Windows towards west", "v", 2, 1.5, shading));
        building.TransparentConstructions.Add(Window("Windows towards north", "n", 3, 1.2, shading));

        building.OpaqueConstructions.Add(new OpaqueConstruction { Name = "External walls", Area = 110, U = 0.15, BFactor = 1 });
        building.OpaqueConstructions.Add(new OpaqueConstruction { Name = "Roof", Area = 150, U = 0.09, BFactor = 1 });

        // Floor on ground: the design outside temperature is the ground temperature, and floor
        // heating raises the design inside temperature.
        building.OpaqueConstructions.Add(new OpaqueConstruction
        {
            Name = "Floor on ground with floor heating",
            Area = 150,
            U = 0.08,
            BFactor = 0.7,
            DesignInsideTemperature = 30,
            DesignOutsideTemperature = 10,
        });

        // For thermal bridges, Area is the length in m and U the linear loss in W/mK.
        building.ThermalBridges.Add(new ThermalBridge { Name = "Foundation", Area = 52, U = 0.12, BFactor = 0.7 });
        building.ThermalBridges.Add(new ThermalBridge { Name = "Joints around windows", Area = 60, U = 0.03, BFactor = 1 });
    }

    private static TransparentConstruction Window(string name, string orientation, int count, double area, Shading shading) => new()
    {
        Name = name,
        Count = count,
        Area = area,
        U = 0.8,
        BFactor = 1,
        Orientation = orientation,
        Slope = 90,
        GlazingFraction = 0.75,
        GValue = 0.5,
        ShadingFactor = 1,
        IncludeInSummerComfort = orientation != "n",
        Shading = shading,
    };

    private static void AddVentilationAndGains(Building building)
    {
        building.VentilationZones.Add(new VentilationZone
        {
            Name = "Whole house",
            Area = 150,
            UsageFactor = 1,
            MechanicalVentilation = 0.3,
            HeatRecoveryEfficiency = 0.85,
            InletTemperature = 18,
            NaturalVentilation = 0,
            Sel = 1.0,
            MechanicalVentilationSummer = 0.3,
            NaturalVentilationSummer = 0.9,
            MechanicalVentilationNight = 0,
            NaturalVentilationNight = 0,
        });

        var usage = new Usage();
        usage.Loads.Add(new UsageLoad { Name = "Whole house", Area = 150, PersonsHeatGain = 1.5, AppliancesHeatGain = 3.5 });
        building.Usage = usage;

        building.SummerComfortRoom = new SummerComfortRoom
        {
            Name = "Living room",
            Area = 40,
            VentilationWinter = 0.3,
            VentilationSummerDay = 0.9,
            VentilationSummerNight = 0.6,
        };
    }

    private static void AddHeatSupply(Building building)
    {
        building.HeatingSystems = new HeatingSystems
        {
            DistrictHeatingExchanger = new DistrictHeatingExchanger
            {
                Name = "District heating exchanger",
                NominalPower = 32,
                HeatLoss = 1,
                AutomaticsStandbyPower = 5,
            },
        };

        building.HeatDistribution = new HeatDistribution
        {
            Name = "Two-pipe system",
            PipeSystemType = 2,
            InletTemperature = new DesignTemperature { Temperature = 55 },
            ReturnTemperature = new DesignTemperature { Temperature = 35 },
            TimeControlledHeatingSeasonPump = new DistributionPump { Name = "Demand-controlled pump", NominalPower = 25, Fp = 0.4, Count = 1 },
            ConstantYearRoundPump = new DistributionPump(),
            ConstantHeatingSeasonPump = new DistributionPump(),
            CombiPump = new DistributionPump(),
        };

        building.HotWater = new DomesticHotWater
        {
            Name = "Domestic hot water",
            Consumption = 250,
            Temperature = 55,
            Tank = new HotWaterTank
            {
                Name = "Hot water tank",
                Count = 1,
                Volume = 150,
                CentralHeatingFlowTemperature = 60,
                HeatLoss = 1.2,
                HotWaterFraction = 1,
                ChargingPower = 6,
                ElectricHeating = TankElectricHeating.None,
                CirculationPump = new CirculationPump { Name = "Circulation pump" },
            },
        };
    }
}
