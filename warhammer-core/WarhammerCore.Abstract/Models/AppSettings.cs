namespace WarhammerCore.Abstract.Models
{
    /// <summary>
    /// Application settings taken from appsettings.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Whether the application is ran by the developer and the error handlers can give more sensitive information.
        /// </summary>
        public bool IsDevelopmentModeOn { get; set; }
    }
}
