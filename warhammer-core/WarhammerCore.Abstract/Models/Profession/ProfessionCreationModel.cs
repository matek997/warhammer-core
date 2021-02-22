using System.Collections.Generic;

namespace WarhammerCore.Abstract.Models
{
    /// <summary>
    /// Profession can be created in the database based on this model.
    /// </summary>
    public class ProfessionCreationModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public bool IsAdvanced { get; set; }
        public MainProfile MainProfile { get; set; }
        public SecondaryProfile SecondaryProfile { get; set; }
        public List<string> AdvanceFrom { get; set; }
        public List<string> AdvanceTo { get; set; }
    }
}