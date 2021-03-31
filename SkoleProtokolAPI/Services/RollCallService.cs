using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkoleProtokolLibrary.Interfaces;
using SkoleProtokolLibrary.Models;

namespace SkoleProtokolAPI.Services
{
    public class RollCallService
    {

        #region InstanceFields

        private readonly IMongoCollection<User> _users;

        #endregion

        #region Constructor

        public RollCallService(IRollCallDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.CollectionName);
        }

        #endregion

        #region Methods

        public List<User> GetAll() =>
            _users.Find(user => true).ToList();


        #endregion
    }
}
