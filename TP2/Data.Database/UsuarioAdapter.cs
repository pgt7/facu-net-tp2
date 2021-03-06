using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using Util;

namespace Data.Database
{
    public class UsuarioAdapter : Adapter
    {

        public List<Usuario> GetAll(TiposUsuarios TipoUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {

                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM usuarios WHERE tipo_usuario = @tipo_usuario ORDER BY apellido, nombre", sqlConn);
                cmdUsuarios.Parameters.Add("@tipo_usuario", SqlDbType.Int).Value = (int)TipoUsuario;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                while (drUsuarios.Read())
                {
                    Usuario usr = new Usuario();

                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.EMail = (string)drUsuarios["email"];
                    usr.Telefono = (string)drUsuarios["telefono"];
                    usr.FechaNac = ((DateTime)drUsuarios["fecha_nac"]).ToString("dd/MM/yyyy");
                    usr.Legajo = (int)drUsuarios["legajo"];
                    usr.TipoUsuario = (TiposUsuarios)drUsuarios["tipo_usuario"];
                    usr.IdPlan = (drUsuarios["id_plan"] as int?) ?? 0;

                    usuarios.Add(usr);
                }
                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return usuarios;
        }

        public int addDocenteToCurso(int id_docente,int id_curso) {
            int ID = 0;
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT INTO docentes_cursos (id_curso, id_docente, cargo) " +
                    "VALUES (@id_curso, @id_docente, 1) " +
                    "SELECT @@identity", sqlConn);

                cmdSave.Parameters.Add("@id_curso", SqlDbType.VarChar, 50).Value = id_curso;
                cmdSave.Parameters.Add("@id_docente", SqlDbType.VarChar, 50).Value = id_docente;
                
               ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al asignar el docente al curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return ID;    
        
        }

