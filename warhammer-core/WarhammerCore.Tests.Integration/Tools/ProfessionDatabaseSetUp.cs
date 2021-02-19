using System.Collections.Generic;
using WarhammerCore.Data.Models;

namespace WarhammerCore.Tests.Integration.Tools
{
    /// <summary>
    /// Prepare database for professions tests.
    /// </summary>
    public class ProfessionDatabaseSetUp
    {
        private readonly WarhammerDbContext _dbContext;

        public ProfessionDatabaseSetUp(WarhammerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create database mock records for testing professions.
        /// </summary>
        /// <returns></returns>
        public void PrepareDatabase()
        {
            List<string> professionIds = new List<string>() { "ABBOT", "GAMBLER", "PIT_FIGHTER", "SERGEANT" };

            ProfessionEntity baseProfession = new ProfessionEntity()
            {
                Description = "profession-description",
                IsAdvanced = false,
                Label = "profession-label",
                Notes = "",
                Source = "",
                MainProfile = "dab",
                SecondaryProfile = "abc",
                NumberOfAdvances = 2,
                Role = "",
            };
            MainProfileEntity baseMainProfile = new MainProfileEntity()
            {
                Id = "dab",
                Ws = 100,
                Bs = 1,
                S = 1,
                T = 1,
                Ag = 1,
                Int = 1,
                Wp = 1,
                Fel = 1
            };
            SecondaryProfileEntity baseSecondaryProfile = new SecondaryProfileEntity()
            {
                Id = "abc",
                A = 100,
                W = 1,
                Sb = 1,
                Tb = 1,
                M = 1,
                Mag = 1,
                Ip = 1,
                Fp = 1
            };

            foreach (string professionId in professionIds)
            {
                SaveRecords(baseProfession, baseMainProfile, baseSecondaryProfile, professionId);
            }
        }

        /// <summary>
        /// Save profession in the database.
        /// </summary>
        private void SaveRecords(ProfessionEntity baseProfession, MainProfileEntity baseMainProfile, SecondaryProfileEntity baseSecondaryProfile, string professionId)
        {
            MainProfileEntity mainProfile = baseMainProfile;
            mainProfile.Id = $"{professionId}-main-profile";

            SecondaryProfileEntity secondaryProfile = baseSecondaryProfile;
            secondaryProfile.Id = $"{professionId}-secondary-profile";

            ProfessionEntity profession = baseProfession;
            profession.Id = professionId;
            profession.MainProfile = mainProfile.Id;
            profession.SecondaryProfile = secondaryProfile.Id;

            _dbContext.MainProfiles.Add(mainProfile);
            _dbContext.SecondaryProfiles.Add(secondaryProfile);
            _dbContext.Professions.Add(profession);
            _dbContext.SaveChanges();
        }
    }
}