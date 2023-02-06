using Orange.Common.Entities;

namespace Orange.Common.Business
{
    public interface IOrangeCashManager
    {
        WalletBalanceInquiryOutput CheckDialAndPin(OrangeCashInput input);
    }
}
