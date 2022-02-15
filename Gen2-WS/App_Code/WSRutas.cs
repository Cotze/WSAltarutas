using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de WSRutas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
//  [System.Web.Script.Services.ScriptService]
public class WSRutas : System.Web.Services.WebService
{

    public WSRutas()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int insDireccion(string Calle, string Numero, string Colonia, string Ciudad, string Estado, string CP)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        SqlCommand cmd = new SqlCommand("Direcciones_Insertar", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Calle", Calle);
        cmd.Parameters.AddWithValue("@Numero", Numero);
        cmd.Parameters.AddWithValue("@Colonia", Colonia);
        cmd.Parameters.AddWithValue("@Ciudad", Ciudad);
        cmd.Parameters.AddWithValue("@Estado", Estado);
        cmd.Parameters.AddWithValue("@CP", CP);
        conn.Open();
        int IdOrigenDestino = int.Parse(cmd.ExecuteScalar().ToString());
        conn.Close();
        return IdOrigenDestino;
    }

    [WebMethod]
    public string InsertarCargamento(long IdRuta, string Descripcion, double Peso)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ToString());

        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
        conn.Open();
        string Query = "Cargamento_Insertar";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType= CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ruta_id", IdRuta);
        cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
        cmd.Parameters.AddWithValue("@Peso", Peso);
        cmd.ExecuteNonQuery();
        conn.Close();
        return "OK";
    }
}
