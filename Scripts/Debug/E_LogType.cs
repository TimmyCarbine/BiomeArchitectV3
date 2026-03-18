namespace BiomeArchitectV3.Scripts.Debug
{
    public enum E_LogType
    {
        Error,      // Red      - Critical errors that break expected behaviour
        Warning,    // Yellow   - Unexpected or undesirable behaviou that has not broken the system
        Success,    // Green    - Successful system operations
        Info,       // White    - General infomation about system processes
        Init,       // Purple   - Minimal logs associated with system initialisation
        Debug,      // Cyan     - Verbose diagnostics for develoment and debugging
        Trace       // Teal     - Extremely verbose diagnostics for development and debugging
    }
}