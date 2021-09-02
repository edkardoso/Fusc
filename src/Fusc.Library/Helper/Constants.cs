namespace Fusc.Library.Helper
{
    public static class Constants
    {
        // TODO: Levar para o App
        #region Errors Code  
        public const string ERROR_CODE_DEFAULT = "ER000";
        public const string ERROR_INTERNAL_ERROR_SERVER = "ER001";
        public const string ERROR_UNAUTHORIZED = "ER002";
        public const string ERROR_UNAUTHENTICATED = "ER003";
        #endregion

        #region Severity
        public const string SEVERITY_DEFAULT = "INFO";
        public const string SEVERITY_WARNING = "WARNING";
        public const string SEVERITY_CREATED = "INFO";
        public const string SEVERITY_NO_CONTENT = "WARNING";
        public const string SEVERITY_UNAUTHORIZED = "ERROR";
        public const string SEVERITY_FORBIDDEN = "ERROR";
        public const string SEVERITY_INVALID_DATA = "ERROR";
        public const string SEVERITY_BUSINESS_ERROR = "ERROR";
        public const string SEVERITY_FATAL_ERROR = "ERROR";
        #endregion

        #region Permission Codes
        // TODO: Levar para o App - deixar na lib apenas os de navegação
        public const string PERMISSION_ADD = "P_ADD";
        public const string PERMISSION_UPDATE = "P_UPD";
        public const string PERMISSION_DELETE = "P_DEL";
        public const string PERMISSION_SEARCH = "P_SEA";
        public const string PERMISSION_FIRST = "P_FST";
        public const string PERMISSION_NEXT = "P_NXT";
        public const string PERMISSION_LAST = "P_LST";
        public const string PERMISSION_PREVIOUS = "P_PREV";
        public const string PERMISSION_EXPORT_PDF = "P_PDF";
        #endregion

        public const string ACTION_GET = "GET";
        public const string ACTION_POST = "POST";
        public const string ACTION_PUT = "PUT";
        public const string ACTION_DELETE = "DELETE";

    }
}
