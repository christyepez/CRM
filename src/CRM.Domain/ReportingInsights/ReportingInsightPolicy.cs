using System.Text.RegularExpressions;
namespace CRM.Domain.ReportingInsights;
public static class ReportingInsightPolicy
{
 public const int MaxTitleLength=160;
 public static (bool Valid,ReportingInsightValidationErrorCode Error,string Message,ReportingInsightSnapshot? Snapshot) Validate(ReportingInsightSnapshot s)
 {
  var key=(s.Key??string.Empty).Trim().ToLowerInvariant();var title=(s.Title??string.Empty).Trim();
  if(key.Length==0)return(false,ReportingInsightValidationErrorCode.KeyRequired,"Key required.",null);
  if(!Regex.IsMatch(key,"^[a-z0-9]+(?:-[a-z0-9]+)*$"))return(false,ReportingInsightValidationErrorCode.InvalidKey,"Invalid key.",null);
  if(title.Length==0)return(false,ReportingInsightValidationErrorCode.TitleRequired,"Title required.",null);
  if(title.Length>MaxTitleLength)return(false,ReportingInsightValidationErrorCode.TitleTooLong,"Title too long.",null);
  if(s.Metrics is null||s.Metrics.Count==0)return(false,ReportingInsightValidationErrorCode.MetricsRequired,"Metrics required.",null);
  var names=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
  foreach(var m in s.Metrics){var n=(m.Name??string.Empty).Trim();if(n.Length==0)return(false,ReportingInsightValidationErrorCode.InvalidMetricName,"Metric name required.",null);if(!names.Add(n))return(false,ReportingInsightValidationErrorCode.DuplicateMetricName,"Duplicate metric.",null);if(m.Value<0)return(false,ReportingInsightValidationErrorCode.NegativeMetricValue,"Negative metric.",null);}
  if(!string.Equals(s.SourceMode,"FoundationMock",StringComparison.Ordinal))return(false,ReportingInsightValidationErrorCode.InvalidSourceMode,"FoundationMock required.",null);
  return(true,ReportingInsightValidationErrorCode.None,"Accepted.",s with{Key=key,Title=title});
 }
}