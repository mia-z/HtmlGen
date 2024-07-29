namespace HtmlGen.Hyperscript.Structs;

public readonly struct HyperscriptPart
{
    private string Command { get; init; }
    private string? FollowUp { get; init; }

    public static implicit operator string(HyperscriptPart part)
        => part.ToString();

    public override string ToString()
    {
        return FollowUp is null ? Command : $"{Command} {FollowUp}";
    }

    public HyperscriptPart(string command, string? followUp = null)
    {
        Command = command;
        FollowUp = followUp;
    }

    public static implicit operator HyperscriptPart(string input)
    {
        var (command, followUp) = input.Split(' ', 2) switch
        {
            { Length: 1 } splitArray => (splitArray[0], null), 
            { Length: 2 } splitArray => (splitArray[0], splitArray[1]),
            _ => throw new InvalidOperationException($"command invalid - got {input} which didnt split correctly")
        };

        return new HyperscriptPart(command, followUp);
    }
}