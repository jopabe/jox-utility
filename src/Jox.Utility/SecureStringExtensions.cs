using System.Runtime.InteropServices;
using System.Security;

namespace Jox.Utility;

public static class SecureStringExtensions
{
    public static string ToUnsecureString(this SecureString pwd)
    {
        var unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(pwd);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }

    public static SecureString ToSecureString(this IEnumerable<char> value)
    {
        var secureString = new SecureString();
        try
        {
            if (value != null)
            {
                foreach (var c in value)
                {
                    secureString.AppendChar(c);
                }
            }
            secureString.MakeReadOnly();
            return secureString;
        }
        catch (Exception)
        {
            secureString.Dispose();
            throw;
        }
    }
}
