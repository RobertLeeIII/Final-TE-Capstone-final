using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPotluckDao
    {
        public IList<Potluck> GetPotlucks();
        public Potluck GetPotluckById(int potluckId);
        public Potluck GetPotluckByName(int potluckName);
        public IList<Potluck> GetPotlucksByUserId(int userId);
        public IList<Potluck> getPotluckByHostID(int hostId);
        public Potluck CreatePotluck(NewPotluckDTO incomingPotluck);
        public Potluck UpdatePotluck(UpdatePotluckDTO updatedPotluck, int potluckId);
        public int DeletePotluck(int potluckId);
        // TODO: Two separate SQL statments needed.
        // DELETE FROM potluck_user WHERE potluck_user.user_id = @user_id AND potluck_user.potluck_id = @potluck_id
        public int UninviteUser(int potluckId, int userId);

    }
}
