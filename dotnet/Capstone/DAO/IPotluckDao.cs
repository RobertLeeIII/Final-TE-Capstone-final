using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPotluckDao
    {
        public IList<Potluck> GetPotlucks();
        public Potluck GetPotluckById(int potluckId);
        public Potluck GetPotluckByName(int potluckName);
        public Potluck CreatePotluck(NewPotluckDTO incomingPotluck);
        public Potluck UpdatePotluck(Potluck potluck, int hostId);

    }
}
