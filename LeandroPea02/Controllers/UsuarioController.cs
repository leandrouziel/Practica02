using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using LeandroPea02.Models;

namespace LeandroPea02.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly string connectionString = "Data Source=(local);Initial Catalog=Tienda;Integrated Security=True";
        // GET: Usuario
        public ActionResult Index()
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var queryUsuario = "SELECT * FROM Usuario";
                var usuarios = cn.Query<UsuarioEntity>(queryUsuario).ToList();
                return View(usuarios);
            }
        }

        public ActionResult Editar(int id)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var queryClient = "SELECT * FROM Usuario WHERE IdUsuario = " + id;
                UsuarioEntity usuario = cn.QueryFirst<UsuarioEntity>(queryClient);
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult Editar(UsuarioEntity u)
        {
            var queryUpdateClient = "UPDATE Usuario SET Nombres = @nombre, Apellidos = @apellido, Usuario = @usuario, clave = @clave, Estado = @estado, Observacion = @observacion WHERE IdUsuario = @IdUsuario";

            using (var cn = new SqlConnection(connectionString))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@nombre", u.Nombres);
                parametros.Add("@apellido", u.Apellidos);
                parametros.Add("@usuario", u.Usuario);
                parametros.Add("@clave", u.Clave);
                parametros.Add("@estado", u.Estado);
                parametros.Add("@observacion", u.Observacion);
                parametros.Add("@IdUsuario", u.IdUsuario);
                cn.Execute(queryUpdateClient, param: parametros);

                var usuarios = cn.Query<UsuarioEntity>("SELECT * FROM Usuario").ToList();
                return View("Index", usuarios);
            }
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(UsuarioEntity u)
        {
            var queryInsertClient = "INSERT INTO Usuario VALUES (@nombre, @apellido, @usuario, @clave, @estado, @observacion)";

            using (var cn = new SqlConnection(connectionString))
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@nombre", u.Nombres);
                parametros.Add("@apellido", u.Apellidos);
                parametros.Add("@usuario", u.Usuario);
                parametros.Add("@clave", u.Clave);
                parametros.Add("@estado", u.Estado);
                parametros.Add("@observacion", u.Observacion);
                cn.Execute(queryInsertClient, param: parametros);

                var usuarios = cn.Query<UsuarioEntity>("SELECT * FROM Usuario").ToList();
                return View("Index", usuarios);
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var queryClient = "SELECT * FROM Usuario WHERE IdUsuario = " + id;
                UsuarioEntity usuario = cn.QueryFirst<UsuarioEntity>(queryClient);
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(UsuarioEntity u)
        {
            var queryDeleteCliente = "DELETE FROM Usuario WHERE IdUsuario = " + u.IdUsuario;

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Execute(queryDeleteCliente);

                var usuarios = cn.Query<UsuarioEntity>("SELECT * FROM Usuario").ToList();
                return View("Index", usuarios);
            }
        }

    }
}