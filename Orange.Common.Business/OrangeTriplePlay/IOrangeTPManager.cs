using Orange.Common.Entities;
using Orange.Common.Entities.OrangeTriplePlay;

namespace Orange.Common.Business.OrangeTriplePlay
{
    public interface IOrangeTPManager
    {
        bool IsUserIdentified(TPInput input, Channel channel, ModulesNames moduleName);
    }
}
