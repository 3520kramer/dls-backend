using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkoleProtokolLibrary.Interfaces;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolAPI.Services
{
    /// <summary>
    /// Interacts with the roll-call mongoDB users collection
    /// </summary>
    public class RollCallUsersService
    {

        #region InstanceFields

        private readonly IMongoCollection<User> _users;//Collection of users from the mongoDB.

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a instance on RollCallUsersService and
        /// retrieves the roll-call database from mongoDB and
        /// extracts the user collection based on the IRollCallDatabaseSettings object.
        /// </summary>
        /// <param name="settings"></param>
        public RollCallUsersService(IRollCallDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollection);
        }

        #endregion

        #region Methods

        //public List<User> GetAll() =>
        //    _users.Find(user => true).ToList();


        #endregion
    }
}
