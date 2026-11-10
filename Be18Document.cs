using System.Text;
using System.Xml.Linq;
using Be18_parser.Models;
using Be18_parser.Serialization;

namespace Be18_parser;

/// <summary>
/// A Be18 project file (<c>.bexml</c> or <c>.xml</c>; both have the same content). Load one with
/// <see cref="Load(string)"/>, or create an empty one with <c>new Be18Document()</c>, and save it
/// with <see cref="Save(string)"/>.
/// </summary>
/// <remarks>
/// <para>
/// Work with the project through the object graph starting at <see cref="Building"/>. Entities
/// added to the graph are saved automatically; they do not need to be added to
/// <see cref="Entities"/>.
/// </para>
/// <para>
/// A file that is loaded and saved without changes is written back byte for byte, as long as it
/// uses the layout Be itself writes.
/// </para>
/// </remarks>
public sealed class Be18Document
{
    /// <summary>The encoding of Be18 files: ISO-8859-1 (Latin-1).</summary>
    public static Encoding TextEncoding { get; } = Encoding.GetEncoding(28591);

    /// <summary>The header entity (<c>BE05</c>), which links to the building.</summary>
    public ModelHeader Header { get; set; } = new();

    /// <summary>The building described by the file. Shortcut for <c>Header.Building</c>.</summary>
    public Building? Building
    {
        get => Header.Building;
        set => Header.Building = value;
    }

    /// <summary>
    /// All entities in file order, including entities nothing refers to. When saving, these are
    /// written first, followed by any entity reachable from them that is not in the list.
    /// </summary>
    public List<Be18Entity> Entities { get; } = [];

    /// <summary>
    /// Text of the XML comments before the root element, such as Be's <c>FILE_DESCRIPTION</c> and
    /// <c>FILE_NAME</c> lines, without the <c>&lt;!--</c> and <c>--&gt;</c>.
    /// </summary>
    public List<string> FileComments { get; } = [];

    /// <summary>Loads a <c>.bexml</c> or <c>.xml</c> file.</summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>The loaded document.</returns>
    /// <exception cref="Be18FormatException">The file is not a valid Be18 file.</exception>
    public static Be18Document Load(string path)
    {
        using var stream = File.OpenRead(path);
        return Load(stream);
    }

    /// <summary>Loads a Be18 file from a stream. The encoding is taken from the XML declaration.</summary>
    /// <param name="stream">The file content.</param>
    /// <returns>The loaded document.</returns>
    /// <exception cref="Be18FormatException">The content is not a valid Be18 file.</exception>
    public static Be18Document Load(Stream stream) => Be18Reader.Read(stream);

    /// <summary>Reads a Be18 file from a string, such as the result of <see cref="ToXml"/>.</summary>
    /// <param name="xml">The XML text.</param>
    /// <returns>The loaded document.</returns>
    /// <exception cref="Be18FormatException">The text is not a valid Be18 file.</exception>
    public static Be18Document Parse(string xml)
    {
        XDocument document;
        try
        {
            document = XDocument.Parse(xml);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new Be18FormatException($"The text is not valid XML: {ex.Message}", ex);
        }

        return Be18Reader.Read(document);
    }

    /// <summary>
    /// Saves the document as a <c>.bexml</c> or <c>.xml</c> file in ISO-8859-1. An entity whose
    /// <see cref="Be18Entity.Rid"/> is not positive, or is used by another entity in the file, is
    /// given a new one.
    /// </summary>
    /// <param name="path">Path to the file. An existing file is overwritten.</param>
    /// <exception cref="InvalidOperationException">A value cannot be saved, such as an enumeration that has not been set.</exception>
    public void Save(string path) => File.WriteAllBytes(path, TextEncoding.GetBytes(ToXml()));

    /// <summary>Writes the document to a stream in ISO-8859-1.</summary>
    /// <param name="stream">The stream to write to.</param>
    /// <exception cref="InvalidOperationException">A value cannot be saved, such as an enumeration that has not been set.</exception>
    public void Save(Stream stream)
    {
        var bytes = TextEncoding.GetBytes(ToXml());
        stream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// Returns the document as XML text, as it would be saved. This is also the input
    /// <see cref="Engine.BeEngine"/> expects.
    /// </summary>
    /// <returns>The XML text.</returns>
    /// <exception cref="InvalidOperationException">A value cannot be saved, such as an enumeration that has not been set.</exception>
    public string ToXml() => Be18Writer.Write(this);
}
