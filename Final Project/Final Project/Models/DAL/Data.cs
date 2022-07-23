using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Collections.Specialized;
using System.Collections;


namespace Final_Project.Models.DAL
{
    public class Data
    {
        public List<Admin> ReadAdmin(string phone, string password)
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectAdmin(phone, password, con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                List<Admin> adminList = new List<Admin>();
                while (dr.Read())
                {
                    Admin admin = new Admin();
                    admin.Id = Convert.ToInt16(dr["id"]);
                    admin.Email = (string)dr["email"];
                    admin.Password = (string)dr["password"];
                    admin.First_name = (string)dr["fname"];
                    admin.Last_name = (string)dr["lname"];
                    admin.Role = (string)dr["position"];
                    admin.Phone = (string)dr["phone"];
                    adminList.Add(admin);
                }

                dr.Close();
                return adminList;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an Admin", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//login function

        public List<Admin> ReadAllAdmins()
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectAllAdmins(con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                List<Admin> adminList = new List<Admin>();
                while (dr.Read())
                {
                    Admin admin = new Admin();
                    
                    admin.Email = (string)dr["email"];
                    admin.Password = (string)dr["password"];
                    admin.First_name = (string)dr["fname"];
                    admin.Last_name = (string)dr["lname"];
                    admin.Role = (string)dr["position"];
                    admin.Phone = (string)dr["phone"];
                    adminList.Add(admin);
                }

                dr.Close();
                return adminList;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an Admin", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//admins list to data table

        public int InserAdmin(Admin admin)
        {
            SqlConnection con = null;
            int numEffected = 0;


            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertAdmin(admin, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();
            }


            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to insert an Admin", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            return numEffected;

        }//add new admin to DB


        private SqlCommand CreateSelectAdmin(string phone, string password, SqlConnection con)
        {

            string commandStr = "SELECT * FROM admin_2022 WHERE phone LIKE @phone AND password LIKE @password AND status=1";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@password", password);
            return cmd;
        }//LogIn Query

        private SqlCommand CreateSelectAllAdmins(SqlConnection con)
        {

            string commandStr = "SELECT * FROM admin_2022 WHERE status=1";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//admins list to data table query

        SqlCommand CreateInsertAdmin(Admin admin, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO admin_2022 ([fname],[lname],[phone],[position],[email],[password],[status]) VALUES (@FN,@LN,@PH,@POS,@EM,@PS,@ST)", con);
            cmd.Parameters.Add("@FN", System.Data.SqlDbType.VarChar).Value = admin.First_name;
            cmd.Parameters.Add("@LN", System.Data.SqlDbType.VarChar).Value = admin.Last_name;
            cmd.Parameters.Add("@PH", System.Data.SqlDbType.VarChar).Value = admin.Phone;
            cmd.Parameters.Add("@POS", System.Data.SqlDbType.VarChar).Value = admin.Role;
            cmd.Parameters.Add("@EM", System.Data.SqlDbType.VarChar).Value = admin.Email;
            cmd.Parameters.Add("@PS", System.Data.SqlDbType.VarChar).Value = admin.Password;
            cmd.Parameters.Add("@ST", System.Data.SqlDbType.SmallInt).Value = 1;
            return cmd;
        }//insert admin query

        SqlConnection Connect(string connectionStringName)
        {

            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        } //general
        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {

            SqlCommand cmd = new SqlCommand();  // create the command object
            cmd.Connection = con;               // assign the connection to the command object
            cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 60; // seconds

            return cmd;
        }//general

        public List<User> ReadAllUsers() //users to datatable
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectAllUsers(con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                List<User> userList = new List<User>();
                while (dr.Read())
                {
                    User user = new User();

                    user.Name = (string)dr["name"];
                    user.Phone = (string)dr["phone"];
                    user.Bdate = Convert.ToString(dr["bdate"]);
                    user.Signdate = Convert.ToString(dr["signdate"]);
                    user.Age = Convert.ToString(dr["age"]);
                    userList.Add(user);
                }

                dr.Close();
                return userList;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an User", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        private SqlCommand CreateSelectAllUsers(SqlConnection con)
        {

            string commandStr = "SELECT * ,DATEDIFF(YEAR, bdate,GETDATE() ) as age FROM patient_2022 WHERE status=1";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        } //users data table query generator

        public string UpdateStatus(Admin a, int status)
        {
            SqlConnection con = null;
            int numEffected = 0;
            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateUpdateUserAdmin(a, status, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update status", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            // num effected
            return "row updated to 0";

        }//update admin status >> delete admin 
        SqlCommand CreateUpdateUserAdmin(Admin a, int status, SqlConnection con)  //create command update status >> delete admin
        {
            SqlCommand cmd = new SqlCommand("UPDATE admin_2022 SET status=@ST WHERE email=@EM", con);
            cmd.Parameters.Add("@EM", System.Data.SqlDbType.VarChar).Value = a.Email;
            cmd.Parameters.Add("@ST", System.Data.SqlDbType.VarChar).Value = status;
            return cmd;
        }

        public string UpdateData(Admin a) //update admin data > store proc
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                using (SqlCommand cmd = new SqlCommand("editAdmin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", a.Id);
                    cmd.Parameters.AddWithValue("@fn", a.First_name);
                    cmd.Parameters.AddWithValue("@ln", a.Last_name);
                    cmd.Parameters.AddWithValue("@ph", a.Phone);
                    cmd.Parameters.AddWithValue("@ro", a.Role);
                    cmd.Parameters.AddWithValue("@em", a.Email);

                    //E Execute
                    cmd.ExecuteNonQuery();
                }

            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update admin data", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }



            return "success";
        }

        public string UpdatePass(string pass, string phone) //update admin pass > store proc
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                using (SqlCommand cmd = new SqlCommand("editAdminPass", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ps", pass);
                    cmd.Parameters.AddWithValue("@ph", phone);

                    //E Execute
                    cmd.ExecuteNonQuery();
                }

            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update admin password", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }



            return "success";
        }
        public string UpdateUserStatus(User u, int status)
        {
            SqlConnection con = null;
            int numEffected = 0;
            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateUpdateUser(u, status, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update status", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            // num effected
            return "row updated to 0";

        }//update user status >> delete user 

        SqlCommand CreateUpdateUser(User u, int status, SqlConnection con)  //create command update status >> delete user
        {
            SqlCommand cmd = new SqlCommand("UPDATE patient_2022 SET status=@ST WHERE phone=@PH", con);
            cmd.Parameters.Add("@PH", System.Data.SqlDbType.VarChar).Value = u.Phone;
            cmd.Parameters.Add("@ST", System.Data.SqlDbType.VarChar).Value = status;
            return cmd;
        }

        public List<Right> ReadAllRights()
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");
                
                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectAllRights(con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                List<Right> rightList = new List<Right>();
                while (dr.Read())
                {
                    Right right = new Right();
                    right.Name = (string)dr["name"];
                    right.Gender = (string)dr["gender"];
                    right.Id = Convert.ToInt16(dr["id"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("st1")))
                        right.St1 = (string)dr["st1"];
                    if (!dr.IsDBNull(dr.GetOrdinal("st3")))
                        right.St3 = (string)dr["st3"];
                    if (!dr.IsDBNull(dr.GetOrdinal("st2")))
                        right.St2 = (string)dr["st2"];
                    right.Description = (string)dr["description"];
                    right.Source = (string)dr["source"];
                    if (!dr.IsDBNull(dr.GetOrdinal("ac_name")))
                        right.Accident_type = (string)dr["ac_name"];
                    if (!dr.IsDBNull(dr.GetOrdinal("min_disaP")))
                        right.Min_disap = Convert.ToInt16(dr["min_disaP"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("max_disaP")))
                        right.Max_disap = Convert.ToInt16(dr["max_disaP"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("minInc")))
                        right.MinInc = Convert.ToInt16(dr["minInc"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("maxInc")))
                        right.MaxInc = Convert.ToInt16(dr["maxInc"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("workDis")))
                        right.Workdis = Convert.ToInt16(dr["workDis"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("minAge")))
                        right.MinAge = Convert.ToInt16(dr["minAge"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("maxAge")))
                        right.MaxAge = Convert.ToInt16(dr["maxAge"]);
                    right.CzUrl = (string)dr["czUrl"];
                    right.InsertAdmin = (string)dr["fname"] + " " + (string)dr["lname"];
                    right.Date = Convert.ToString(dr["date"]);
                    ListDictionary rightmedstat = new ListDictionary();
                    rightmedstat = ReadRightMedStat(right.Id);
                    right.Medstatwithstring = rightmedstat;

                    //copy to int array med_stat
                    ICollection ic = rightmedstat.Keys;
                    int[] array = new int[ic.Count];
                    ic.CopyTo(array, 0);
                    
                    right.Medstat = array;
                    rightList.Add(right);
                }

                dr.Close();
                return rightList;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an Right", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//Rights list to data table

        public ListDictionary ReadRightMedStat(int id)
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectMedStat(id, con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                ListDictionary rightmedstat = new ListDictionary();
                while (dr.Read())
                {
                    int msid = Convert.ToInt16(dr["msid"]);
                    string s = (string)dr["status"];
                    rightmedstat.Add(msid,s);
                }

                dr.Close();
                return rightmedstat;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an medical statuses for the right", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//medical status of right reader

        private SqlCommand CreateSelectMedStat(int id, SqlConnection con)
        {

            string commandStr = "select msid,[status] from rightStatus_2022 as r inner join medicalStatus_2022 as m on r.msid=m.id WHERE rid=@id";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        } //medical status of right Query

        public int ReadcountRights()
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectCountRights(con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();

                dr.Close();
                return Convert.ToInt16(dr["c"]);
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an Right", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//Rights count to index >>> TO BE DELETED


        private SqlCommand CreateSelectAllRights(SqlConnection con)
        {

            string commandStr = "SELECT r.*, ac.name as ac_name,ad.fname,ad.lname FROM rights_2022 as r left join accident_2022 as ac on r.acc_type = ac.id inner join admin_2022 as ad on ad.id = r.adminId where r.status = 1";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//select all active rights query

        private SqlCommand CreateSelectCountRights(SqlConnection con)
        {

            string commandStr = "SELECT count(*) as c FROM rights_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//select count rights for index page

        public string UpdateRightStatus(Right r, int status)
        {
            SqlConnection con = null;
            int numEffected = 0;
            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateUpdateRight(r, status, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
               
                throw new Exception("Failed to update status", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            // num effected
            return "row updated to 0";

        }//update right status >> delete right 

        SqlCommand CreateUpdateRight(Right r, int status, SqlConnection con)  //create command update status >> delete right
        {
            SqlCommand cmd = new SqlCommand("UPDATE rights_2022 SET status=@ST WHERE id=@ID", con);
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = r.Id;
            cmd.Parameters.Add("@ST", System.Data.SqlDbType.VarChar).Value = status;
            return cmd;
        }

        public int InsertUser(User user)
        {
            SqlConnection con = null;
            int numEffected = 0;


            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertUser(user, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();
            }


            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to insert an User", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            return numEffected;

        }//add new User to DB

        SqlCommand CreateInsertUser(User user, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO patient_2022 ([name],[phone],[password],[status],[signdate],[bdate]) VALUES (@FN,@PH,@PS,@ST,@TS,@BD)", con);
            cmd.Parameters.Add("@FN", System.Data.SqlDbType.VarChar).Value = user.Name;
            cmd.Parameters.Add("@PH", System.Data.SqlDbType.VarChar).Value = user.Phone;
            cmd.Parameters.Add("@PS", System.Data.SqlDbType.VarChar).Value = user.Password;
            cmd.Parameters.Add("@ST", System.Data.SqlDbType.SmallInt).Value = 1;
            cmd.Parameters.Add("@TS", System.Data.SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cmd.Parameters.Add("@BD", System.Data.SqlDbType.Date).Value = user.Bdate;
            return cmd;
        }//insert user query



        public object InsertRight(Right right) 
        {
            int temp=Int16.Parse(right.InsertAdmin);
            SqlConnection con = null;
            object inserted_id = 0;


            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertRight(right, con);

                //E Execute
                inserted_id = insertCommand.ExecuteScalar();
                
            }


            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to insert an Right", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }

            int[] medStat = right.Medstat;
            for (int i = 0; i < medStat.Length; i++)
            {
                InsertRightMedStatus(medStat[i], inserted_id);
            }

            return inserted_id;

        }//add new Right to DB

        SqlCommand CreateInsertRight(Right right, SqlConnection con) 
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO rights_2022 ([name],[st1],[st2],[st3],[description],[source],[acc_type],[gender],[residence],[fstatus],[min_disaP],[max_disaP],[minInc],[maxInc],[workDis],[minAge],[maxAge],[czUrl],[date],[adminId],[status]) VALUES (@FN,@STA,@STB,@STC,@DS,@SR,@AT,@GN,@RES,@FST,@MIP,@MAP,@MIC,@MAC,@WD,@MIA,@MAA,@CZ,@TS,@AD,@ST) SELECT SCOPE_IDENTITY()", con);
            cmd.Parameters.Add("@FN", System.Data.SqlDbType.VarChar).Value = right.Name;
            cmd.Parameters.Add("@STA", System.Data.SqlDbType.VarChar).Value = right.St1;
            cmd.Parameters.Add("@STB", System.Data.SqlDbType.VarChar).Value = right.St2;
            cmd.Parameters.Add("@STC", System.Data.SqlDbType.VarChar).Value = right.St3;
            cmd.Parameters.Add("@DS", System.Data.SqlDbType.VarChar).Value = right.Description;
            cmd.Parameters.Add("@SR", System.Data.SqlDbType.VarChar).Value = right.Source;
            cmd.Parameters.Add("@AT", System.Data.SqlDbType.SmallInt).Value = Int16.Parse(right.Accident_type);
            cmd.Parameters.Add("@GN", System.Data.SqlDbType.VarChar).Value = right.Gender;
            cmd.Parameters.Add("@RES", System.Data.SqlDbType.SmallInt).Value = right.Residance;
            cmd.Parameters.Add("@FST", System.Data.SqlDbType.SmallInt).Value = right.Fstatus;
            cmd.Parameters.Add("@MIP", System.Data.SqlDbType.SmallInt).Value = right.Min_disap;
            cmd.Parameters.Add("@MAP", System.Data.SqlDbType.SmallInt).Value = right.Max_disap;
            cmd.Parameters.Add("@MIC", System.Data.SqlDbType.Int).Value = right.MinInc;
            cmd.Parameters.Add("@MAC", System.Data.SqlDbType.Int).Value = right.MaxInc;
            cmd.Parameters.Add("@WD", System.Data.SqlDbType.SmallInt).Value = right.Workdis;
            cmd.Parameters.Add("@MIA", System.Data.SqlDbType.SmallInt).Value = right.MinAge;
            cmd.Parameters.Add("@MAA", System.Data.SqlDbType.SmallInt).Value = right.MaxAge;
            cmd.Parameters.Add("@CZ", System.Data.SqlDbType.VarChar).Value = right.CzUrl;
            cmd.Parameters.Add("@AD", System.Data.SqlDbType.SmallInt).Value = Int16.Parse(right.InsertAdmin);
            cmd.Parameters.Add("@ST", System.Data.SqlDbType.SmallInt).Value = 1;
            cmd.Parameters.Add("@TS", System.Data.SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            return cmd;
        }//insert right query

        public void InsertRightMedStatus(int msid,object rid)
        {
            SqlConnection con = null;
            int numEffected = 0;

            

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertRightMedStatus(msid,rid, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();

            }


            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to insert an RightMedStat", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }




        }//add new Right Med Ststus to DB

        SqlCommand CreateInsertRightMedStatus(int msid,object rid, SqlConnection con)//NOT FINALEIZE
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO rightStatus_2022 ([msid],[rid]) VALUES (@MSID,@RID)", con);
            cmd.Parameters.Add("@RID", System.Data.SqlDbType.SmallInt).Value = rid;
            cmd.Parameters.Add("@MSID", System.Data.SqlDbType.SmallInt).Value = msid;


            return cmd;
        }//insert right med status query

        public string UpdateRightData(Right r)
        {
            SqlConnection con = null;
            int numEffected = 0;
            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand updateCommand = CreateUpdateRightData(r, con);

                //E Execute
                numEffected = updateCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                throw new Exception("Failed to update right data", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            // num effected

            InsertRedit(r.Id, r.InsertAdmin);
            DeleteMedStat(r.Id);
            int[] medStat = r.Medstat;
            for (int i = 0; i < medStat.Length; i++)
            {
                InsertRightMedStatus(medStat[i], r.Id);
            }

            return "row updated";

        }//update right data 

        SqlCommand CreateUpdateRightData(Right right, SqlConnection con)  //create command update data >> edit right page
        {
            SqlCommand cmd = new SqlCommand("update rights_2022 set [name]=@FN,[st1]=@STA,[st2]=@STB,[st3]=@STC,[description]=@DS,[source]=@SR,[acc_type]=@AT,[gender]=@GN,[residence]=@RES,[min_disaP]=@MIP,[max_disaP]=@MAP,[minInc]=@MIC,[maxInc]=@MAC,[workDis]=@WD,[minAge]=@MIA,[maxAge]=@MAA,[czUrl]=@CZ WHERE id=@ID", con);
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = right.Id;
            cmd.Parameters.Add("@FN", System.Data.SqlDbType.VarChar).Value = right.Name;
            cmd.Parameters.Add("@STA", System.Data.SqlDbType.VarChar).Value = right.St1;
            cmd.Parameters.Add("@STB", System.Data.SqlDbType.VarChar).Value = right.St2;
            cmd.Parameters.Add("@STC", System.Data.SqlDbType.VarChar).Value = right.St3;
            cmd.Parameters.Add("@DS", System.Data.SqlDbType.VarChar).Value = right.Description;
            cmd.Parameters.Add("@SR", System.Data.SqlDbType.VarChar).Value = right.Source;
            cmd.Parameters.Add("@AT", System.Data.SqlDbType.SmallInt).Value = Int16.Parse(right.Accident_type);
            cmd.Parameters.Add("@GN", System.Data.SqlDbType.VarChar).Value = right.Gender;
            cmd.Parameters.Add("@RES", System.Data.SqlDbType.SmallInt).Value = right.Residance;
            //cmd.Parameters.Add("@FST", System.Data.SqlDbType.SmallInt).Value = right.Fstatus;
            cmd.Parameters.Add("@MIP", System.Data.SqlDbType.SmallInt).Value = right.Min_disap;
            cmd.Parameters.Add("@MAP", System.Data.SqlDbType.SmallInt).Value = right.Max_disap;
            cmd.Parameters.Add("@MIC", System.Data.SqlDbType.Int).Value = right.MinInc;
            cmd.Parameters.Add("@MAC", System.Data.SqlDbType.Int).Value = right.MaxInc;
            cmd.Parameters.Add("@WD", System.Data.SqlDbType.SmallInt).Value = right.Workdis;
            cmd.Parameters.Add("@MIA", System.Data.SqlDbType.SmallInt).Value = right.MinAge;
            cmd.Parameters.Add("@MAA", System.Data.SqlDbType.SmallInt).Value = right.MaxAge;
            cmd.Parameters.Add("@CZ", System.Data.SqlDbType.VarChar).Value = right.CzUrl;
            return cmd;
        }

        public int InsertRedit(int rid, string aid)
        {
            SqlConnection con = null;
            int numEffected = 0;


            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateInsertRedits(rid, aid, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();
            }


            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to insert a redits record", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            return numEffected;

        }//add new row to redits table to DB

        SqlCommand CreateInsertRedits(int rid, string aid, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO redits_2022 ([aid],[rid],[date]) VALUES (@AD,@ID,@TS)", con);
            cmd.Parameters.Add("@TS", System.Data.SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cmd.Parameters.Add("@AD", System.Data.SqlDbType.SmallInt).Value = Int16.Parse(aid);
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = rid;

            return cmd;
        }//insert redits query

        public string DeleteMedStat(int id) //delete med status for right > store proc
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                using (SqlCommand cmd = new SqlCommand("deleteMedStat", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    //E Execute
                    cmd.ExecuteNonQuery();
                }

            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update admin data", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }



            return "success";
        }

        ///////////////////////////////////////////////////////// index

        public DashBoard ReadDashBoard()
        {
            SqlConnection con = null;
            DashBoard dash = new DashBoard();

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //Count patients
                SqlCommand selectCommand = CreateSelectDashBoard0(con);

                //Count patients
                SqlDataReader dr0 = selectCommand.ExecuteReader();
                while (dr0.Read())
                {
                    dash.Patient = Convert.ToInt32(dr0["cp"]);
                }
                dr0.Close();

                //Count rights
                SqlCommand selectCommand1 = CreateSelectDashBoard1(con);

                //Count rights
                SqlDataReader dr1 = selectCommand1.ExecuteReader();
                while (dr1.Read())
                {
                    dash.Rights = Convert.ToInt16(dr1["cr"]);
                }
                dr1.Close();

                //LOG
                SqlCommand selectCommand2 = CreateSelectDashBoard2(con);

                //LOG
                SqlDataReader dr2 = selectCommand2.ExecuteReader();

                List<string> dashList = new List<string>();
                List<int> times = new List<int>();

                while (dr2.Read())
                {
                    dashList.Add((string)dr2["st"]);
                    times.Add(Convert.ToInt16(dr2["t"]));
                }
                dash.Log = dashList;
                dash.Times = times;
                dr2.Close();

                //executed rights
                SqlCommand selectCommand3 = CreateSelectDashBoard3(con);

                //executed rights
                SqlDataReader dr3 = selectCommand3.ExecuteReader();
                int temp=0;
                while (dr3.Read())
                {
                   temp = Convert.ToInt16(dr3["prs"]);
                }
                
                dr3.Close();

                //count each source
                SqlCommand selectCommand4 = CreateSelectDashBoard4(con);

                //count each source
                SqlDataReader dr4 = selectCommand4.ExecuteReader();
                List<int> quantityList = new List<int>();
                List<string> sourceList = new List<string>();
                while (dr4.Read())
                {
                    quantityList.Add(Convert.ToInt16(dr4["num"]));
                    sourceList.Add((string)dr4["source"]);
                }
                dash.Quantity = quantityList;
                dash.SourceList = sourceList;
                dr4.Close();

                //count new patient for month
                SqlCommand selectCommand5 = CreateSelectDashBoard5(con);

                //count new patient for month
                SqlDataReader dr5 = selectCommand5.ExecuteReader();



                List<int> monthsList = new List<int>();
                List<string> monthNameList = new List<string>();
                while (dr5.Read())
                {
                    monthsList.Add(Convert.ToInt16(dr5["num"]));
                    monthNameList.Add((string)dr5["month"]);
                }
                dash.Userpermonth = monthsList;
                dash.MonthNameList = monthNameList;

                dr5.Close();

                //count executed rights
                SqlCommand selectCommand6 = CreateSelectDashBoard6(con);

                //count executed rights
                SqlDataReader dr6 = selectCommand6.ExecuteReader();

                while (dr6.Read())
                {
                    dash.Exrights = Convert.ToInt16(dr6["c"]);
                }
                dash.Percent = (dash.Exrights *100/ temp);
                return dash;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an DashBoard", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//Get Data for visualizations - index page

        private SqlCommand CreateSelectDashBoard0(SqlConnection con)
        {

            string commandStr = "SELECT COUNT(id) as cp FROM patient_2022 WHERE status=1";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//Count patients
        private SqlCommand CreateSelectDashBoard3(SqlConnection con)
        {

            string commandStr = "SELECT COUNT(rid) as prs FROM patientRight_2022"; //saved rights = 7
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//Count executed rights

        private SqlCommand CreateSelectDashBoard6(SqlConnection con)
        {

            string commandStr = "SELECT COUNT(rid) as c from patientRight_2022 where [status]=3"; //saved rights = 7
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//Count executed rights
        private SqlCommand CreateSelectDashBoard1(SqlConnection con)
        {

            string commandStr = "SELECT COUNT(id) as cr FROM rights_2022 WHERE status=1"; //all rights
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//Count rights
        private SqlCommand CreateSelectDashBoard2(SqlConnection con)
        {
            string commandStr = "select top 6 *, DATEDIFF(HOUR,timestamp,GETDATE()) AS t,[table]+' '+[val] +' '+ [action] as st from log_2022 order by timestamp DESC"; //למשוך לפי תאריך אחרון 
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//Data for last activities
        private SqlCommand CreateSelectDashBoard4(SqlConnection con)
        {
            string commandStr = "SELECT COUNT(*) AS num, [source] FROM rights_2022 WHERE [status]=1 GROUP BY [source]";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//count the number of rights from each source
        private SqlCommand CreateSelectDashBoard5(SqlConnection con)
        {
            string commandStr = "SELECT COUNT(*) AS num, DATENAME(month,signdate) as [month] FROM patient_2022  where [status]=1 GROUP BY DATENAME(month,signdate) ORDER BY [month] DESC";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }//count the number of new patients for each month 


        //////////////////////////////////////// USER SIDE /////////////////////////

        public List<User> ReadUser(string phone, string password)
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectUser(phone, password, con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                List<User> userList = new List<User>();
                while (dr.Read())
                {
                    User user = new User();
                    user.Id = Convert.ToInt16(dr["id"]);
                    user.Name = (string)dr["name"];
                    user.Phone = (string)dr["phone"];
                    user.Password = (string)dr["password"];
                    if (!dr.IsDBNull(dr.GetOrdinal("gender")))
                        user.Gender = Convert.ToInt16(dr["gender"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("fstatus")))
                        user.FamilyStatus = (string)dr["fstatus"];
                    if (!dr.IsDBNull(dr.GetOrdinal("disaP")))
                        user.DisP = Convert.ToInt16(dr["disaP"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("income")))
                        user.Income = Convert.ToInt16(dr["income"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("residence")))
                        user.Isres = Convert.ToInt16(dr["residence"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("workDis")))
                        user.WorkDis = Convert.ToInt16(dr["workDis"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("aid")))
                        user.AccId = Convert.ToInt16(dr["aid"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("age")))
                        user.Age = Convert.ToString(dr["age"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("bdate")))
                        user.Bdate = Convert.ToString(dr["bdate"]);
                    user.Signdate = Convert.ToString(dr["signdate"]);
                    userList.Add(user);
                }

                dr.Close();
                return userList;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find a User", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//login function

        private SqlCommand CreateSelectUser(string phone, string password, SqlConnection con)
        {

            string commandStr = "SELECT *,DATEDIFF(YEAR, bdate,GETDATE() ) as age FROM patient_2022 WHERE phone LIKE @phone AND password LIKE @password AND status=1";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@password", password);
            return cmd;
        }//LogIn Query

        //////////////////////////DashBoard USER SIDE////////////////////////////

        public DashBoard ReadDashBoard(int id)
        {
            SqlConnection con = null;
            DashBoard dash = new DashBoard();

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //Count rights
                SqlCommand selectCommand1 = CreateSelectDashBoard1(con, id);

                //Count rights
                SqlDataReader dr1 = selectCommand1.ExecuteReader();
                while (dr1.Read())
                {
                    dash.Rights = Convert.ToInt16(dr1["cr"]);
                }
                dr1.Close();

                //LOG
                SqlCommand selectCommand2 = CreateSelectDashBoard2(con, id);

                //LOG
                SqlDataReader dr2 = selectCommand2.ExecuteReader();

                List<string> dashList = new List<string>();
                List<int> times = new List<int>();

                while (dr2.Read())
                {
                    dashList.Add((string)dr2["st"]);
                    times.Add(Convert.ToInt16(dr2["t"]));
                }
                dash.Log = dashList;
                dash.Times = times;
                dr2.Close();

                //executed rights
                SqlCommand selectCommand3 = CreateSelectDashBoard3(con, id);

                //executed rights
                SqlDataReader dr3 = selectCommand3.ExecuteReader();
                while (dr3.Read())
                {
                    dash.Exrights = Convert.ToInt16(dr3["prs"]);
                }
                if (dash.Exrights==0|| dash.Rights==0)
                {
                    dash.Percent = 0;
                }
                else
                    dash.Percent = dash.Exrights / dash.Rights;

                dr3.Close();

                //count each source
                SqlCommand selectCommand4 = CreateSelectDashBoard4(con, id);

                //count each source
                SqlDataReader dr4 = selectCommand4.ExecuteReader();
                List<int> quantityList = new List<int>();
                List<string> levelList = new List<string>();
                while (dr4.Read())
                {
                    quantityList.Add(Convert.ToInt16(dr4["num"]));
                   int temp = Convert.ToInt16(dr4["status"]); //לדאוג בדף אינטרנט להפוך מ1,2,3 לראיתי בתהליך מימשתי
                    string status = "";
                    switch (temp)
                    {
                        case 1:
                            status = "ראיתי";
                            break;
                        case 2:
                            status = "בתהליך";
                            break;
                        default:
                            status = "מימשתי";
                            break;
                    }
                    levelList.Add(status);
                }
                dash.Quantity = quantityList;
                dash.SourceList = levelList;
                dr4.Close();

                return dash;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find a DashBoard", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//Get Data for visualizations - index page
        private SqlCommand CreateSelectDashBoard1(SqlConnection con, int id)
        {

            string commandStr = "SELECT COUNT(pid) as cr FROM patientRight_2022 WHERE pid=@id";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        }//Count rights for patient

        private SqlCommand CreateSelectDashBoard3(SqlConnection con, int id)
        {

            string commandStr = "SELECT COUNT(pid) as prs FROM patientRight_2022 WHERE [status]=3 AND pid=@id";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        }//Count executed rights

        private SqlCommand CreateSelectDashBoard2(SqlConnection con, int id)
        {
            string commandStr = "select top 5 pr.*, DATEDIFF(HOUR,pr.[update],GETDATE()) AS t,r.[name] as st from patientRight_2022 pr left join [Rights_2022] r ON pr.rid=r.id WHERE pr.pid=@id order by pr.[update] DESC";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        }//Data for last saved rights
        private SqlCommand CreateSelectDashBoard4(SqlConnection con, int id)
        {
            string commandStr = "select status,count(*) as num FROM patientRight_2022 WHERE pid=@id GROUP BY status‏";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        }//count the number of saved rights for each level

       // תוספות 4.5///////////////////////////////////////////////////////////////////////////

        public string UpdateUserPass(string pass, string phone) //update user pass > store proc
        {
            SqlConnection con = null;
            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                using (SqlCommand cmd = new SqlCommand("editUserPass", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ps", pass);
                    cmd.Parameters.AddWithValue("@ph", phone);

                    //E Execute
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update user password", ex);
            }
            finally
            {
                //C Close Connction
                con.Close();
            }
            return "success";
        }

        ///////////////////////////14.5//////////////////////
        ///
        public List<Right> ReadRights4User(int id)
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectRights4User(con,id);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                List<Right> rightList = new List<Right>();
                while (dr.Read())
                {
                    Right right = new Right();
                    right.Name = (string)dr["name"];
                    right.Gender = (string)dr["gender"];
                    right.Id = Convert.ToInt16(dr["id"]);
                    right.Rstatus = Convert.ToInt16(dr["rstatus"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("st1")))
                        right.St1 = (string)dr["st1"];
                    if (!dr.IsDBNull(dr.GetOrdinal("st3")))
                        right.St3 = (string)dr["st3"];
                    if (!dr.IsDBNull(dr.GetOrdinal("st2")))
                        right.St2 = (string)dr["st2"];
                    right.Description = (string)dr["description"];
                    right.Source = (string)dr["source"];
                    if (!dr.IsDBNull(dr.GetOrdinal("ac_name")))
                        right.Accident_type = (string)dr["ac_name"];
                    if (!dr.IsDBNull(dr.GetOrdinal("min_disaP")))
                        right.Min_disap = Convert.ToInt16(dr["min_disaP"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("max_disaP")))
                        right.Max_disap = Convert.ToInt16(dr["max_disaP"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("minInc")))
                        right.MinInc = Convert.ToInt16(dr["minInc"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("maxInc")))
                        right.MaxInc = Convert.ToInt16(dr["maxInc"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("workDis")))
                        right.Workdis = Convert.ToInt16(dr["workDis"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("minAge")))
                        right.MinAge = Convert.ToInt16(dr["minAge"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("maxAge")))
                        right.MaxAge = Convert.ToInt16(dr["maxAge"]);
                    right.CzUrl = (string)dr["czUrl"];
                    //right.InsertAdmin = (string)dr["fname"] + " " + (string)dr["lname"];
                    right.Date = Convert.ToString(dr["date"]);
                    ListDictionary rightmedstat = new ListDictionary();
                    rightmedstat = ReadRightMedStat(right.Id);
                    right.Medstatwithstring = rightmedstat;

                    //copy to int array med_stat
                    ICollection ic = rightmedstat.Keys;
                    int[] array = new int[ic.Count];
                    ic.CopyTo(array, 0);

                    right.Medstat = array;
                    rightList.Add(right);
                }

                dr.Close();
                return rightList;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find an Right", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        private SqlCommand CreateSelectRights4User(SqlConnection con,int id)
        {

            string commandStr = "SELECT r.*,pr.status as rstatus, ac.name as ac_name FROM rights_2022 as r left join accident_2022 as ac on r.acc_type = ac.id inner join patientRight_2022 as pr on pr.rid = r.id where pr.pid = @pid and r.status = 1";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@pid", id);
            return cmd;
        }
        ////////////////////////////////15.5///
        public int changeStatus(int uid, int rid, int status)
        {
            SqlConnection con = null;
            int numEffected = 0;
            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = CreateUpdateRightStatus(uid, rid, status, con);

                //E Execute
                numEffected = insertCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to update status", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }
            // num effected
            return 1;

        }//update right status 
        SqlCommand CreateUpdateRightStatus(int uid, int rid, int status, SqlConnection con)  //create command update status
        {
            string commandStr = "UPDATE patientRight_2022 SET status=@ST WHERE rid = @RID AND pid = @UID";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@RID", rid);
            cmd.Parameters.AddWithValue("@ST", status);
            return cmd;
        }

        /////Match between user deatails to Rights!
        ///

        public List<Right> ReadUserRights (User user)
        {
            List<Right> rightList = new List<Right>();
            string disp;
            string gen;
            string acc;
            string inc;
            string res;
            string age;
            string work = "AND (workDis=0 OR workDis="+user.WorkDis+") ";
            switch (user.Gender)
            {
                case 1:
                    gen = "AND (gender = 'שני המינים' OR gender ='זכר') ";
                    break;
                case 2:
                    gen = "AND (gender = 'שני המינים' OR gender ='נקבה') ";
                    break;
                case 3:
                    gen = "AND 1=1 ";
                    break;

                default: gen = "AND 1=1 ";
                    break;
            }

            switch (user.AccId)
            {
                case 1:
                    acc = "AND (acc_type=0 OR acc_type=1) ";
                    break;
                case 2:
                    acc = "AND (acc_type=0 OR acc_type=2) ";
                    break;
                case 3:
                    acc = "AND 1=1 ";
                    break;

                default:
                    acc = "AND 1=1 ";
                    break;
            }



            if (user.DisP!=-1)
            {
                disp = "AND " + user.DisP + " between min_disaP AND max_disaP ";

            }
            else
            {
                disp = "AND 1=1 ";
            }

            if (user.Isres != -1)
            {
                res = "AND residence=" + user.Isres+" ";
            }
            else
            {
                res = "AND 1=1 ";
            }

            if (user.Income != -1)
            {
                inc = "AND " + user.Income + " between minInc AND maxInc ";
            }
            else
            {
                inc = "AND 1=1 ";
            }

            if (user.Bdate != null)
            {
                age = "AND DATEDIFF(YEAR," + user.Bdate + ", GETDATE()) between minAge AND maxAge";
            }
            else
            {
                age = "AND 1=1";
            }



            string str = gen + acc + disp+ inc+res+work+age;

            rightList=RightMatch(str,user);


            return rightList;
        }

        public List<Right> RightMatch(string str, User user)
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                SqlCommand selectCommand = CreateSelectUserRight(str,user, con);

                //E Execute
                SqlDataReader dr = selectCommand.ExecuteReader();
                List<Right> rightList = new List<Right>(); /////
                while (dr.Read())
                {
                    Right right = new Right();
                    right.Name = (string)dr["name"];
                    right.Gender = (string)dr["gender"];
                    right.Id = Convert.ToInt16(dr["id"]);
                    right.Rstatus = Convert.ToInt16(dr["status"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("st1")))
                        right.St1 = (string)dr["st1"];
                    if (!dr.IsDBNull(dr.GetOrdinal("st3")))
                        right.St3 = (string)dr["st3"];
                    if (!dr.IsDBNull(dr.GetOrdinal("st2")))
                        right.St2 = (string)dr["st2"];
                    right.Description = (string)dr["description"];
                    right.Source = (string)dr["source"];
                    //if (!dr.IsDBNull(dr.GetOrdinal("ac_name")))
                    //    right.Accident_type = (string)dr["ac_name"];
                    if (!dr.IsDBNull(dr.GetOrdinal("min_disaP")))
                        right.Min_disap = Convert.ToInt16(dr["min_disaP"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("max_disaP")))
                        right.Max_disap = Convert.ToInt16(dr["max_disaP"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("minInc")))
                        right.MinInc = Convert.ToInt16(dr["minInc"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("maxInc")))
                        right.MaxInc = Convert.ToInt16(dr["maxInc"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("workDis")))
                        right.Workdis = Convert.ToInt16(dr["workDis"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("minAge")))
                        right.MinAge = Convert.ToInt16(dr["minAge"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("maxAge")))
                        right.MaxAge = Convert.ToInt16(dr["maxAge"]);
                    right.CzUrl = (string)dr["czUrl"];
                    //right.InsertAdmin = (string)dr["fname"] + " " + (string)dr["lname"];
                    //right.Date = Convert.ToString(dr["date"]);
                    rightList.Add(right);
                }

                dr.Close();
                return rightList;
            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to find a Right", ex);
            }

            finally
            {
                //C Close Connction
                if (con != null)
                {
                    con.Close();
                }

            }


        }//match function

        private SqlCommand CreateSelectUserRight(string str,User user, SqlConnection con)
        {

            string commandStr = "select * from rights_2022 where [status]=1 " + str;
            SqlCommand cmd = createCommand(con, commandStr);

            return cmd;
        }//rights after questions query

        //////4.6//////
        //public int addRighttoUser(int uid, int rid)
        //{
        //    SqlConnection con = null;
        //    int numEffected = 0;
        //    try
        //    {
        //        //C - Connect to the Database
        //        con = Connect("tar3DB");

        //        //C Create the Insert SqlCommand
        //        SqlCommand insertCommand = CreateAddRightToUser(uid, rid, con);

        //        //E Execute
        //        numEffected = insertCommand.ExecuteNonQuery();
        //    }

        //    catch (Exception ex)
        //    {
        //        // this code needs to write the error to a log file
        //        throw new Exception("Failed to add right", ex);
        //    }

        //    finally
        //    {
        //        //C Close Connction
        //        con.Close();
        //    }
        //    // num effected
        //    return 1;

        //}//update right status 
        //SqlCommand CreateAddRightToUser(int uid, int rid, SqlConnection con)  //create command add right
        //{
        //    //string commandStr = "INSERT INTO patientRight_2022 ([pid],[rid],[status],[update]) VALUES (@UID,@RID,1,getdate())‏";
        //    SqlCommand cmd = new SqlCommand("INSERT INTO patientRight_2022 ([pid],[rid],[status],[update]) VALUES (@UID,@RID,1,GETDATE())‏", con);
        //    //SqlCommand cmd = createCommand(con, commandStr);
        //    //cmd.Parameters.AddWithValue("@UID", uid);
        //    //cmd.Parameters.AddWithValue("@RID", rid);

        //    cmd.Parameters.Add("@UID", System.Data.SqlDbType.SmallInt).Value = uid;
        //    cmd.Parameters.Add("@RID", System.Data.SqlDbType.SmallInt).Value =rid;
        //    return cmd;
        //}


        public int addRighttoUser(int uid, int rid) //add right after form filled in > store proc
        {
            SqlConnection con = null;

            try
            {
                //C - Connect to the Database
                con = Connect("tar3DB");

                //C Create the Insert SqlCommand
                using (SqlCommand cmd = new SqlCommand("addRight", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UID", uid);
                    cmd.Parameters.AddWithValue("@RID", rid);

                    //E Execute
                    cmd.ExecuteNonQuery();
                }

            }

            catch (Exception ex)
            {
                // this code needs to write the error to a log file
                throw new Exception("Failed to insert right for patient", ex);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }



            return 1;
        }

    }

}