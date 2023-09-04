using System.Text;

namespace Cifra_Cesar_Intellitrader;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;
        Console.Write("Do you wish to (e)ncrpyt or (d)ecrypt a text?\nInput -> ");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "e":
            case "encrypt":
            {
                EncryptUi();
                break;
            }
            case "d":
            case "decrypt":
            {
                DecryptUi();
                break;
            }
        }

    }

    private static void DecryptUi()
    {
        Console.WriteLine("Type bellow the text you wish to decrypt:");
        var inputString = Console.ReadLine();
        if (string.IsNullOrEmpty(inputString))
        {
            Console.WriteLine("Input string cannot be empty.");
            return;
        }

        Console.Write("Type the shift used in the original encryption (type 0 if you don't know): ");
        if (!int.TryParse(Console.ReadLine(), out var shift))
        {
            Console.WriteLine("Invalid shift value.");
            return;
        }

        if (shift > 0)
        {
            Console.WriteLine($"Encrypted string: {inputString}");
            Console.WriteLine($"Decrypted (shift value: {shift}): {Decrypt(inputString, shift)}");
        } else if (shift < 0)
        {
            Console.WriteLine("Shift value cannot be negative.");
        }
        else
        {
            Console.WriteLine(
                "Ok, since you don't know, then we will try every possibility until you find the original text.");
                Console.WriteLine($"Encrypted string: {inputString}");
                Console.WriteLine($"Decrypted (shift value: Unknown: {Decrypt(inputString)}");
        }
    }

    private static void EncryptUi()
    {
        Console.WriteLine("Type below the text you wish to encrypt:");
        string inputString = Console.ReadLine() ?? throw new InvalidOperationException();
        if (inputString == null) 
        {
            throw new InvalidOperationException("Input string cannot be empty");
        }

        Console.Write("Type the desired shift (only integers): ");
        if (!int.TryParse(Console.ReadLine(), out int shift))
        {
            throw new ArgumentException("Shift value must be an integer number");
        }

        Console.WriteLine($"Original string: {inputString}");

        string encryptedString;
        try
        {
            encryptedString = Encrypt(inputString, shift);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred during the encryption: {e.Message}");
            return;
        }

        Console.WriteLine($"Encrypted (shift value: {shift}): {encryptedString}");
    }

    private static string Decrypt(string text, int shift = 0)
    {
        if (ValidateString(text, shift) != "No problems with the text.")
        {
            return ValidateString(text, shift);
        }

        StringBuilder encryptedText = new();

        if (shift != 0)
        {
            foreach (char character in text)
            {
                if (char.IsLetter(character))
                {
                    encryptedText.Append(ProcessLetter(shift, character, "decrypt"));
                }
                else if (char.IsDigit(character))
                {
                    var number = Convert.ToInt32(character);
                    encryptedText.Append(number - shift);
                }
                else
                {
                    encryptedText.Append(character);
                }
            }
        }
        else
        {
            StringBuilder currentAttempt = new();
            for (int currentShift = 0; currentShift <= 26; currentShift++)
            {
                foreach (var character in text)
                {
                    if (char.IsLetter(character))
                    {
                        currentAttempt.Append(ProcessLetter(currentShift, character, "decrypt"));
                    }
                    else if (char.IsDigit(character))
                    {
                        var number = Convert.ToInt32(character);
                        currentAttempt.Append(number - currentShift);
                    }
                    else
                    {
                        currentAttempt.Append(character);
                    }
                }
                Console.WriteLine($"Doess this text looks intelligible ?\n{currentAttempt}\n(y/n)? ");
                var response = Console.ReadLine()?.ToLower(); 
                if (response == "y")
                {
                    encryptedText = currentAttempt;
                }
            }
        }

        return encryptedText.ToString();
    }

    private static string Encrypt(string text, int shift)
    {
        if (ValidateString(text, shift) != "No problems with the text.")
        {
            return ValidateString(text, shift);
        }

        StringBuilder encryptedText = new();

        foreach (var character in text)
        {
            if (char.IsLetter(character))
            {
                encryptedText.Append(ProcessLetter(shift, character, "encrypt"));
            }
            else if (char.IsDigit(character))
            {
                var number = Convert.ToInt32(character);
                encryptedText.Append(number + shift);
            }
            else
            {
                encryptedText.Append(character);
            }

        }

        return encryptedText.ToString();
    }

    private static string ValidateString(string text, int shift)
    {
        text = text.Trim();

        if (string.IsNullOrEmpty(text))
        {
            return "Empty texts are not valid.";
        }

        if (shift <= 0 || shift >= 26)
        {
            return "Shift must be between 1 and 26.";
        }

        return "No problems with the text.";
    }

    private static char ProcessLetter(int shift, char letter, string operation)
    {
        var letterUnicode = Convert.ToInt32(letter);
        var shiftedUnicode = letterUnicode + shift;
        if (operation == "encrypt")
        {
            switch (letterUnicode)
            {
                case >= 65 and <= 90 when shiftedUnicode > 90:
                    letterUnicode += shift - 25;
                    break;
                case >= 65 and <= 90:
                    letterUnicode += shift;
                    break;
                case >= 97 and <= 122 when shiftedUnicode > 122:
                    letterUnicode += shift - 25;
                    break;
                case >= 97 and <= 122:
                    letterUnicode += shift;
                    break;
                case >= 192 and <= 214 when shiftedUnicode > 214:
                    letterUnicode += shift - 22;
                    break;
                case >= 192 and <= 214:
                    letterUnicode += shift;
                    break;
                case >= 216 and <= 222 when shiftedUnicode > 222:
                    letterUnicode += shift - 6;
                    break;
                case >= 216 and <= 222:
                    letterUnicode += shift;
                    break;
                case >= 223 and <= 246 when shiftedUnicode > 246:
                    letterUnicode += shift - 23;
                    break;
                case >= 223 and <= 246:
                    letterUnicode += shift;
                    break;
                case >= 248 and <= 255 when shiftedUnicode > 255:
                    letterUnicode += shift - 7;
                    break;
                case >= 248 and <= 255:
                    letterUnicode += shift;
                    break;
            }
        }
        else if (operation == "decrypt")
        {
            switch (letterUnicode)
            {
                case >= 65 and <= 90 when shiftedUnicode < 65:
                    letterUnicode -= shift + 25;
                    break;
                case >= 65 and <= 90:
                    letterUnicode -= shift;
                    break;
                case >= 97 and <= 122 when shiftedUnicode < 97:
                    letterUnicode -= shift + 25;
                    break;
                case >= 97 and <= 122:
                    letterUnicode -= shift;
                    break;
                case >= 192 and <= 214 when shiftedUnicode < 192:
                    letterUnicode -= shift + 22;
                    break;
                case >= 192 and <= 214:
                    letterUnicode -= shift;
                    break;
                case >= 216 and <= 222 when shiftedUnicode < 216:
                    letterUnicode += shift - 6;
                    break;
                case >= 216 and <= 222:
                    letterUnicode -= shift;
                    break;
                case >= 223 and <= 246 when shiftedUnicode < 223:
                    letterUnicode -= shift + 23;
                    break;
                case >= 223 and <= 246:
                    letterUnicode -= shift;
                    break;
                case >= 248 and <= 255 when shiftedUnicode < 248:
                    letterUnicode -= shift + 7;
                    break;
                case >= 248 and <= 255:
                    letterUnicode -= shift;
                    break;
            }
        }

        return Convert.ToChar(letterUnicode);
    }
}