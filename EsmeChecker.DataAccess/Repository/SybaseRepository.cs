using AdoNetCore.AseClient;
using EsmeChecker.DataAccess.Helper;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Models.Sybase;
using System.Collections.Generic;

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
        private Esme ServiceP;

		public async Task<List<Esme>> QueryAllEsme()

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
                        //  command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info Where system_id='" + Cond + "'";
                        command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info ORDER BY activeenabletime DESC";

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

        public async Task<Esme> CheckEsme(string systemID)
        {
            try
            {
                string connectionString = SybaseConnectionString.ConnectionString_mocscdb;

                using (var connection = new AseConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT system_id, description, password, activeenabletime, activeexpiry FROM mocscdb.dbo.sme_info WHERE system_id='"+ systemID + "'";

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
                                    Activeenabletime =  new DateTime(1994, 1, 1, 0, 0, 0).AddSeconds(int.Parse(reader.GetString(3))).ToString("yyyy-MM-dd hh:mm:ss tt"),
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
                    System_Id = "Exp:0004",
                    Description = msg,
                };


                return ServiceP;

            }

        }

        public async Task<string> EsmeAuth(string SN , int MoMt)
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
			maxMinDate= maxMinDate.GetMaxMinTime();


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




                            return (Message ,ServiceP);
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