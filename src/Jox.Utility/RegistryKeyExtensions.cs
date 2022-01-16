using Microsoft.Win32;

namespace Jox.Utility;

#if NET
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
public static class RegistryKeyExtensions
{
    public static void RecursivelyCopyTo(this RegistryKey from, RegistryKey to)
    {
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
