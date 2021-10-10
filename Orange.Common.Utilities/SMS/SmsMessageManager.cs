using Orange.Common.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Orange.Common.Utilities
{
    public class SmsMessageManager : ISmsMessageManager
    {
        #region Props

        int Accountid = 0;
        private readonly ILogger _logger;

        #endregion

        #region Ctors

        public SmsMessageManager(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// checks the account if ok Send 0 else -1
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public int Initialize(string Username, string Password)
        {
            Accountid = CheckAccount(Username, Password);
            if (Accountid > 0)
                return 0;
            else
                return -1;
        }
        private int CheckAccount(string Username, string Password)
        {
            SqlConnection gatewayConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SMSConnectionString"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("PR_AccountLogin", gatewayConnection);
            // Mark the Command as a SPROC
            sqlCommand.CommandType = CommandType.StoredProcedure;
            // Add Parameters to SPROC
            SqlParameter parameter1 = new SqlParameter("@AU_Login", SqlDbType.NVarChar, 50);
            parameter1.Value = Username;
            sqlCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter("@AU_Password", SqlDbType.NVarChar, 50);
            parameter2.Value = Password;
            sqlCommand.Parameters.Add(parameter2);

            SqlParameter parameter4 = new SqlParameter("@AU_Id", SqlDbType.Int, 4);
            parameter4.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(parameter4);
            try
            {
                gatewayConnection.Open();
                sqlCommand.ExecuteNonQuery();
                gatewayConnection.Close();
                int id = Int32.Parse(parameter4.Value.ToString());
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex, false);
                return 0;
            }
            finally
            {
                if (gatewayConnection.State == ConnectionState.Open)
                    gatewayConnection.Close();
            }
        }

        public int SendMessage(MessageDetails Msg)
        {
            int Messageid = -1;
            SqlConnection sqlConnection = null;
            try
            {
                if (Accountid == 0)
                    return -1;
                //add the Message   
                sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SMSConnectionString"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("PR_AddMessage", sqlConnection);
                // Mark the Command as a SPROC
                sqlCommand.CommandType = CommandType.StoredProcedure;
                // Add Parameters to SPROC

                SqlParameter parameter1 = new SqlParameter("@Message_Body", SqlDbType.VarChar, 650);
                parameter1.Value = Msg.MessageBody;
                sqlCommand.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter("@AU_ID", SqlDbType.Int, 4);
                parameter2.Value = this.Accountid;
                sqlCommand.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter("@AUO_Target", SqlDbType.NVarChar, 50);
                parameter3.Value = Msg.MessageTarget;
                sqlCommand.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter("@MessageId", SqlDbType.Int, 4);
                parameter4.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter("@Message_Creation_Date", SqlDbType.DateTime, 8);
                parameter5.Value = DateTime.Now;
                sqlCommand.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter("@AUO_Creation_DateTime", SqlDbType.DateTime, 8);
                parameter6.Value = DateTime.Now;
                sqlCommand.Parameters.Add(parameter6);

                //SqlParameter parameter7 = new SqlParameter("@AUO_Retrial_Hours", SqlDbType.Int ,4);
                //parameter7.Value = 5;
                //sqlCommand.Parameters.Add(parameter7);

                SqlParameter parameter7 = new SqlParameter("@AUO_Status", SqlDbType.Int, 4);
                parameter7.Value = 0;
                sqlCommand.Parameters.Add(parameter7);

                SqlParameter parameter8 = new SqlParameter("@AUO_Number", SqlDbType.Int, 4);
                parameter8.Value = 5555;
                sqlCommand.Parameters.Add(parameter8);

                SqlParameter parameter9 = new SqlParameter("@option", SqlDbType.Int, 4);
                parameter9.Value = Msg.MessageOption;
                sqlCommand.Parameters.Add(parameter9);

                SqlParameter parameter10 = new SqlParameter("@AUO_Creation_Date", SqlDbType.DateTime, 8);
                parameter10.Value = DateTime.Now;
                sqlCommand.Parameters.Add(parameter10);

                SqlParameter parameter11 = new SqlParameter("@AUO_Alias", SqlDbType.NVarChar, 50);
                parameter11.Value = Msg.Alias;
                sqlCommand.Parameters.Add(parameter11);

                SqlParameter parameter12 = new SqlParameter("@AUO_Reference_ID", SqlDbType.NVarChar, 100);
                parameter12.Value = Msg.ReferenceID;
                sqlCommand.Parameters.Add(parameter12);


                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                // Calculate the CustomerID using Output Param from SPROC
                Messageid = (int)parameter4.Value;

                sqlCommand = new SqlCommand("PR_IncreaseMessagesAccount", sqlConnection);
                // Mark the Command as a SPROC
                sqlCommand.CommandType = CommandType.StoredProcedure;
                // Add Parameters to SPROC

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                System.Diagnostics.EventLog.WriteEntry("Send Message Error:", exp.Message, System.Diagnostics.EventLogEntryType.Error);
                _logger.LogError(exp.StackTrace, exp, false);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return Messageid;
        }

        #endregion
    }
}
