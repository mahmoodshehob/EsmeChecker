using AdoNetCore.AseClient;
using EsmeChecker.DataAccess.Helper;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Models.Sybase;
using System.Collections.Generic;
using System.Data;

namespace EsmeChecker.DataAccess.Repository
{
    public class SybaseRepository : ISybaseRepository
    {

		public static class SybaseConnectionString
		{
			public const string ConnectionString_mocscdb = "Data Source=192.168.75.3;Port=4100;Database=mocscdb;Uid=sa;Pwd=;";
			public const string ConnectionString_qasdb = "Data Source=192.168.75.12;Port=4100;Database=qasdb;Uid=sa;Pwd=;";
		}


		private AdoNetCore.AseClient.AseDataReader xreader;
        //private Esme ServiceP;

		public async Task<IEnumerable<Esme>> GetAllEsmes(string? filter = null)
        {
            List<Esme> ServiceP = new List<Esme>();

            try
            {
                string connectionString = SybaseConnectionString.ConnectionString_mocscdb;

                using (var connection = new AseConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        string baseQuery = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info ";
                        
                        if (filter != null)
                        {
                            //command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info "+SqlFilter +" ORDER BY activeenabletime DESC";
                            filter = $" WHERE {filter}";
                            command.CommandText = $"{baseQuery} {filter} ORDER BY activeenabletime DESC";


                        }
                        else 
                        {
                            //command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info ORDER BY activeenabletime DESC";
                            command.CommandText = $"{baseQuery} ORDER BY activeenabletime DESC";
                        }


                        using (var reader = command.ExecuteReader())
                        {
                            // Get the results.
                            while (reader.Read())
                            {
                                foreach (var item in reader)
                                {
                                    var obj = (object[])item;

                                    if(int.TryParse(obj[0].ToString(), out int value))
                                    {
                                        ServiceP.Add(new Esme()
                                        {
                                            System_Id = obj[0].ToString(),
                                            Description = obj[1].ToString(),
                                            Activeenabletime = new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(obj[3].ToString())).ToString("yyyy-MM-dd hh:mm:ss tt"),
                                            Activeexpiry = new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(obj[4].ToString())).ToString("yyyy-MM-dd hh:mm:ss tt")
                                        });
                                    }
                                }
                                return ServiceP;
                            }
                        }
                    }

                }

                return ServiceP;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                Esme ExcEsme = new Esme()
                {
                    System_Id = "Exp:0004",
                    Description = msg,
                };
                ServiceP.Clear();
                ServiceP.Add(ExcEsme);

