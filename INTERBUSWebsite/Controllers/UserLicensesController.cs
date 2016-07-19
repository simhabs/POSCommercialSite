﻿using BTPOSDashboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace INTERBUSWebsite.Controllers
{
    public class UserLicensesController : ApiController
    {
        [HttpGet]

        public DataTable getUserLicenses()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetUserLicense";
            cmd.Connection = conn;

            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            // int found = 0;
            return Tbl;



        }
    
        [HttpPost]
        public HttpResponseMessage saveFleetOwnerRoutefare(UserLicense RouteFareConfig)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                    //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "InsUpdDelUserLicense";
                    cmd1.Connection = conn;
                    conn.Open();
                
       
                    SqlParameter vid1 = new SqlParameter();
                    vid1.ParameterName = "@UserId";
                    vid1.SqlDbType = SqlDbType.Int;
                    vid1.Value = RouteFareConfig.UserId;
                    cmd1.Parameters.Add(vid1);

                    SqlParameter ccd1 = new SqlParameter();
                    ccd1.ParameterName = "@FOId";
                    ccd1.SqlDbType = SqlDbType.Int;
                    ccd1.Value = RouteFareConfig.FOId;
                    cmd1.Parameters.Add(ccd1);

                    SqlParameter pu = new SqlParameter();
                    pu.ParameterName = "@LicenseTypeId";
                    pu.SqlDbType = SqlDbType.Int;
                    pu.Value = RouteFareConfig.LicenseTypeId;
                    cmd1.Parameters.Add(pu);

                     SqlParameter pid = new SqlParameter();
                    pid.ParameterName = "@GracePeriod";
                    pid.SqlDbType = SqlDbType.Int;
                    pid.Value = RouteFareConfig.GracePeriod;
                    cmd1.Parameters.Add(pid);
                
                    SqlParameter fd1 = new SqlParameter();
                    fd1.ParameterName = "@ActualExpiry";
                    fd1.SqlDbType = SqlDbType.DateTime;
                    fd1.Value = RouteFareConfig.ActualExpiry;
                    cmd1.Parameters.Add(fd1);

                
                
                    SqlParameter fid = new SqlParameter();
                    fid.ParameterName = "@StartDate";
                    fid.SqlDbType = SqlDbType.DateTime;
                    fid.Value = RouteFareConfig.StartDate;
                    cmd1.Parameters.Add(fid);
                  
                    SqlParameter sid = new SqlParameter();
                    sid.ParameterName = "@LastUpdatedOn";
                    sid.SqlDbType = SqlDbType.DateTime;
                    sid.Value = RouteFareConfig.LastUpdatedOn;
                    cmd1.Parameters.Add(sid);
                   
                    SqlParameter td1 = new SqlParameter();
                    td1.ParameterName = "@ExpiryOn";
                    td1.SqlDbType = SqlDbType.DateTime;
                    td1.Value = RouteFareConfig.ExpiryOn;
                    cmd1.Parameters.Add(td1);

                    SqlParameter hid = new SqlParameter();
                    hid.ParameterName = "@StatusId";
                    hid.SqlDbType = SqlDbType.Int;
                    hid.Value = RouteFareConfig.StatusId;
                    cmd1.Parameters.Add(hid);

                

                    cmd1.ExecuteScalar();


                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsUpdDelUserLicensePayments";
                cmd.Connection = conn;
              
                  
                List<ULLicense> License = null;
                if (RouteFareConfig.routeFare != null && RouteFareConfig.routeFare.Count > 0)
                {
                    License = RouteFareConfig.routeFare;
                }


                foreach (ULLicense b in License)
                {
                  
       
                  
                    SqlParameter aid = new SqlParameter();
                    aid.ParameterName = "@ULId";
                    aid.SqlDbType = SqlDbType.Int;
                    aid.Value = b.ULId;
                    cmd.Parameters.Add(aid);

                    SqlParameter s1id = new SqlParameter();
                    s1id.ParameterName = "@StatusId";
                    s1id.SqlDbType = SqlDbType.Int;
                    s1id.Value = b.StatusId;
                    cmd.Parameters.Add(s1id);
                       
                     SqlParameter cci= new SqlParameter();
                     cci.ParameterName = "@LicensePymtTransId";
                     cci.SqlDbType = SqlDbType.Int;
                     cci.Value = b.LicensePymtTransId;
                     cmd.Parameters.Add(cci);
                    
                    SqlParameter tid = new SqlParameter();
                    tid.ParameterName = "@IsRenewal";
                    tid.SqlDbType = SqlDbType.Int;
                    tid.Value = b.IsRenewal;
                    cmd.Parameters.Add(tid);
                   


                    SqlParameter dd = new SqlParameter();
                    dd.ParameterName = "@Amount";
                    dd.SqlDbType = SqlDbType.Decimal;
                    dd.Value = b.Amount;
                    cmd.Parameters.Add(dd);

                    SqlParameter lid = new SqlParameter();
                    lid.ParameterName = "@UnitPrice";
                    lid.SqlDbType = SqlDbType.Decimal;
                    lid.Value = b.UnitPrice;
                    cmd.Parameters.Add(lid);

                    SqlParameter kki = new SqlParameter();
                    kki.ParameterName = "@Units";
                    kki.SqlDbType = SqlDbType.Decimal;
                    kki.Value = b.Units;
                    cmd.Parameters.Add(kki);

                    SqlParameter qqi = new SqlParameter();
                    qqi.ParameterName = "@CreatedOn";
                    qqi.SqlDbType = SqlDbType.DateTime;
                    qqi.Value = b.CreatedOn;
                    cmd.Parameters.Add(qqi);

                    cmd.ExecuteScalar();

                    cmd.Parameters.Clear();
                }
               //connect to database
                conn.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                string str = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        public void Options()
        {

        }

    }
}

  