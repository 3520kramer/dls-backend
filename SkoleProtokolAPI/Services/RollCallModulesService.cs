using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkoleProtokolLibrary.DBModels;
using SkoleProtokolLibrary.Interfaces;

namespace SkoleProtokolAPI.Services
{
    public class RollCallModulesService
    {

        #region InstanceFields

        private readonly IMongoCollection<DBModule> _modules;

        #endregion



        #region Constructor
        /// <summary>
        /// Initializes a instance on RollCallModulesService and
        /// retrieves the roll-call database from mongoDB and
        /// extracts the modules collection based on the IRollCallDatabaseSettings object.
        /// </summary>
        /// <param name="settings">Settings for connecting to a mongoDB, is injected automatically</param>
        public RollCallModulesService(IRollCallDatabaseSettings settings)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("SkoleprotokolMongoConnection"));
            var database = client.GetDatabase(settings.DatabaseName);

            _modules = database.GetCollection<DBModule>(settings.ModulesCollection);
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gets all the modules from the database
        /// </summary>
        /// <returns>List of DBModules</returns>
        public List<DBModule> GetModules()
        {
            return _modules.Find(module => true).ToList();
        }

        #endregion

    }
}
