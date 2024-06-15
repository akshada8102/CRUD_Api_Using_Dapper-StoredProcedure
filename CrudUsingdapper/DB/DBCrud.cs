using CrudUsingdapper.IService;
using CrudUsingdapper.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CrudUsingdapper.DB
{
    public class DBCrud :ICrudService
    {
        String connection;
        public void connectionString(String conn)
        {
            connection = conn;
        }
        public string Delete(int Idd)
        {
            String result="";
            try
            {
                using (IDbConnection con = new SqlConnection(connection))
                {
                    String type = "D";
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        var Ocrud = con.Query<ModelCrud>("proc_Register", this.SetType(type, Idd),
                        commandType: CommandType.StoredProcedure);
                        result = "Deleted Successfully";
                    }

                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public ModelCrud Get(int Idd)
        {

            ModelCrud crudd = new ModelCrud();
            try
            {
                using (IDbConnection con = new SqlConnection(connection))
                {
                    String type = "S";
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        var Ocrud = con.Query<ModelCrud>("proc_Register", this.SetType(type, Idd),
                        commandType: CommandType.StoredProcedure);
                        if (Ocrud != null && Ocrud.Count() > 0)
                        {
                            crudd = Ocrud.FirstOrDefault();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                crudd.Message = ex.Message;
            }
            return crudd;
        }

        public List<ModelCrud> Gets()
        {
            List<ModelCrud> crudd = new List<ModelCrud>();
            try
            {
                using (IDbConnection con = new SqlConnection(connection))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        var Ocrud = con.Query<ModelCrud>("Select * from Register where isnull(isDeleted,'0')=0;").ToList();
                        if (Ocrud != null && Ocrud.Count() > 0)
                        {
                            crudd = Ocrud;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                //crudd.Message = ex.Message;
            }
            return crudd;
        }

        public ModelCrud Post(ModelCrud crud, String connectionString)
        {
            ModelCrud crudd = new ModelCrud();
            try
            {
                String type = "I";
                using (IDbConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        var Ocrud = con.Query<ModelCrud>("proc_Register", this.SetParameters(crud,type),
                        commandType: CommandType.StoredProcedure);
                        Type objectType = Ocrud.GetType();
                        Console.WriteLine("Return type of object: " + objectType);
                        if (Ocrud != null && Ocrud.Count() > 0)
                        {
                            crudd = Ocrud.FirstOrDefault();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                crudd.Message = ex.Message;
            }
            return crudd;
        }

        public ModelCrud Update(ModelCrud crud)
        {
            ModelCrud crudd = new ModelCrud();
            try
            {
                using (IDbConnection con = new SqlConnection(connection))
                {
                    String type = "U";
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        var Ocrud = con.Query<ModelCrud>("proc_Register", this.SetParameters(crud, type),
                        commandType: CommandType.StoredProcedure);
                        if (Ocrud != null && Ocrud.Count() > 0)
                        {
                            crudd = Ocrud.FirstOrDefault();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                crudd.Message = ex.Message;
            }
            return crudd;
        }
        public DynamicParameters SetParameters(ModelCrud crud, string type)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Name", crud.Name);
            parameter.Add("@Mobile_No", crud.Mobile_No);
            parameter.Add("@Email", crud.Email);
            parameter.Add("@Gender", crud.Gender);
            parameter.Add("@UserName", crud.UserName);
            parameter.Add("@Password", crud.Password);
            parameter.Add("@Id", crud.Id);
            parameter.Add("@UserType", crud.UserType);
            parameter.Add("@type", type);
            return parameter;
        }

        public DynamicParameters SetType( string type,int Id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@type", type);
            parameter.Add("@Id", Id);
            return parameter;
        }

        
    }
}