                return ServiceP;
            }

        }

        public async Task<IEnumerable<SmeInfo>> GetAllEsmesInfo(string? filter = null)
        {
            List<SmeInfo> esmes = new List<SmeInfo>();
            string SqlFilter = string.Empty;

            try
            {
                string connectionString = SybaseConnectionString.ConnectionString_mocscdb;

                using (var connection = new AseConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        // Build the base query
                        string baseQuery = "SELECT * FROM mocscdb.dbo.sme_info";

                        // Apply filter if provided
                        if (!string.IsNullOrWhiteSpace(filter))
                        {
                            SqlFilter = $" WHERE {filter}";
                            command.CommandText = $"{baseQuery} {SqlFilter} ORDER BY activeenabletime DESC";
                        }
                        else
                        {
                            command.CommandText = $"{baseQuery} ORDER BY activeenabletime DESC";
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var sme = new SmeInfo
                                {
                                    SmeIndex = reader.GetInt32("sme_index"),
                                    SystemId = reader.IsDBNull("system_id") ? null : reader.GetString("system_id"),
                                    Description = reader.IsDBNull("description") ? null : reader.GetString("description"),
                                    Password = reader.IsDBNull("password") ? null : reader.GetString("password"),
                                    SystemType = reader.GetByte("system_type"),
                                    InterfaceVersion = reader.GetByte("interface_version"),
                                    AddrTonNpi = reader.GetByte("addr_ton_npi"),
                                    Changed = reader.GetByte("changed"),
                                    CheckCode = reader.GetByte("check_code"),
                                    SendAlarm = reader.GetByte("sendalarm"),
                                    Auth = reader.GetByte("auth"),
                                    ReturnKind = reader.GetByte("returnkind"),
                                    AddCountryCodePrefix = reader.GetByte("addcountrycodeprefix"),
                                    ScpType = reader.GetByte("scptype"),
                                    TranscodeSche = reader.GetByte("transcodesche"),
                                    VasMtTrigger = reader.GetByte("vasmttrigger"),
                                    BFreeSrcAddr = reader.GetByte("bfreesrcaddr"),
                                    BSaveSubmitSm = reader.GetByte("bsavesubmitsm"),
                                    ForbidA2AEsmeSr = reader.GetByte("forbida2aesmesr"),
                                    ForbidA2PEsmeSr = reader.GetByte("forbida2pesmesr"),
                                    SzDefAuth = reader.IsDBNull("szdefauth") ? null : reader.GetString("szdefauth"),
                                    BAoOrgPps = reader.GetByte("baoorgpps"),
                                    BWpmEnable = reader.GetByte("bwpmenable"),
                                    BearerType = reader.GetByte("bearertype"),
                                    CodeType = reader.GetByte("codetype"),
                                    FillValidityPeriod = reader.GetByte("fillvalidityperiod"),
                                    ForbidLoopSubmit = reader.GetByte("forbidloopsubmit"),
                                    Window = reader.GetInt32("window"),
                                    AoFlowLimit = reader.GetInt32("aoflowlimit"),
                                    AtFlowLimit = reader.GetInt32("atflowlimit"),
                                    AoBufLimit = reader.GetInt32("aobuflimit"),
                                    AtBufLimit = reader.GetInt32("atbuflimit"),
                                    GenBill = reader.GetByte("genbill"),
                                    AtRetryProfile = reader.GetByte("atretryprofile"),
                                    TransLinkCount = reader.GetByte("translinkcount"),
                                    RecvLinkCount = reader.GetByte("recvlinkcount"),
                                    TransceivLinkCount = reader.GetByte("transceivlinkcount"),
                                    OptionalParameter = reader.GetInt32("optional_parameter"),
                                    NeedFirstFailStaReport = reader.GetByte("needfirstfailstareport"),
                                    BRole = reader.GetByte("brole"),
                                    AuthAccWithMsc = reader.GetByte("authaccwithmsc"),
                                    AuthAccWithCellId = reader.GetByte("authaccwithcellid"),
                                    UcpAtDestAddrUse15x5y = reader.GetByte("ucpatdestaddruse15x5y"),
                                    ForbidSendLink = reader.GetByte("forbidsendlink"),
                                    AuthAccWithDcs = reader.GetByte("authaccwithdcs"),
                                    PasswordAuth = reader.GetByte("passwordauth"),
                                    AuthAccWithDestLocationInfo = reader.GetByte("authaccwithdestlocationinfo"),
                                    CimdEsmeType = reader.GetByte("cimdesmetype"),
                                    CimdCodeTransSche = reader.GetByte("cimdcodetranssche"),
                                    CimdAtWithCheckSum = reader.GetByte("cimdatwithchecksum"),
                                    RejectNumPerDay = reader.GetInt32("rejectnumperday"),
                                    RejectNumPerHour = reader.GetInt32("rejectnumperhour"),
                                    RejectNumPerSec = reader.GetInt32("rejectnumpersec"),
                                    AlarmNumPerSec = reader.GetInt32("alarmnumpersec"),
                                    AppendDefultAccNum = reader.GetByte("appenddefultaccnum"),
                                    AllowAlphOigAddr = reader.GetByte("allowalphoigaddr"),
                                    AuthAccWithTariffClass = reader.GetByte("authaccwithtariffclass"),
                                    OrigWithCcWhenTerm = reader.GetByte("origwithccwhenterm"),
                                    ActiveEnableTime = reader.GetInt32("activeenabletime"),
                                    ActiveExpiry = reader.GetInt32("activeexpiry"),
                                    BDefaultSrRequest = reader.GetByte("bdefaultsrrequest"),
                                    SubmitDefaultPid = reader.GetByte("submitdefaultpid"),
                                    SubmitDefaultDcs = reader.GetByte("submitdefaultdcs"),
                                    DeliveryWithPid = reader.GetByte("deliverywithpid"),
                                    DeliveryWithDcs = reader.GetByte("deliverywithdcs"),
                                    DeliverSrWithStatusErr = reader.GetByte("deliversrwithstatuserr"),
                                    TariffClass = reader.IsDBNull("tariffclass") ? null : reader.GetString("tariffclass"),
                                    ServiceDescription = reader.IsDBNull("servicedescription") ? null : reader.GetString("servicedescription"),
                                    OrigChargeLaid = reader.GetByte("origchargelaid"),
                                    AuthAccWithFee = reader.GetByte("authaccwithfee"),
                                    SrRequest = reader.GetByte("sr_request"),
                                    AuthAccWithExServiceType = reader.GetByte("authaccwithexservicetype"),
                                    DeliverWithVmscAddr = reader.GetByte("deliverwithvmscaddr"),
                                    DeliverWithOrigImsi = reader.GetByte("deliverwithorigimsi"),
                                    DeliverWithScAddr = reader.GetByte("deliverwithscaddr"),
                                    OpId = reader.GetInt16("opid"),
                                    ForbidOloDestUser = reader.GetByte("forbidolodestuser"),
                                    CallbackCopy = reader.GetByte("callbackcopy"),
                                    AuthAccWithDestImsi = reader.GetByte("authaccwithdestimsi"),
                                    AddrEsston = reader.GetByte("addresston"),
                                    AllowNullOigAddr = reader.GetByte("allownulloigaddr"),
                                    DeliveryWithCallbackMo = reader.GetByte("deliverywithcallbackmo"),
                                    DeliveryToVasWithCallbackAo = reader.GetByte("deliverytovaswithcallbackao"),
                                    SupportDegradeCharge = reader.GetByte("supportdegradecharge"),
                                    SupportKeywordCharge = reader.GetByte("supportkeywordcharge"),
                                    DefaultContentId = reader.IsDBNull("defaultcontentid") ? null : reader.GetString("defaultcontentid"),
                                    AuthAccWithContentId = reader.GetByte("authaccwithcontentid"),
                                    DeliveryWithDefAuth = reader.GetByte("deliverywithdefauth"),
                                    AtPdbTimeout = reader.GetByte("atpdbtimeout"),
                                    KeepIdleLink = reader.GetByte("keepidlelink"),
                                    LinkCheckInterval = reader.GetInt32("linkcheckinterval"),
                                    MsgIdScheme = reader.GetDecimal("msgidscheme"),
                                    TranGroupId = reader.GetDecimal("trangroupid"),
                                    GroupId = reader.GetDecimal("groupid"),
                                    OrigWithCcWhenAtTerm = reader.GetDecimal("origwithccwhenatterm"),
                                    BCheckIpOnly = reader.GetByte("bcheckiponly"),
                                    VasDeliverWithSc = reader.GetByte("vasdeliverwithsc"),
                                    NeedMtTimeRestrict = reader.GetByte("needmttimerestrict"),
                                    AuthAccWithTransactionId = reader.GetByte("authaccwithtransactionid"),
                                    AuthAccWithFlowFlag = reader.GetByte("authaccwithflowflag"),
                                    AuthAccWithOrigSystemId = reader.GetByte("authaccwithorigsystemid"),
                                    AuthAccWithOrigEsmeIndex = reader.GetByte("authaccwithorigesmeindex"),
                                    LoginPost = reader.GetByte("loginpost"),
                                    LoginModule = reader.GetByte("loginmodule"),
                                    IsMtUseSpecOrigAddr = reader.GetByte("ismtusespecorigaddr"),
                                    MtSpecAddr = reader.IsDBNull("mtspecaddr") ? null : reader.GetString("mtspecaddr"),
                                    MtSpecAddrTon = reader.GetByte("mtspecaddrton"),
                                    OrigStrategyId = reader.GetByte("origstrategyid"),
                                    DefaultAddrTon = reader.GetByte("default_addr_ton"),
                                    DefaultAddr = reader.IsDBNull("defaultaddr") ? null : reader.GetString("defaultaddr"),
                                    FullMemTransFlash = reader.GetByte("fullmemtransflash"),
                                    EnableAlias = reader.GetByte("enablealias"),
                                    ActionWhenSidInvalid = reader.GetByte("actionwhensidinvalid"),
                                    SmeIndexBackup = reader.GetInt32("sme_index_backup"),
                                    Ucp57WithoutOadc = reader.GetByte("ucp57withoutoadc"),
                                    Ucp57WithoutAc = reader.GetByte("ucp57withoutac"),
                                    StrSystemType = reader.IsDBNull("strsystemtype") ? null : reader.GetString("strsystemtype"),
                                    CheckSystemType = reader.GetByte("checksystemtype"),
                                    DeliverNextAfterSuccess = reader.GetByte("delivernextaftersuccess"),
                                    SupportOrigAddressCharge = reader.GetByte("supportorigaddresscharge"),
                                    SendSubmitAckFirstly = reader.GetByte("sendsubmitackfirstly"),
                                    SupportSrRouterByOa = reader.GetByte("supportsrrouterbyoa"),
                                    AtDestAddrFormat = reader.GetByte("atdestaddrformat"),
                                    SupportTransactionMode = reader.GetByte("supporttransactionmode")
                                };

                                esmes.Add(sme);
                            }
                        }
                    }
                }

                return esmes;
            }
            catch (Exception ex)
            {
                // Handle error
                var errorSme = new SmeInfo
                {
                    SystemId = "ERROR",
                    Description = ex.Message
                };
                return new List<SmeInfo> { errorSme };
            }
        }

        private DateTime? GetDateTimeFromSeconds(int seconds)
        {
            if (seconds <= 0) return null;
            return new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(seconds);
        }

        public Esme GetEsme(string systemID, string? filter =null)
        {
            string[] filterList = filter.Split(',');

            if (filter != "")
            {
                filter = string.Empty;

                foreach (var str in filterList)
                {
                    filter = str + "or ";
                }
            }

            try
            {
                string connectionString = SybaseConnectionString.ConnectionString_mocscdb;

                using (var connection = new AseConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        if (filter != null)
                        {
                            command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info WHERE system_id='" + systemID + " " + filter + "' ORDER BY activeenabletime DESC";
                        }
                        else
                        {
                            command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info WHERE system_id='" + systemID + "' ORDER BY activeenabletime DESC";
                        }



                        using (var reader = command.ExecuteReader())
                        {
                            // Get the results.
                            while (reader.Read())
                            {

                                var bb = reader.GetString(0);

                                return new Esme()
                                {
                                    System_Id = reader.GetString(0),
                                    Description = reader.GetString(1),
                                    Password = reader.GetString(2),
                                    Activeenabletime =  new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(reader.GetString(3))).ToString("yyyy-MM-dd hh:mm:ss tt"),
                                    Activeexpiry = new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(reader.GetString(4))).ToString("yyyy-MM-dd hh:mm:ss tt")
                                };
                            }
                        }
                    }
                }
                return new Esme()
                {
                    System_Id = "Exp:0003",
                    Description = null,
                };
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return new Esme()
                {
                    System_Id = "Exp:0004",
                    Description = msg,
                };
            }
        }

        public async Task<string> GetEsmeAuth(string SN , int MoMt)
        {
            Esme ServiceP = new Esme();
            string Cond = SN;
            string Message = null;

            try
            {
                string connectionString = SybaseConnectionString.ConnectionString_qasdb;

                using (var connection = new AseConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
SELECT mocscdb.dbo.mb_info.mb_number
FROM mocscdb.dbo.sme_info
INNER JOIN mocscdb.dbo.mb_info ON mocscdb.dbo.sme_info.sme_index = mocscdb.dbo.mb_info.sme_index
INNER JOIN mocscdb.dbo.mb_group ON mocscdb.dbo.mb_group.number_id = mocscdb.dbo.mb_info.number_id and mocscdb.dbo.mb_group.authtype ="+MoMt+"\n"+
"WHERE mocscdb.dbo.sme_info.system_id = '"+Cond+"'";



    
                        using (var reader = command.ExecuteReader())
                        {
                            
                            // Get the results
                            while (reader.Read())
                            {
                                var vv = reader;

                                List<string> AuthList = new List<string>();


                                foreach (object[] item in reader) 
                                {
                                    AuthList.Add(item.GetValue(0).ToString());

                                    Message = Message+ "\n" + item.GetValue(0).ToString();
                                }

                                if(Message==null)
                                    Message = "null";

                                return Message;
                            }
                        }
                    }

                }
                if (Message == null)
                    Message = "null";
                return Message;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return msg;

            }

        }

        public async Task<(string, List<Esme>)> NotifcationEsme()
        {
            string Message = null;

            List<Esme> ServiceP = new List<Esme>();
            MaxMinDate maxMinDate = new();
            maxMinDate = await maxMinDate.GetMaxMinTime();


            try
            {
                string connectionString = SybaseConnectionString.ConnectionString_mocscdb;

                using (var connection = new AseConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        //                        command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info Where system_id='" + Cond + "'";
                        command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info Where activeexpiry <= " + maxMinDate.MaxDateTimeSecond + " and activeexpiry >= " + maxMinDate.MinDateTimeSecond + " ORDER BY activeenabletime DESC";

                        using (AdoNetCore.AseClient.AseDataReader reader = command.ExecuteReader())
                        {

                            //// Get the results.
                            //while (reader.Read())
                            //{
                            foreach (var item in reader)
                            {
                                var obj = (object[])item;

                                if (int.TryParse(obj[0].ToString(), out int value))
                                {
                                    Esme esme = new Esme()
                                    {
                                        System_Id = obj[0].ToString(),
                                        Description = obj[1].ToString(),
                                        Activeenabletime = new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(obj[3].ToString())).ToString("yyyy-MM-dd hh:mm:ss tt"),
                                        Activeexpiry = new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(obj[4].ToString())).ToString("yyyy-MM-dd hh:mm:ss tt")

                                    };

                                    ServiceP.Add(esme);


                                    if (Message == null)
                                    {
                                        Message = Message + "Service Name : " + esme.Description + "\nShort Number : " + esme.System_Id + "\nExpiry Date : " + esme.Activeexpiry + "\n----------";
                                    }
                                    else
                                    {
                                        Message = Message + "\n" + "Service Name : " + esme.Description + "\nShort Number : " + esme.System_Id + "\nExpiry Date : " + esme.Activeexpiry + "\n----------";
                                    }
                                }

                            }

                            //}




                            return (Message, ServiceP);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                Esme ExcEsme = new Esme()
                {
                    System_Id = "Exp:0004",
                    Description = msg,
                };
                ServiceP.Clear();
                ServiceP.Add(ExcEsme);

                return (Message, ServiceP);

            }

        }

        public async Task<Esme> QueryEsme(string systemID)
        {
            Esme ServiceP = new Esme();

            try
            {
                string connectionString = SybaseConnectionString.ConnectionString_mocscdb;

                using (var connection = new AseConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info WHERE system_id='" + systemID + "'";

                        using (var reader = command.ExecuteReader())
                        {
                            // Get the results.
                            while (reader.Read())
                            {

                                var bb = reader.GetString(0);

                                ServiceP = new Esme()
                                {
                                    System_Id = reader.GetString(0),
                                    Description = reader.GetString(1),
                                    Password = reader.GetString(2),
                                    Activeenabletime = new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(reader.GetString(3))).ToString("yyyy-MM-dd hh:mm:ss tt"),
                                    Activeexpiry = new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(reader.GetString(4))).ToString("yyyy-MM-dd hh:mm:ss tt")
                                };
                            }
                        }
                    }

                }

                return ServiceP;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                ServiceP = new Esme()
                {
                    Description = "ErrorCode#0004 : " + msg,
                };


                return ServiceP;

            }

        }
    }
}