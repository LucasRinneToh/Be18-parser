namespace Be18_parser.Models;

/// <summary>
/// Base class for all entities in a Be18 file. Each entity is one child element of the root
/// <c>&lt;BE05&gt;</c> element. It is identified by its <c>rid</c> attribute and referenced from
/// other entities as <c>#rid</c>.
/// </summary>
/// <remarks>
/// A new entity gets the next rid from a counter shared by all entities in the process, so rids
/// are unique and sequential in creation order. Loading a file moves the counter past the highest
/// rid in the file, so entities created afterwards never reuse a rid from a loaded file.
/// </remarks>
public abstract class Be18Entity
{
    private static int _lastRid;

    [ThreadStatic]
    private static bool t_suppressRids;

    /// <summary>Creates the entity and gives it the next rid from the counter.</summary>
    protected Be18Entity()
    {
        if (!t_suppressRids)
            Rid = NextRid();
    }

    /// <summary>
    /// The entity's record id: the number in <c>rid="#12"</c>. Must be unique within a file.
    /// Saving a file gives a new rid to any entity whose rid is not positive or is already used by
    /// another entity in the file.
    /// </summary>
    public int Rid { get; set; }

    /// <summary>Name or description of the entity (<c>id</c>).</summary>
    [Be18Field("id")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Fields found in the file that this model does not know about, in file order. They are
    /// kept so they can be written back unchanged.
    /// </summary>
    public List<Be18RawField> ExtraFields { get; } = [];

    /// <summary>The last rid handed out by the counter.</summary>
    public static int LastRid => Volatile.Read(ref _lastRid);

    /// <summary>
    /// Resets the counter, so the next entity created gets <paramref name="lastRid"/> + 1. Use it
    /// to number a new project from #1. Entities created before the reset keep their rids, so only
    /// reset when they will not be saved in the same file as new entities.
    /// </summary>
    /// <param name="lastRid">The rid to continue after; 0 makes the next rid 1.</param>
    public static void ResetRidCounter(int lastRid = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(lastRid);
        Volatile.Write(ref _lastRid, lastRid);
    }

    /// <summary>Takes the next rid from the counter.</summary>
    internal static int NextRid() => Interlocked.Increment(ref _lastRid);

    /// <summary>Moves the counter forward so the next rid is above <paramref name="rid"/>.</summary>
    internal static void EnsureRidCounterAbove(int rid)
    {
        var current = Volatile.Read(ref _lastRid);
        while (current < rid)
        {
            var previous = Interlocked.CompareExchange(ref _lastRid, rid, current);
            if (previous == current)
                return;
            current = previous;
        }
    }

    /// <summary>
    /// Stops entities created on the current thread from taking rids from the counter until the
    /// returned scope is disposed. Used while loading a file, where the rids come from the file.
    /// </summary>
    internal static RidSuppressionScope SuppressRidCounter()
    {
        var scope = new RidSuppressionScope(t_suppressRids);
        t_suppressRids = true;
        return scope;
    }

    internal readonly struct RidSuppressionScope(bool previous) : IDisposable
    {
        public void Dispose() => t_suppressRids = previous;
    }
}
