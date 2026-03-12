namespace Adapter.Gateways.Notifications.Constants;

internal static class EmailNotificationConstants
{
    internal const string EMAIL_SUBJECT_FOR_SUCCESSFUL_VIDEO_PROCESSING = "VideoFrame - Seu frame foi gerado com sucesso!";
    internal const string EMAIL_SUBJECT_FOR_ERROR_VIDEO_PROCESSING = "VideoFrame - O processamento do seu frame falhou";

    internal const string EMAIL_TEMPLATE_FOR_SUCCESSFUL_VIDEO_PROCESSING = "SuccessEmailTemplate.html";
    internal const string EMAIL_TEMPLATE_FOR_ERROR_VIDEO_PROCESSING = "ErrorEmailTemplate.html";
}
