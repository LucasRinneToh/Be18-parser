using System.Collections;
using System.Globalization;
using System.Text;
using Be18_parser.Models;

namespace Be18_parser.Serialization;

/// <summary>Writes a <see cref="Be18Document"/> in the same layout as files saved by Be.</summary>
internal static class Be18Writer
{
    private const string NewLine = "\r\n";

    public static string Write(Be18Document document)
    {
        var entities = CollectEntities(document);
        AssignRids(entities);

        var text = new StringBuilder();
        text.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>").Append(NewLine);
        foreach (var comment in document.FileComments)
            text.Append("<!--").Append(comment).Append("-->").Append(NewLine);

        text.Append("<BE05>").Append(NewLine);
        foreach (var entity in entities)
        {
            text.Append('\t');
            WriteEntity(text, entity);
            text.Append(NewLine);
        }
        text.Append("</BE05>");

        return text.ToString();
    }

    /// <summary>
    /// <see cref="Be18Document.Entities"/> in order, with the header first if it is missing, followed
    /// by every entity they reference that is not in the list yet.
    /// </summary>
    private static List<Be18Entity> CollectEntities(Be18Document document)
    {
        var seen = new HashSet<Be18Entity>(ReferenceEqualityComparer.Instance);
        var entities = new List<Be18Entity>();
        void Add(Be18Entity entity)
        {
            if (seen.Add(entity))
                entities.Add(entity);
        }

        if (!document.Entities.Contains(document.Header))
            Add(document.Header);
        foreach (var entity in document.Entities)
            Add(entity);

        for (var i = 0; i < entities.Count; i++)
        {
            if (entities[i] is UnknownEntity)
                continue;

            foreach (var field in Be18Schema.Get(entities[i].GetType()).Fields)
            {
                var value = field.Property.GetValue(entities[i]);
                if (value is Be18Entity referenced)
                    Add(referenced);
                else if (field.Kind == FieldKind.ReferenceList && value is IList list)
                    foreach (var item in list.OfType<Be18Entity>())
                        Add(item);
            }
        }

        return entities;
    }

    /// <summary>
    /// Gives every entity whose rid is not positive, or already used by an earlier entity in the
    /// list, a new rid from the counter.
    /// </summary>
    private static void AssignRids(List<Be18Entity> entities)
    {
        var used = new HashSet<int>();
        var needRid = new List<Be18Entity>();
        foreach (var entity in entities)
        {
            if (entity.Rid > 0 && used.Add(entity.Rid))
                continue;
            needRid.Add(entity);
        }

        if (needRid.Count == 0)
            return;

        if (used.Count > 0)
            Be18Entity.EnsureRidCounterAbove(used.Max());
        foreach (var entity in needRid)
            entity.Rid = Be18Entity.NextRid();
    }

    private static void WriteEntity(StringBuilder text, Be18Entity entity)
    {
        var elementName = entity is UnknownEntity unknown ? unknown.TypeName : Be18Schema.Get(entity.GetType()).ElementName;
        text.Append('<').Append(elementName).Append(" rid=\"#").Append(entity.Rid.ToString(CultureInfo.InvariantCulture)).Append("\">");

        if (entity is not UnknownEntity)
        {
            foreach (var field in Be18Schema.Get(entity.GetType()).Fields)
                WriteField(text, field.Name, FormatValue(entity, elementName, field));
        }

        foreach (var field in entity.ExtraFields)
            WriteField(text, field.Name, Escape(field.Value));

        text.Append("</").Append(elementName).Append('>');
    }

    private static void WriteField(StringBuilder text, string name, string value)
        => text.Append('<').Append(name).Append('>').Append(value).Append("</").Append(name).Append('>');

    private static string FormatValue(Be18Entity entity, string elementName, FieldSchema field)
    {
        var value = field.Property.GetValue(entity);
        switch (field.Kind)
        {
            case FieldKind.String:
                return Escape((string?)value ?? "");

            case FieldKind.Double:
                return value == null ? "" : FormatNumber((double)value, elementName, field);

            case FieldKind.Int:
                return value == null ? "" : ((int)value).ToString(CultureInfo.InvariantCulture);

            case FieldKind.Bool:
                return (bool)value! ? ".T." : ".F.";

            case FieldKind.Enum:
                var token = ((IBe18Enum)value!).Value;
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException(
                        $"{elementName} #{entity.Rid}: {field.Property.Name} (<{field.Name}>) has no value. Set it before saving.");
                return "." + token + ".";

            case FieldKind.Reference:
                return value is Be18Entity referenced ? FormatReference(referenced) : "$";

            case FieldKind.ReferenceList:
                var items = (IList?)value ?? Array.Empty<object>();
                return string.Join(" ", items.Cast<Be18Entity?>().Select(item => item == null ? "$" : FormatReference(item)));

            default:
                throw new InvalidOperationException($"Unhandled field kind {field.Kind}.");
        }
    }

    private static string FormatReference(Be18Entity entity)
        => "#" + entity.Rid.ToString(CultureInfo.InvariantCulture);

    /// <summary>Formats a number with <c>.</c> as decimal separator, no exponent and no trailing zeros.</summary>
    private static string FormatNumber(double value, string elementName, FieldSchema field)
    {
        if (!double.IsFinite(value))
            throw new InvalidOperationException($"{elementName}: {field.Property.Name} (<{field.Name}>) is {value}, which cannot be saved.");

        try
        {
            return ((decimal)value).ToString(CultureInfo.InvariantCulture);
        }
        catch (OverflowException)
        {
            return value.ToString("R", CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Escapes text for an element. Characters that ISO-8859-1 cannot represent are written as
    /// character references.
    /// </summary>
    private static string Escape(string value)
    {
        var text = new StringBuilder(value.Length);
        foreach (var rune in value.EnumerateRunes())
        {
            switch (rune.Value)
            {
                case '&': text.Append("&amp;"); break;
                case '<': text.Append("&lt;"); break;
                case '>': text.Append("&gt;"); break;
                case > 0xFF: text.Append("&#").Append(rune.Value.ToString(CultureInfo.InvariantCulture)).Append(';'); break;
                default: text.Append(rune.ToString()); break;
            }
        }
        return text.ToString();
    }
}
