using System;
using System.Data.SqlClient;
using System.Data;
namespace CommonDatabaseAccess
{
    /// <summary>
    /// sql server 数据库访问类
    /// </summary>
    public class SqlServerDBA : CommonDatabaseAccessFactory
    {

        private SqlConnection conn = null;
        private SqlCommand cmd = null;

        private DataSet ds = null;

        private SqlDataAdapter da = null;
        private SqlTransaction tran = null;


        public SqlServerDBA()
        {
            this.conn = new SqlConnection(ConnectStr);

        }

        public override IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectStr);
        }

        public override void OpenConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public override void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public override IDbCommand GetCommand()
        {
            return new SqlCommand();
        }
        public override IDbCommand GetCommand(string sqlStr, string cmdType)
        {

            this.OpenConnection(conn);
            cmd = new SqlCommand(sqlStr, conn);
            cmd.CommandType = cmdType == "text" ? CommandType.Text : CommandType.StoredProcedure;

            cmd.CommandTimeout = 180;
            return cmd;

        }


        #region 数据获取
        public override DataTable GetDataTableBySql(string sqlStr)
        {
            ds = this.GetDataSetBySql(sqlStr);
            return ds == null ? null : ds.Tables[0];
        }

        public override DataSet GetDataSetBySql(string sqlStr)
        {
            cmd = (SqlCommand)this.GetCommand(sqlStr, "text");
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public override DataTable GetDaTatableByProc(string procName, object[] names, object[] values)
        {
            ds = this.GetDataSetByProc(procName, names, values);
            return ds == null ? null : ds.Tables[0];
        }

        public override DataSet GetDataSetByProc(string procName, object[] names, object[] values)
        {
            cmd = (SqlCommand)this.GetCommand(procName, "storeprocedure");
            cmd.CommandTimeout = 180;
            Array paras = Array.CreateInstance(typeof(SqlParameter), names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                paras.SetValue(new SqlParameter(names[i].ToString(), values[i]), i);
            }

            cmd.Parameters.AddRange(paras);

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public override object ExcuteScalar(string sqlStr)
        {

            cmd = (SqlCommand)this.GetCommand(sqlStr, "text");

            return cmd.ExecuteScalar();
        }

        public override IDataReader GetDataReaderBySql(string sqlStr)
        {
            cmd = (SqlCommand)this.GetCommand(sqlStr, "text");
            return cmd.ExecuteReader();
        }

        public override IDataReader GetDataReaderByProc(string procName, object[] names, object[] values)
        {
            cmd = (SqlCommand)this.GetCommand(procName, "storeprocedure");
            Array paras = Array.CreateInstance(typeof(SqlParameter), names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                paras.SetValue(new SqlParameter(names[i].ToString(), values[i]), i);
            }

            cmd.Parameters.AddRange(paras);

            return cmd.ExecuteReader();
        }
        #endregion

        #region 数据插入和更新
        public override int ExcuteNoneQueryNoTran(string sqlStr)
        {
            cmd = (SqlCommand)this.GetCommand(sqlStr, "text");
            return cmd.ExecuteNonQuery();
        }

        public override bool ExcuteNoneQueryByTran(string sqlStr)
        {
            cmd = (SqlCommand)this.GetCommand(sqlStr, "text");
            tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                this.CloseConnection(conn);
            }
        }
        #endregion
    }
}