        public void removeDocenteToCurso(int id_docente, int id_curso)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("DELETE docentes_cursos WHERE id_docente=@id_docente AND id_curso=@id_curso", sqlConn);
                cmdDelete.Parameters.Add("@id_docente", SqlDbType.Int).Value = id_docente;
                cmdDelete.Parameters.Add("@id_curso", SqlDbType.Int).Value = id_curso;

                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al quitar el docente:" + Ex.ToString(), Ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void removeDocentesFromCurso(string excepto, int id_curso)
        {
            try
            {
                this.OpenConnection();
                string command = "DELETE docentes_cursos WHERE id_curso=@id_curso";
                if (excepto != "") command += " AND id_docente NOT IN (" + excepto + ")";
                SqlCommand cmdDelete = new SqlCommand(command, sqlConn);
                cmdDelete.Parameters.Add("@id_curso", SqlDbType.Int).Value = id_curso;

                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al quitar el docente:" + Ex.ToString(), Ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }


        public List<Usuario> GetDocentesByCurso(int idCurso)
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {

                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT U.* FROM  docentes_cursos DC INNER JOIN usuarios U ON  DC.id_docente = U.id_usuario WHERE DC.id_curso=@id_curso", sqlConn);
                cmdUsuarios.Parameters.Add("@id_curso", SqlDbType.Int).Value = (int)idCurso;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                while (drUsuarios.Read())
                {
                    Usuario usr = new Usuario();

                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.EMail = (string)drUsuarios["email"];
                    usr.Telefono = (string)drUsuarios["telefono"];
                    usr.FechaNac = ((DateTime)drUsuarios["fecha_nac"]).ToString("dd/MM/yyyy");
                    usr.Legajo = (int)drUsuarios["legajo"];
                    usr.TipoUsuario = (TiposUsuarios)drUsuarios["tipo_usuario"];
                    usr.IdPlan = (drUsuarios["id_plan"] as int?) ?? 0;

                    usuarios.Add(usr);
                }
                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Docentes", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return usuarios;
        }

        public Usuario getUserByLegajo(int legajo)
        {
            Usuario usr=null;
            try
            {

                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT id_usuario FROM usuarios WHERE legajo=@legajo", sqlConn);
                cmdUsuarios.Parameters.Add("@legajo", SqlDbType.Int).Value = legajo;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
               
                if (drUsuarios.Read())
                {
                    usr = this.GetOne((int)drUsuarios["id_usuario"]);
                }
                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
               // this.CloseConnection();
            }
            return usr;


        }
        
        public Business.Entities.Usuario GetOne(int ID)
        {
            Usuario usr = new Usuario();

            try
            {

                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM usuarios WHERE id_usuario=@id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                if (drUsuarios.Read())
                {
                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.EMail = (string)drUsuarios["email"];
                    usr.Telefono = (string)drUsuarios["telefono"];
                    usr.FechaNac = ((DateTime)drUsuarios["fecha_nac"]).ToString("dd/MM/yyyy");
                    usr.Legajo = (int)drUsuarios["legajo"];
                    usr.TipoUsuario = (TiposUsuarios)drUsuarios["tipo_usuario"];
                    usr.IdPlan = (drUsuarios["id_plan"] as int?) ?? 0;
                  

                }
                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return usr;
        }

        public Business.Entities.Usuario GetUserValid(string usuario, string clave)
        {
            Usuario usr =null;

            try
            {

                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM usuarios WHERE nombre_usuario=@usuario AND clave=@clave", sqlConn);
                cmdUsuarios.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuario;
                cmdUsuarios.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = clave;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                if (drUsuarios.HasRows && drUsuarios.Read())
                {
                    usr = new Usuario();
                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.EMail = (string)drUsuarios["email"];
                    usr.Telefono = (string)drUsuarios["telefono"];
                    usr.FechaNac = ((DateTime)drUsuarios["fecha_nac"]).ToString("dd/MM/yyyy");
                    usr.Legajo = (int)drUsuarios["legajo"];
                    usr.TipoUsuario = (TiposUsuarios)drUsuarios["tipo_usuario"];
                    usr.IdPlan =  (drUsuarios["id_plan"] as int?) ?? 0;
                      //  drUsuarios["id_plan"] as default(int);
                }
                
                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return usr;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("DELETE usuarios WHERE id_usuario=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar usuario:" + Ex.ToString(), Ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Usuario usuario)
        {

            if (usuario.State == BusinessEntity.States.New)
            {
                this.Insert(usuario);
            }
            else if (usuario.State == BusinessEntity.States.Deleted)
            {
                this.Delete(usuario.ID);
            }
            else if (usuario.State == BusinessEntity.States.Modified)
            {
                this.Update(usuario);
            }
            usuario.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Usuario usuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE usuarios SET nombre_usuario = @nombre_usuario, clave = @clave, habilitado = @habilitado, nombre = @nombre, apellido = @apellido, email = @email, telefono = @telefono, legajo = @legajo, fecha_nac = @fecha_nac, id_plan = @id_plan WHERE id_usuario = @id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = usuario.ID;
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.EMail;
                cmdSave.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = usuario.Telefono;
                cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = usuario.Legajo;
                cmdSave.Parameters.Add("@fecha_nac", SqlDbType.Date).Value = DateTime.ParseExact(Util.Util.DateToDb(usuario.FechaNac), "yyyy-MM-dd", null);
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = usuario.IdPlan;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Usuario usuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT INTO usuarios (nombre_usuario, clave, habilitado, nombre, apellido, email, telefono, legajo, fecha_nac, tipo_usuario, id_plan) " +
                    "VALUES (@nombre_usuario, @clave, @habilitado, @nombre, @apellido, @email, @telefono, @legajo, @fecha_nac, @tipo_usuario, @id_plan) " +
                    "SELECT @@identity", sqlConn);

                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.EMail;
                cmdSave.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = usuario.Telefono;
                cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = usuario.Legajo;
                cmdSave.Parameters.Add("@fecha_nac", SqlDbType.Date).Value = DateTime.ParseExact(Util.Util.DateToDb(usuario.FechaNac), "yyyy-MM-dd", null);
                cmdSave.Parameters.Add("@tipo_usuario", SqlDbType.Int).Value = (int)usuario.TipoUsuario;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = usuario.IdPlan;
                usuario.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

    }
}
