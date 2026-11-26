using System;
using System.Text.RegularExpressions;

namespace PermissionsInspector.AppCode.Helpers
{
    public static class UrlParserHelper
    {
        private static readonly Regex GuidRegex = new Regex(@"[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}", RegexOptions.Compiled);

        public static bool TryParseDataverseUrl(string input, out string entityLogicalName, out Guid recordId)
        {
            entityLogicalName = string.Empty;
            recordId = Guid.Empty;

            if (string.IsNullOrWhiteSpace(input)) return false;

            input = input.Trim();

            // Check if input is simply a GUID
            if (Guid.TryParse(input.Trim('{', '}'), out var directGuid))
            {
                recordId = directGuid;
                return true;
            }

            try
            {
                // Parse URL parameters
                // e.g. https://myorg.crm.dynamics.com/main.aspx?appid=...&pagetype=entityrecord&etn=account&id=00000000-0000-0000-0000-000000000000
                var uri = new Uri(input);
                var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);

                string? etn = queryParams["etn"] ?? queryParams["typename"] ?? queryParams["entity"];
                string? idStr = queryParams["id"] ?? queryParams["recordid"];

                if (!string.IsNullOrEmpty(etn))
                {
                    entityLogicalName = etn.ToLowerInvariant();
                }

                if (!string.IsNullOrEmpty(idStr))
                {
                    var match = GuidRegex.Match(idStr);
                    if (match.Success && Guid.TryParse(match.Value, out var parsedGuid))
                    {
                        recordId = parsedGuid;
                    }
                }

                return !string.IsNullOrEmpty(entityLogicalName) || recordId != Guid.Empty;
            }
            catch
            {
                // Fallback regex search for etn and guid
                var etnMatch = Regex.Match(input, @"(?:etn|typename)=([a-zA-Z0-9_]+)", RegexOptions.IgnoreCase);
                if (etnMatch.Success)
                {
                    entityLogicalName = etnMatch.Groups[1].Value.ToLowerInvariant();
                }

                var guidMatch = GuidRegex.Match(input);
                if (guidMatch.Success && Guid.TryParse(guidMatch.Value, out var parsedGuid))
                {
                    recordId = parsedGuid;
                }

                return !string.IsNullOrEmpty(entityLogicalName) && recordId != Guid.Empty;
            }
        }
    }
}
