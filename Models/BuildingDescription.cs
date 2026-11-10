namespace Be18_parser.Models;

/// <summary>Owner and address details of the building (<c>DESCRIPT</c>).</summary>
[Be18Entity("DESCRIPT")]
public sealed class BuildingDescription : Be18Entity
{
    /// <summary>BBR number of the building (<c>bbr</c>).</summary>
    [Be18Field("bbr")]
    public string Bbr { get; set; } = "";

    /// <summary>Owner of the building (<c>owner</c>).</summary>
    [Be18Field("owner")]
    public string Owner { get; set; } = "";

    /// <summary>Address, first line (<c>addr1</c>).</summary>
    [Be18Field("addr1")]
    public string Address1 { get; set; } = "";

    /// <summary>Address, second line (<c>addr2</c>).</summary>
    [Be18Field("addr2")]
    public string Address2 { get; set; } = "";

    /// <summary>Address, third line (<c>addr3</c>).</summary>
    [Be18Field("addr3")]
    public string Address3 { get; set; } = "";
}
