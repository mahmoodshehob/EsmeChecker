using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsmeChecker.Models.Sybase

{
    [Table("sme_info", Schema = "dbo")]
    public class SmeInfo
    {
        [Key]
        [Column("sme_index")]
        public int SmeIndex { get; set; }

        [Column("system_id")]
        [StringLength(255)] // Adjust length as needed
        public string SystemId { get; set; }

        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }

        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }

        [Column("system_type")]
        public byte SystemType { get; set; }

        [Column("interface_version")]
        public byte InterfaceVersion { get; set; }

        [Column("addr_ton_npi")]
        public byte AddrTonNpi { get; set; }

        [Column("changed")]
        public byte Changed { get; set; }

        [Column("check_code")]
        public byte CheckCode { get; set; }

        [Column("sendalarm")]
        public byte SendAlarm { get; set; }

        [Column("auth")]
        public byte Auth { get; set; }

        [Column("returnkind")]
        public byte ReturnKind { get; set; }

        [Column("addcountrycodeprefix")]
        public byte AddCountryCodePrefix { get; set; }

        [Column("scptype")]
        public byte ScpType { get; set; }

        [Column("transcodesche")]
        public byte TranscodeSche { get; set; }

        [Column("vasmttrigger")]
        public byte VasMtTrigger { get; set; }

        [Column("bfreesrcaddr")]
        public byte BFreeSrcAddr { get; set; }

        [Column("bsavesubmitsm")]
        public byte BSaveSubmitSm { get; set; }

        [Column("forbida2aesmesr")]
        public byte ForbidA2AEsmeSr { get; set; }

        [Column("forbida2pesmesr")]
        public byte ForbidA2PEsmeSr { get; set; }

        [Column("szdefauth")]
        [StringLength(255)]
        public string SzDefAuth { get; set; }

        [Column("baoorgpps")]
        public byte BAoOrgPps { get; set; }

        [Column("bwpmenable")]
        public byte BWpmEnable { get; set; }

        [Column("bearertype")]
        public byte BearerType { get; set; }

        [Column("codetype")]
        public byte CodeType { get; set; }

        [Column("fillvalidityperiod")]
        public byte FillValidityPeriod { get; set; }

        [Column("forbidloopsubmit")]
        public byte ForbidLoopSubmit { get; set; }

        [Column("window")]
        public int Window { get; set; }

        [Column("aoflowlimit")]
        public int AoFlowLimit { get; set; }

        [Column("atflowlimit")]
        public int AtFlowLimit { get; set; }

        [Column("aobuflimit")]
        public int AoBufLimit { get; set; }

        [Column("atbuflimit")]
        public int AtBufLimit { get; set; }

        [Column("genbill")]
        public byte GenBill { get; set; }

        [Column("atretryprofile")]
        public byte AtRetryProfile { get; set; }

        [Column("translinkcount")]
        public byte TransLinkCount { get; set; }

        [Column("recvlinkcount")]
        public byte RecvLinkCount { get; set; }

        [Column("transceivlinkcount")]
        public byte TransceivLinkCount { get; set; }

        [Column("optional_parameter")]
        public int OptionalParameter { get; set; }

        [Column("needfirstfailstareport")]
        public byte NeedFirstFailStaReport { get; set; }

        [Column("brole")]
        public byte BRole { get; set; }

        [Column("authaccwithmsc")]
        public byte AuthAccWithMsc { get; set; }

        [Column("authaccwithcellid")]
        public byte AuthAccWithCellId { get; set; }

        [Column("ucpatdestaddruse15x5y")]
        public byte UcpAtDestAddrUse15x5y { get; set; }

        [Column("forbidsendlink")]
        public byte ForbidSendLink { get; set; }

        [Column("authaccwithdcs")]
        public byte AuthAccWithDcs { get; set; }

        [Column("passwordauth")]
        public byte PasswordAuth { get; set; }

        [Column("authaccwithdestlocationinfo")]
        public byte AuthAccWithDestLocationInfo { get; set; }

        [Column("cimdesmetype")]
        public byte CimdEsmeType { get; set; }

        [Column("cimdcodetranssche")]
        public byte CimdCodeTransSche { get; set; }

        [Column("cimdatwithchecksum")]
        public byte CimdAtWithCheckSum { get; set; }

        [Column("rejectnumperday")]
        public int RejectNumPerDay { get; set; }

        [Column("rejectnumperhour")]
        public int RejectNumPerHour { get; set; }

        [Column("rejectnumpersec")]
        public int RejectNumPerSec { get; set; }

        [Column("alarmnumpersec")]
        public int AlarmNumPerSec { get; set; }

        [Column("appenddefultaccnum")]
        public byte AppendDefultAccNum { get; set; }

        [Column("allowalphoigaddr")]
        public byte AllowAlphOigAddr { get; set; }

        [Column("authaccwithtariffclass")]
        public byte AuthAccWithTariffClass { get; set; }

        [Column("origwithccwhenterm")]
        public byte OrigWithCcWhenTerm { get; set; }

        [Column("activeenabletime")]
        public int ActiveEnableTime { get; set; }

        [Column("activeexpiry")]
        public int ActiveExpiry { get; set; }

        [Column("bdefaultsrrequest")]
        public byte BDefaultSrRequest { get; set; }

        [Column("submitdefaultpid")]
        public byte SubmitDefaultPid { get; set; }

        [Column("submitdefaultdcs")]
        public byte SubmitDefaultDcs { get; set; }

        [Column("deliverywithpid")]
        public byte DeliveryWithPid { get; set; }

        [Column("deliverywithdcs")]
        public byte DeliveryWithDcs { get; set; }

        [Column("deliversrwithstatuserr")]
        public byte DeliverSrWithStatusErr { get; set; }

        [Column("tariffclass")]
        [StringLength(255)]
        public string TariffClass { get; set; }

        [Column("servicedescription")]
        [StringLength(255)]
        public string ServiceDescription { get; set; }

        [Column("origchargelaid")]
        public byte OrigChargeLaid { get; set; }

        [Column("authaccwithfee")]
        public byte AuthAccWithFee { get; set; }

        [Column("sr_request")]
        public byte SrRequest { get; set; }

        [Column("authaccwithexservicetype")]
        public byte AuthAccWithExServiceType { get; set; }

        [Column("deliverwithvmscaddr")]
        public byte DeliverWithVmscAddr { get; set; }

        [Column("deliverwithorigimsi")]
        public byte DeliverWithOrigImsi { get; set; }

        [Column("deliverwithscaddr")]
        public byte DeliverWithScAddr { get; set; }

        [Column("opid")]
        public short OpId { get; set; }

        [Column("forbidolodestuser")]
        public byte ForbidOloDestUser { get; set; }

        [Column("callbackcopy")]
        public byte CallbackCopy { get; set; }

        [Column("authaccwithdestimsi")]
        public byte AuthAccWithDestImsi { get; set; }

        [Column("addresston")]
        public byte AddrEsston { get; set; }

        [Column("allownulloigaddr")]
        public byte AllowNullOigAddr { get; set; }

        [Column("deliverywithcallbackmo")]
        public byte DeliveryWithCallbackMo { get; set; }

        [Column("deliverytovaswithcallbackao")]
        public byte DeliveryToVasWithCallbackAo { get; set; }

        [Column("supportdegradecharge")]
        public byte SupportDegradeCharge { get; set; }

        [Column("supportkeywordcharge")]
        public byte SupportKeywordCharge { get; set; }

        [Column("defaultcontentid")]
        [StringLength(255)]
        public string DefaultContentId { get; set; }

        [Column("authaccwithcontentid")]
        public byte AuthAccWithContentId { get; set; }

        [Column("deliverywithdefauth")]
        public byte DeliveryWithDefAuth { get; set; }

        [Column("atpdbtimeout")]
        public byte AtPdbTimeout { get; set; }

        [Column("keepidlelink")]
        public byte KeepIdleLink { get; set; }

        [Column("linkcheckinterval")]
        public int LinkCheckInterval { get; set; }

        [Column("msgidscheme")]
        public decimal MsgIdScheme { get; set; }

        [Column("trangroupid")]
        public decimal TranGroupId { get; set; }

        [Column("groupid")]
        public decimal GroupId { get; set; }

        [Column("origwithccwhenatterm")]
        public decimal OrigWithCcWhenAtTerm { get; set; }

        [Column("bcheckiponly")]
        public byte BCheckIpOnly { get; set; }

        [Column("vasdeliverwithsc")]
        public byte VasDeliverWithSc { get; set; }

        [Column("needmttimerestrict")]
        public byte NeedMtTimeRestrict { get; set; }

        [Column("authaccwithtransactionid")]
        public byte AuthAccWithTransactionId { get; set; }

        [Column("authaccwithflowflag")]
        public byte AuthAccWithFlowFlag { get; set; }

        [Column("authaccwithorigsystemid")]
        public byte AuthAccWithOrigSystemId { get; set; }

        [Column("authaccwithorigesmeindex")]
        public byte AuthAccWithOrigEsmeIndex { get; set; }

        [Column("loginpost")]
        public byte LoginPost { get; set; }

        [Column("loginmodule")]
        public byte LoginModule { get; set; }

        [Column("ismtusespecorigaddr")]
        public byte IsMtUseSpecOrigAddr { get; set; }

        [Column("mtspecaddr")]
        [StringLength(255)]
        public string MtSpecAddr { get; set; }

        [Column("mtspecaddrton")]
        public byte MtSpecAddrTon { get; set; }

        [Column("origstrategyid")]
        public byte OrigStrategyId { get; set; }

        [Column("default_addr_ton")]
        public byte DefaultAddrTon { get; set; }

        [Column("defaultaddr")]
        [StringLength(255)]
        public string DefaultAddr { get; set; }

        [Column("fullmemtransflash")]
        public byte FullMemTransFlash { get; set; }

        [Column("enablealias")]
        public byte EnableAlias { get; set; }

        [Column("actionwhensidinvalid")]
        public byte ActionWhenSidInvalid { get; set; }

        [Column("sme_index_backup")]
        public int SmeIndexBackup { get; set; }

        [Column("ucp57withoutoadc")]
        public byte Ucp57WithoutOadc { get; set; }

        [Column("ucp57withoutac")]
        public byte Ucp57WithoutAc { get; set; }

        [Column("strsystemtype")]
        [StringLength(255)]
        public string StrSystemType { get; set; }

        [Column("checksystemtype")]
        public byte CheckSystemType { get; set; }

        [Column("delivernextaftersuccess")]
        public byte DeliverNextAfterSuccess { get; set; }

        [Column("supportorigaddresscharge")]
        public byte SupportOrigAddressCharge { get; set; }

        [Column("sendsubmitackfirstly")]
        public byte SendSubmitAckFirstly { get; set; }

        [Column("supportsrrouterbyoa")]
        public byte SupportSrRouterByOa { get; set; }

        [Column("atdestaddrformat")]
        public byte AtDestAddrFormat { get; set; }

        [Column("supporttransactionmode")]
        public byte SupportTransactionMode { get; set; }
    }
}