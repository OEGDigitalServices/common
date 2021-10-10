﻿namespace Orange.Common.Entities
{
    public enum ErrorCode
    {
        Success = 0,
        NullResponse = 1,
        ServiceException = 2,
        UnspecifiedError = 3,
        StatusNotOne = 4,
        EaiBscsConnectionDown = 5,
        InvalidDial = 6,
        NonMobinil = 7,
        NoContract = 8,
        InvalidDialStatus = 9,
        TechnicalError = 10,
        NoGPRSService = 11,
        CorporatePostpaidNonPaymentResp = 12,
        UserHasMainBucketNotActive = 13,
        NoDataClass = 14,
        NoBuckets = 15,
        XMLTXNStatusElementNotExist = 16,
        ExistingUser = 17,
        NoneOrangeMoneySubscriber = 18,
        InvalidPin = 19,
        WrongPin = 20,
        WrongPinSecondTime = 21,
        WrongPinAccountDeactivated = 22,
        IncompatiblePin = 23,
        PINExpired = 24,
        XMLTXNstatusElementNotSuccess = 25,
        LockedAccount = 26,
        ResetPin = 27,
        ChannelIsNull = 28,
        DBExceptionWhileCheckingChannelInfo = 29,
        InvalidChannelInfo = 30,
        NoPrivilegeForChannel = 31,
        DialIsNull = 32,
        PinIsNull = 33,
        PasswordIsNull = 34,
        InvalidUserNameOrPassword = 35,
        XMLMessageElementNotSuccess = 36,
        XMLMessageElementNotExist = 37,
        MaximumAllowedCallsExceeded = 38,
        WalletBalanceInquiryServiceError = 39,
        NotEnoughCredit = 40,
        PendingRequest = 41,
        FailedToUnsubscribeFromInternetPackage = 42,
        PreventCustomerFromProvisioning = 43,
        DailyMaximumLimit = 44,
        NotEligibleSC = 45,
        NoEnoughBalance = 46,
        ExceededTheSubscriptionLimit = 47,
        BucketSpecialDiscount = 48,
        CustomerNotPrepaid = 49,
        BucketNotActive = 50,
        BucketNotWeeklyOrMonthly = 51,
        BucketTrafficTrigger = 52,
        NoActivatePAM = 53,
        BucketUnsubscribedByCustomer = 54,
        RequestInProgress = 55,
        NotAvailable = 56,
        SenderNotFound = 57,
        UserSuspended = 58,
        RequestedAmountLessThanAllowed = 59,
        RequestedAmountMoreThanAllowed = 60,
        BalanceNotSufficient = 61,
        NotReceivedFromEBC = 62,
        RequestFailedAtEBC = 63,
        CanNotMigrate = 64,
        DisAllow = 65,
        Warn = 66,
        InEligibleForRoamingOrInternational = 67,
        DialIsDisconnectedOrNotActive = 68,
        MaxCappingPerDay = 69,
        MaxCappingPerPromotion = 70,
        UsedCode = 71,
        WrongCode = 72,
        BlockedUser = 73,
        PromotionORRedeemingInvalidCode = 74,
        DialIsDeActive = 75,
        InvalidBucketCategory = 76,
        GPRSDeactivated = 77,
        InvalidBucketID = 78,
        PrimaryDialisSD = 79,
        PrimaryDialHasSIMSwapOffer = 80,
        PrimaryDialExceedsMaxNumber = 81,
        PrimaryExceedsMaxMBs = 82,
        WrongFormatOfSecondaryNumber = 83,
        SecondaryDialNotActive = 84,
        SecondaryIsOnIneligibleTariff = 85,
        MBsExceedsTheMaxLimit = 86,
        MBsExceedsTheRemainingQuota = 87,
        OwnerDoesnotHaveEnoughQuota = 88,
        InvalidBucketStaticFee = 89,
        InvalidFreePeriods = 90,
        CustomerNotFound=97,
        Timeout = 98,
        HasDueBills = 99,
        SuspendedDueToDunning = 100,
        SuspendedOnDemand = 101,
        EmptyServiceClass = 102,
        ExceededMaximumLimit = 103,
        FulfillYourRequest = 104,
        SenderNumberIsNotActive = 105,
        ReceiverNumberIsNotActive = 106,
        ReceiverNumberIsNotOrange = 107,
        ReceiverNumberIsInCorrect = 108,
        CustomerHasNoEnoughCredit = 109,
        ReceiverMsisdnGiftCardDAIsFull = 110,
        DonorMSISDNCorpNonPaymentResponsible = 111,
        ExceedNumberOfTimes = 112,
        BalanceOptionOnly = 113,
        IneligibleSC = 114,
        AlreadySubscribed = 115,
        NoDetails = 116,
        CustomerIsOnControlMiniProfile = 117,
        BalanceAndBillOptions = 118,
        BalanceOptionForEagleOnly = 119,
        AccountNotEligible = 120,
        AccountNotEligibleAndTrialsExceedsCounter = 121,
        HavePool = 122,
        HaveKey = 123,
        AlreadyCommunityOwnerOrMember = 124,
        WrongNumberFormat = 125,
        OtherOperatorNumber = 126,
        CommunityReachMaxCapacity = 127,
        MemberAddedToMaxNumberOfCommunities = 128,
        NoGiftAssignedToday = 129,
        TodayGiftAlreadyRedeemed = 130,
        InvalidRequestParams = 131,
        CustomerIsNotAMemberIn012Community = 132,
        CustomerNotOwner = 133,
        NoOfferExists = 134,
        CustomerHasNoSegments = 135,
        InvalidActionKey = 136,
        SubscriberNotFound = 137,
        InviterNotExist = 138,
        InviterExceedGiftsLimit = 139,
        InviteeAlreadyOrangeCashSubscriber = 140,
        InviteeAlreadyExistInInviterList = 141,
        InviteeAlreadyExistInAnotherInviterList = 142,
        InviterInBlacklist = 143,
        IneligibleDueToDebtAmount = 146,
        DidntConsumeEGP10DuringHisLifetimeWithOrange = 147,
        AlreadySubscribedAndDidnotUseUnits = 148,
        IneligibleRatePlan = 149,
        IneligibleDueToHavingCreditMoreThanThreshold = 150,
        CustomerIsEligibleButDoesnotHaveGPRS = 151,
        DialNotFound = 152,
        OptedInOnTheSubscriptionDay = 153,
        NotActive = 154,
        OneLEOfferSubscribed = 155,
        InactiveRomaing = 156,
        SplitButNotEnoughCreditToRenew = 158,
        DialNotRechargedYet = 159,
        AlreadySubscribedDoubleBucket = 160,
        SuccessForRAndR = 161,
        AddMemberTransactionsExceeded = 162,
        DeactivatedState = 163,
        AlreadyRedeemed = 164,
        NoConsumptionYesterday = 165,
        CustomerDidNotRechargeBeforeRedemption = 166,
        PromotionIdNotValid = 167,
        NotOrangeDial = 168,
        NotOptedIn = 169,
        NotOptedInOnTheFreeDay = 170,
        OptedInOnTheFreeDay = 171,
        SIMAndHandsetAreNotLTE = 172,
        SIMIsNotLTE = 173,
        HandsetIsNotLTE = 174,
        EligibleButNoRemaningDays = 175,
        PromoNotAvailable = 176,
        OptedInBeforeRamadan = 177,
        EagleSuccess = 178,
        EagleOptIn = 179,
        OfferExpired = 180,
        DeviceAlreadyTookOffer = 181,
        NoPendingInvitations = 182,
        InEligibleToViewFamily = 183,
        DialHasPendingRequest = 184,
        DialDoesNotHaveMonthlyMBs = 185,
        DialDoesNotHaveMonthlyMins = 186,
        OfferAvailableForIndividualCustomersOnly = 187,
        YouCannotUseService = 188,
        DeviceConsumedAllPromoGifts = 189,
        AlreadyOptedIn = 190,
        InvalidGPRS = 191,
        InvalidUSIM = 192,
        FailedToGetCustomerSegment = 193,
        InvalidVoucherActivationCode = 194,
        DialNotHaveService = 195,
        PasswordNotComptiable = 196,
        OfferIDIsEmpty = 197,
        OfferTaken = 198,
        SuccessOptIn = 199,
        SuccessOptOut = 200,
        PAMAddError = 201,
        PAMError = 202,
        InEligibleToSubscribeAtPrePaidSystem = 203,
        InEligibleToSubscribe = 204,
        FailedToAddBundle = 205,
        OptInNotEligible = 206,
        OptedInNotRechargedEligible = 207,
        OptedInNotRechargedNotEligible = 208,
        OptedInRechargedEligible = 209,
        OptedInRechargedNotEligible = 210,
        PostPaidEligibleHasInclusive = 211,
        PostPaidEligibleNoInclusive = 212,
        PostpaidOptedIn = 213,
        PostpaidSuccess = 214,
        DoubleTariffEligible = 215,
        DoubleTariffNotEligible = 216,


        

        NoOfferAndChannelsUpsellDiscountedOffer = 217,
        ExceededSpinningTheWheelCapping = 218,
        SpinningLockedTillNextOfferTime = 219,
        OfferReachedMaximumFulfillments = 220,
        DialIsCorporate = 221,
        Internal = 222,
        AccountAlreadyCreated = 223,
        OfferIdWrong = 224,
        SpinFirst = 225,
        OfferNotExist = 226,
        OfferNoLuck = 227,
        AlreadyFulfilled = 228,
        NumbersNotMatched = 229,
        SubscriberSimSwap = 230, 
        SuccessWithOnlyOneCategoryReturned = 231,
        NoExtensionFoundForCategory	= 232,
        BalanceOptionForEagleOnlyAsActivationNotExceeded2Months = 233,
        BalanceOptionForEagleOnlyAsCustomerHasDueBillsAndActivationNotExceeded4Month = 234,
        OfferOneEligible =235,
        OfferTwoEligible =236,
        OfferThreeEligible =237,
        UnSubscribedSuccessfully=238,
        PrepaidAndOtherNonEligibleHybrids = 239,
        StarControlAndPostpaidNonBuiltIn = 240,
        PostpaidNonBuiltIn = 241,
        B2BPostpaid_CAndCEligibleCustomers = 242,
        B2BPrepaidEligibleCustomers = 243,
        B2CHybridEligible = 244,
        InactivePostpaidDial = 245,
        InactivePrepaidDial = 246,
        InEligibleDial = 247,
        AccountIsBarred = 248,
        InvalidSourceID = 249,
        CustomerIsEligible = 250,
        ActivationMoreThan30Days = 251,
        ContractNotActive = 252,
        InValidSecondaryDial = 253,
        Home4GSummerOfferSuccess1 = 254,
        Home4GSummerOfferSuccess2 = 255,
        Home4GSummerOfferSuccess3 = 256,
        Home4GSummerOfferSuccess4 = 257,
        Home4GSummerOfferSuccess5 = 258,
        Home4GSummerOfferSuccess6 = 259,
        Home4GSummerOfferSuccess7 = 260,



        BillingAccNoStructureError = 262,
        BillerInActive = 263,
        BankInActive = 264,
        PaymentAmountValidationError = 265,
        BillValidationError = 266,
        BillNotAllowedForPayment = 267,
        PaymentAmdountExceedsDuePaymentAmount = 268,
        PaymentAmountLessDuePaymentAmount = 269,
        BillNotAvailableForPayment = 270,
        InvalidBillingAccount = 271,
        InvalidBillNo = 272,
        FawryPaymentAmdountExceedsDuePaymentAmount = 273,
        FawryPaymentAmountLessDuePaymentAmount = 274,
        InValidorSuspendesCustomer = 275,
        AlreadyFawryRegisteredCustomer = 276,


        NoGiftsFound = 277,
        NoMigrationFlag	= 278,
        CustomerNotHavePromotion = 279,
        DataOutOfBounds	= 280,
        SuccessSubscribedIntoDragonAndDolphinDYBPromo = 281,




        NotValidSCToReceiveMB = 282,
        CustomerIsInRAndRState = 283,
        RemainingUnitsLessThanLowestDenomination = 284,
        NumberOfTransfersEqualXValue = 285,
        CustomerUsedXOfHisQuotaToUnits = 286,
        PostpaidCustomerWithActivationDateLessThan2Months = 287,
        PostpaidCustomerWithActivationDateBetween2And4MonthsAndHasDueBills = 288,
        ServiceClassNotEligibleForThisService = 289,

        // Sharepoint error codes
        NoBucketsInSP = 10000,
        NoPackagesInSP = 10001,
        NoMappingInSP = 10002,
        DealOfTheMonthIsNull = 10003,
        GetCategoriesIsNull = 10004,
        GetPartnersIsNull = 10005,
        InvalidOfferAndActionFlow = 10008,
        HaciendaDialHasUnits = 10009,
        DragonAlreadySubscribed = 10010,
        AFCONSuccessWithNoOptions = 10011,
        AFCONAlreadySubscribed = 10012,
        InternetX4OptedInSubscriptionDay = 10013,
        AFCONAlreadySubscribedWithNoOptions = 10014,


        // Api error codes
        NoMainBucket = 20000,
        NoBucketsMapping = 20001,
        ErrorWhileBucketInfoMapping = 20002,
        ErrorGetCustomerActivatedBuckets = 20003,
        CorporateDialWithNoMainBucket = 20004,
        UnSubscribedUser = 20005,
        ErrorCheckDataProfileStatus = 20006,
        ErrorGetActivatedBucketBalanceV2 = 20007,
        EmptyLang = 20008,
        InvalidLanguage = 20009,
        EmptyBucketId = 20010,
        InvalidType = 20011,
        EmptyRPCode = 20012,
        EmptySPCode = 20013,
        EmptyBeginDate = 20014,
        EmptyEndDate = 20015,
        EmptyRenewDuration = 20016,
        EmptyTxnID = 20017,
        ZeroBuckets = 20018,
        DialNotBelongToAccount = 20019,
        AmountIsZeroOrLess = 20020,
        ActionTypeIsNull = 20021,
        EmptyPromoCode = 20022,
        InvalidKey = 20023,
        EmptyAppVersion = 20024,
        InvalidOSType = 20025,
        SectionFlagsListIsNull = 20026,
        BucketsIDsNotMappedSP = 20027,
        EmptyManageBucketMethodName = 20028,
        EmptySubscribeToBucketMethodName = 20029,
        EmptyUnSubscribeFromBucketMethodName = 20030,
        EmptyPrimaryDial = 20031,
        InvalidDataSharingActionType = 20032,
        EmptyCategoryID = 20033,
        EmptyPecSPDesc = 20043,
        InvalidDSLIdentification = 20049,
        NotEligible = 20050,
        EmptyDslUsername = 20051,
        EmptyProductID = 20052,
        EmptyUCID = 20053,
        DSLUserNotFound = 20054,
        EmptyLandlineNumber = 20055,
        EmptyCityCode = 20056,
        DslLandlineNotLinkedToDial = 20057,
        DslLandlineLinkedToDifferentDial = 20058,
        DSLCustomerNotEligible = 20059,
        InvalidPromoCode = 20060,
        AlreadyfamilyMember_Owner = 20061,
        DialHasPendingRequestToFamily = 20062,
        IneligibleDialFormat = 20064,
        FamilyOwnerNotHaveMembers = 20065,
        DialNotFamilyMember = 20066,
        ExceedsSharingAmount = 20067,
        EmptyFirstClass = 20038,
        EmptySpecialRevampCategory = 20039,
        GetTierAndPointsIsNULL = 20078,
        EmptyLoyaltyFlag = 20090,
        AmountSentNotEqualAmountReceived = 20068,
        SuccessPartialPayment = 20069,
        ParameterNotReturnFromMigs = 20070,
        PortalSecureHashIsNoValid = 20071,
        MIGSSecureHashIsNoValid = 20072,
        NoRequestFound = 20073,
        ExpiredRequest = 20074,
        MigsTransactionNumberConsumedBefore = 20075,
        MigsReceiptNumberConsumedBefore = 20076,
        MIGSResponseCodeNotZero = 20077,
        EndOfPromotion = 20080,
        CodeExpired = 20081,
        InvalidFamilyMemberDial = 20083,
        EmptyFamilyMemberDial = 20084,
        DialisDeactivated = 20085,
        NotWinningDial = 20086,
        CustomerHasDebt = 20087,
        MainBalance = 20088,
        RechargeLast30Days = 20089,
        EmptyGiftCardId = 20091,
        EmptyPaymentType = 20092,
        InvalidPaymentType = 20093,
        EmptyCustomerType = 20094,
        InvalidCustomerType = 20095,
        VoucherIdNotFound = 20096,
        NoGiftsFoundForThisDial = 20097,
        BlockedUserTillEndOfPromo = 20099,
        InvalidEagleRevampActionType = 20098,
        EmptyServiceID = 20100,
        DslLandLineNotExist = 20102,
        EmptyLocation = 20103,
        ProductTypeIsEmpty = 20104,
        EmptyFamilyOwnerDial = 20108,
        InvalidFamilyOwnerDial = 20109,
        ExceedsMonthlyQuota = 20110,
        EmptyMethod = 20111,
        InvalidOTP = 20112,
        EmptyHome4GDial = 20113,
        EmptySharingType = 20114,
        EmptyPlusSocialActionType = 20115,
        InvalidOfferIdOrAction = 20105,
        EmptyActionKey = 20106,
        InvalidActionId = 20120,
        InvalidOfferId = 20121,
        InvalidInviteeDial = 20118,
        InvalidPoints = 20119,
        InvalidManageCommunityActionType = 20116,
        InvalidTodayGiftActionType = 20117,
        DatesOutOfRange = 20101,
        UpsellingBannersNotFound = 20122,

        InvalidZeroTwelveActionType = 20124,
        InvalidSummerOfferId = 20125,
        InvalidSummerOfferActionId = 20126,
        EmptyServiceType = 20127,
        EmptyNationalID = 20128,
        InvalidNationalID = 20129,
        EmptyVariables = 20130,
        EmptyOptions = 20131,
        OfferNotOptedIn = 20132,
        EmptyReleaseNo= 20133,

        InValidAccountNo = 20134,
        InValidCVV = 20135,
        InValidOperationValue = 20136,
        InValidOrderValue = 20137,
        InValidDaysValue = 20138,
        InvalidManageGo2ServiceActionId = 20139,
        InvalidGoServiceId = 20140,
        InvalidMBsForTransferredOrForShared = 20141,
        InvalidUnits = 20147,
        InvalidReceiverDial = 20155,
        InvalidSharingFees = 20156,


     
     
        
        AccountNumberNotExists = 20142,
        AccountTemporaryLocked = 20143,
        PasswordNotSent =	20144,
        PasswordNotReset = 20145,
        EmptyName = 20146,
        EmptyMobileNo = 20148,
        InvalidMobileNo = 20149,
        EmptyCompanyName = 20150,
        EmptyCompanyAddress = 20151,
        EmptyIndustryType = 20152,
        EmptyDocumentsType = 20153,
        EmptyNoOfEmployees = 20154,
        EmailNotSent = 20157,
        ImageSizeExceeded = 20158,
        ImagesLimitExceeded = 20159,
        EmptyImages=20160,

    }
}