using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPotluckDao
    {
        public IList<Potluck> GetPotlucks();
        public Potluck GetPotluckById(int potluckId);
        public Potluck GetPotluckByName(string potluckName);
        public IList<Potluck> GetPotlucksByUserId(int userId);
        public IList<Potluck> GetPotluckByHostID(int hostId);
        public Potluck CreatePotluck(NewPotluckDTO incomingPotluck);
        public Potluck UpdatePotluck(UpdatePotluckDTO updatedPotluck, int potluckId);
        public int DeletePotluck(int potluckId, int userId, int courseId);
        // TODO: Two separate SQL statments needed.
        // DELETE FROM potluck_user WHERE potluck_user.user_id = @user_id AND potluck_user.potluck_id = @potluck_id
        public Potluck GetPotluckDetailsById(int potluckId);

    }
}
