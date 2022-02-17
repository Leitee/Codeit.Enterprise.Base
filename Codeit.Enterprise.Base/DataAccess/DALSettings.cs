/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.DataAccess
{
    public class DALSettings
    {
        public const string SectionKey = "DalSection";
        public bool? UseDatabase { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseConnection { get; set; }
        public bool? EnableDetailedDebug { get; set; }

        public override string ToString()
        {
            return nameof(DALSettings);
        }
    }
}
