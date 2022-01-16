using System.DirectoryServices;

namespace Jox.Utility;

#if NET
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
public static class DirectoryServicesExtensions
{
    public static string FirstStringValue(this ResultPropertyValueCollection prop) => prop?[0]?.ToString() ?? string.Empty;
    public static string FirstStringValue(this SearchResult result, string propertyName) => result.Properties[propertyName].FirstStringValue();
    public static string DistinguishedName(this SearchResult result) => result.Path.Replace("LDAP://", "").Split(new char[] { '/' }, 2).Last();

    public static IEnumerable<SearchResult> SafeFindAll(this DirectorySearcher searcher)
    {
        searcher.PageSize = 1000;
        using var results = searcher.FindAll();
        foreach (var result in results.Cast<SearchResult>())
        {
            yield return result;
        }
    }
}
