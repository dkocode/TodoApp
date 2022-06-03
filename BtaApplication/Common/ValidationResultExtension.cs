using System.Text;

namespace BtaApplication.Common
{
    public static class ValidationResultExtension
    {
        public static string ToErrorMessage(this ValidationResult result)
        {
            var message = new StringBuilder();

            message.AppendLine($"Validation for {result.OperationType.ToString().ToLower()} operation failed!");

            message.AppendLine(string.Join("<br />", result.Errors));

            return message.ToString();
        }
    }
}