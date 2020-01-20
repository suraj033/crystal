using crystalthoughts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace crystalthoughts.Models
{
    public class DLayer
    {

        //private string con = "data source=103.228.112.145;initial catalog=CrystalData;user id=Crystaluser;password=5p@ck66Q";
        private string con = "data source=WINSOME-48;initial catalog=CrystalThoughts;user id=sa;password=sql@2014";


        public string Con
        {
            get
            {
                return con;
            }
        }
        public static byte[] pImage;
        public int Int_Process(String Storp, string[] parametername, string[] parametervalue)
        {
            int a = 0;

            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand(Storp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < parametername.Length; i++)
            {
                if (parametername[i] == "@img")
                {
                    cmd.Parameters.AddWithValue(parametername[i], pImage);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parametername[i], parametervalue[i]);
                }
            }
            con.Open();
            try
            {
                a = cmd.ExecuteNonQuery();

            }
            catch (SqlException ErrorCode)
            {
                switch (ErrorCode.Number)
                {
                    case 2627:
                    // Unique constraint error break; 
                    case 547:
                    // Constraint check violation break; 
                    case 2601:
                        // Duplicated key row error 
                        a = -2627;
                        break;
                    default: break;

                }
            }
            catch (Exception ex)
            {
            }
            con.Dispose();
            return a;
        }
        public DataSet Ds_Process(String Storp, string[] parametername, string[] parametervalue)
        {
            try
            {

                SqlConnection con = new SqlConnection(Con);
                SqlCommand cmd = new SqlCommand(Storp, con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parametername.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parametername[i], parametervalue[i]);
                }
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                con.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                DataSet ds = null;
                return ds;
            }
        }
        public DataSet MyDs_Process(String Storp)
        {

            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand(Storp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            con.Dispose();
            return ds;

        }
        public int ExecNonQuery(String Qry)
        {
            int a = 0;

            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand(Qry, con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            con.Open();
            a = cmd.ExecuteNonQuery();
            con.Dispose();
            return a;
        }

        //----------------------Data Access Layer Work---------------------------
        public DataSet FetchMaster(Property p)
        {
            try
            {
                string[] paname = { "@Condition1", "@Condition2", "@Condition3", "@Condition4", "@Condition5", "@Condition6", "@Condition7", "@Condition8", "@OnTable" };
                string[] pavalue = { p.Condition1, p.Condition2, p.Condition3, p.Condition4, p.Condition5, p.Condition6, p.Condition7, p.Condition8, p.onTable };
                return Ds_Process("FetchMaster", paname, pavalue);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //----------------------------------Insert Code-----------------------------------------
        public int InsertUser_Data(Account_Data u)
        {
            string[] pname = { "@Account_id", "@Name", "@Email", "@Paasword", "@Phoneno", "@Status" };
            string[] pvalue = { u.Account_id, u.Name, u.Email, u.Paasword, u.Phoneno,u.Status };
            return Int_Process("InsertAccountData", pname, pvalue);
        }

        public int Insertcomment(Account_Data ac)
        {
            string[] pname = { "@comment_id", "@user_id", "@comment", "@videoid" ,"@status" };
            string[] pvalue = { ac.comment_id,ac.user_id,ac.comment,ac.videoid,ac.Status };
            return Int_Process("Insertcomment", pname, pvalue);
        }

        public int InsertContactus(Contectusdata co)
        {
            string[] pname = { "@Contectid", "@Name", "@Email", "@Subject", "@Message" };
            string[] pvalue = {co.Contectid,co.Name,co.Email,co.Subject,co.Message };
            return Int_Process("InsertContactus", pname, pvalue);
        }

        public int Insertblog(blogdata bg)
        {
            string[] pname = { "@blogid", "@title", "@image", "@videourl", "@discription","@status" };
            string[] pvalue = { bg.blogid,bg.title,bg.image,bg.videourl,bg.discription,bg.status };
            return Int_Process("Insertblog", pname, pvalue);
        }
        public int Insertaboutus(aboutus abs)
        {
            string[] pname = { "@aboutid", "@title", "@image", "@Description" };
            string[] pvalue = { abs.aboutid,abs.title,abs.image,abs.Description };
            return Int_Process("Insertaboutus", pname, pvalue);
        }

        public int Blog_Insertcomment(Account_Data ac)
        {
            string[] pname = { "@comment_id", "@user_id", "@comment", "@videoid", "@status" };
            string[] pvalue = { ac.comment_id, ac.user_id, ac.comment, ac.videoid, ac.Status };
            return Int_Process("Blog_Insertcomment", pname, pvalue);
        }
        //--------------------------------------------------------------------------------------
    }
}