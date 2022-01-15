using System.Security;

namespace Jox.Utility;

public static class ConsoleHelpers
{
    public static SecureString ReadPassword() => ReadPassword('*');
    public static SecureString ReadPassword(char mask)
    {
        const char ENTER = (char)13;
        const char BACKSPACE = (char)8;
        const char CTRLBACKSPACE = (char)127;
        var clearPreviousCharacter = new string(new char[] { BACKSPACE, ' ', BACKSPACE });
        var pwd = new SecureString();

        char c;
        while ((c = Console.ReadKey(true).KeyChar) != ENTER)
        {
            switch (c)
            {
                case BACKSPACE:
                    if (pwd.Length > 0)
                    {
                        Console.Write(clearPreviousCharacter);
                        pwd.RemoveAt(pwd.Length - 1);
                    }
                    break;
                case CTRLBACKSPACE:
                    for (var i = 0; i < pwd.Length; i++) Console.Write(clearPreviousCharacter);
                    pwd.Clear();
                    break;
                case (char)0:
                case (char)27:
                case (char)9:
                case (char)10:
                    break;
                default:
                    pwd.AppendChar(c);
                    Console.Write(mask);
                    break;
            }
        }

        Console.WriteLine();
        pwd.MakeReadOnly();

        return pwd;
    }
}
