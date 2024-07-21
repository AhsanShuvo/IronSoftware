using System.Text;

namespace IronSoftware.PhonePadLib;

public class OldPhonePadServiceImp: IPhonePadService
{
    private readonly Dictionary<char, string> _keyMappings = new Dictionary<char, string>
    {
        { '2', "ABC" },
        { '3', "DEF" },
        { '4', "GHI" },
        { '5', "JKL" },
        { '6', "MNO" },
        { '7', "PQRS" },
        { '8', "TUV" },
        { '9', "WXYZ" }
    };
    
    public string ProcessInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.");
        }

        var result = new StringBuilder();
        var currentSequence = new StringBuilder();
        char? lastChar = null;

        foreach (char ch in input)
        {
            if (ch == '#')
            {
                if (currentSequence.Length > 0)
                {
                    result.Append(GetCharacterFromSequence(currentSequence.ToString()));
                }
                break;
            }

            if (ch == '*')
            {
                if (currentSequence.Length > 0)
                {
                    result.Append(GetCharacterFromSequence(currentSequence.ToString()));
                    currentSequence.Clear();
                }

                if (result.Length > 0)
                {
                    result.Length--;
                }
                continue;
            }

            if (ch == ' ')
            {
                if (currentSequence.Length > 0)
                {
                    result.Append(GetCharacterFromSequence(currentSequence.ToString()));
                    currentSequence.Clear();
                }

                lastChar = null;
                continue;
            }

            if (lastChar.HasValue && lastChar.Value == ch)
            {
                currentSequence.Append(ch);
            }
            else
            {
                if (currentSequence.Length > 0)
                {
                    result.Append(GetCharacterFromSequence(currentSequence.ToString()));
                    currentSequence.Clear();
                }

                currentSequence.Append(ch);
            }

            lastChar = ch;
        }

        return result.ToString();
    }
    
    private char GetCharacterFromSequence(string sequence)
    {
        if (sequence.Length == 0)
        {
            throw new ArgumentException("Sequence cannot be empty.");
        }

        char key = sequence[0];
        if (!_keyMappings.TryGetValue(key, out string? values))
            throw new ArgumentException($"Invalid key: {key}");

        string letters = _keyMappings[key];
        int index = (sequence.Length - 1) % letters.Length;

        return letters[index];
    }
}