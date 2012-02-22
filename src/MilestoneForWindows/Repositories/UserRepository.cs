using Milestone.Core.Models;
using MilestoneForWindows.Application.Interfaces;

namespace MilestoneForWindows.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(IStorage storage)
            : base(storage)
        {

        }

        public User Add(NGitHub.Models.User user, bool isOrg = false)
        {
            var localUser = new User
                                {
                                    Username = user.Login,
                                    GravatarId = user.GravatarId,
                                    Name = user.Name,
                                    IsOrganisation = isOrg
                                };

            Add(localUser);

            return localUser;
        }
    }
}