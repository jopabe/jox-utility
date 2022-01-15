using Microsoft.Win32;

namespace Jox.Utility;

public static class RegistryKeyExtensions
{
    public static void RecursivelyCopyTo(this RegistryKey from, RegistryKey to)
    {
        #if NET
        if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException("Registry is only supported on Windows.");
        #endif
        foreach (string valueName in from.GetValueNames())
        {
            to.SetValue(valueName, from.GetValue(valueName), from.GetValueKind(valueName));
        }

        foreach (string subKeyName in from.GetSubKeyNames())
        {
            from.OpenSubKey(subKeyName).RecursivelyCopyTo(to.CreateSubKey(subKeyName));
        }
    }
}
