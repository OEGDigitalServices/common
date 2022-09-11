using Orange.Common.Profile;

namespace Orange.Common.Business
{
    public class ProfileManager : IProfileManager
    {
        private readonly IProfileUtilities _profileUtilities;
        public ProfileManager(IProfileUtilities profileUtilities)
        {
            _profileUtilities = profileUtilities;
        }
        public bool IsRequestFromPortal()
        {
            return _profileUtilities.IsRequestFromPortal();
        }
    }
}
