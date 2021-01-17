using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Data.Models;

namespace WarhammerCore.Data
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public partial class DataRepo : IDataRepo
    {
        private readonly WarhammerDbContext _db;

        public DataRepo(WarhammerDbContext db)
        {
            _db = db;
        }
    }
}