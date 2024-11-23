using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CarRental.Models
{
    public class DBCls
    {
        SqlConnection con=new SqlConnection(@"server=DESKTOP-N5FGT72\SQLEXPRESS;database=CarRental;integrated security=true");


        public string AdminLoginInsert(Admincls admincls,int regid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_adminlogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@adid", regid);
                cmd.Parameters.AddWithValue("@adname", admincls.name);
                cmd.Parameters.AddWithValue("@ademail", admincls.email);
                cmd.Parameters.AddWithValue("@adphn", admincls.phone);
                cmd.Parameters.AddWithValue("@regid", regid);
                cmd.Parameters.AddWithValue("@uname", admincls.uname);
                cmd.Parameters.AddWithValue("@pwd", admincls.pwd);
                cmd.Parameters.AddWithValue("@utype", "Admin");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //return "admin and";
                return "inserted";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw ex;
            }
        }

        

        public string UserLoginInsert(Usercls usercls, int regid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_userlogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", regid);
                cmd.Parameters.AddWithValue("@name", usercls.name);
                cmd.Parameters.AddWithValue("@uadd", usercls.uadd);
                cmd.Parameters.AddWithValue("@uphn", usercls.uphn);
                cmd.Parameters.AddWithValue("@uemail", usercls.uemail);
                cmd.Parameters.AddWithValue("@ustatus","Active");
                cmd.Parameters.AddWithValue("@regid", regid);
                cmd.Parameters.AddWithValue("@uname", usercls.username);
                cmd.Parameters.AddWithValue("@pwd", usercls.upwd);
                cmd.Parameters.AddWithValue("@utype", "User");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //return "user and";
                return "inserted";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw ex;
            }
        }

        public string GetRegId()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_loginmaxregid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                con.Open();
                string cid = cmd.ExecuteScalar().ToString();
                con.Close();
                
                return cid;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }

        }
        public string CarInsert(Carcls carcls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_carinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@brand", carcls.brand);
                cmd.Parameters.AddWithValue("@model", carcls.model);
                cmd.Parameters.AddWithValue("@year", carcls.year);
                cmd.Parameters.AddWithValue("@color", carcls.color);
                cmd.Parameters.AddWithValue("@numberplate", carcls.numberplate);
                cmd.Parameters.AddWithValue("@engine", carcls.engine);
                cmd.Parameters.AddWithValue("@fuel", carcls.fuel);
                //cmd.Parameters.AddWithValue("@image", carcls.image);
                cmd.Parameters.AddWithValue("@seatcap", carcls.seatcapacity);

                if (!string.IsNullOrEmpty(carcls.image))
                {
                    cmd.Parameters.AddWithValue("@image", carcls.image);
                }

                con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        
        return "inserted";
    }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }

        }

        public int Logincountid(Logincls logincls)
        {
            
               
            try
            {
                SqlCommand cmd = new SqlCommand("sp_logincountid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", logincls.uname);
                cmd.Parameters.AddWithValue("@pwd", logincls.pwd);
                //cmd.Parameters.AddWithValue("@utype", admincls.utype);
                con.Open();
                var cid = cmd.ExecuteScalar().ToString();
               
                int id = Convert.ToInt32(cid);
                con.Close();
                return id;
                
                    
                
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }
        }

        public int Loginregid(Logincls logincls)
        {


            try
            {
                
                    SqlCommand cmd1 = new SqlCommand("sp_loginregid", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@uid", logincls.uname);
                    cmd1.Parameters.AddWithValue("@pwd", logincls.pwd);
                    con.Open();
                    int uid = 0;
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        uid = Convert.ToInt32(dr["Reg_Id"]);

                    }
                con.Close();
                return uid;
                


            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }
        }

        public string Logintype(Logincls logincls)
        {


            try
            {
                
                    SqlCommand cmd1 = new SqlCommand("sp_logintype", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@uid", logincls.uname);
                    cmd1.Parameters.AddWithValue("@pwd", logincls.pwd);
                    con.Open();
                    string ltype = "";
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                     ltype = dr["UserType"].ToString();

                    }
                con.Close();
                return ltype;
                }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }
        }

        public List<Carcls> SelectAllDetails()
        {

            var list=new List<Carcls>();
            try
            {

                SqlCommand cmd1 = new SqlCommand("sp_displayall", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                
                con.Open();
               
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    var o = new Carcls
                    {
                        Id = Convert.ToInt32(dr["Car_Id"]),
                        brand = dr["Brand"].ToString(),
                        model = dr["Model"].ToString(),
                        year = dr["Year"].ToString(),
                        color = dr["Color"].ToString(),
                        numberplate = dr["NumberPlate"].ToString(),
                        engine = dr["Engine"].ToString(),
                        fuel = dr["FuelType"].ToString(),
                        image = dr["Image"].ToString(),
                        seatcapacity = dr["SeatingCapacity"].ToString(),

                    };
                    list.Add(o);

                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }
        }
        public Carcls getcardetails(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getcardetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@carid", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Carcls car = null;
                while (dr.Read())
                {
                    car = new Carcls
                    {
                        Id = Convert.ToInt32(dr["Car_Id"]),
                        brand = dr["Brand"].ToString(),
                        model = dr["Model"].ToString(),
                        image = dr["Image"].ToString()
                    };

                }
                con.Close();
                return car;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }
        }
        public string RentInsert(Rentcls rentcls)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("sp_rentinsert", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@pickdate", rentcls.pickup);
                cmd1.Parameters.AddWithValue("@dropdate", rentcls.dropoff);
                cmd1.Parameters.AddWithValue("@totamt", rentcls.totalamt);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                return "inserted";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                throw ex;
            }
        }
    }
}
