using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class MateriaAdapter:Adapter
    {

        public List<Materia> GetAll()
        {
            List<Materia> Materias = new List<Materia>();

            try{
            
                this.OpenConnection();

                SqlCommand cmdMaterias = new SqlCommand("SELECT M.id_materia, M.desc_materia, M.hs_semanales, M.hs_totales, M.id_plan, P.desc_plan, E.* FROM materias AS M LEFT OUTER JOIN planes AS P ON P.id_plan = M.id_plan LEFT JOIN especialidades E ON E.id_especialidad = P.id_especialidad", sqlConn);
                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();
                while (drMaterias.Read())
                {
                    Materia Materia = new Materia();

                    Materia.ID = (int)drMaterias["id_materia"];
                    Materia.Descripcion = (string)drMaterias["desc_materia"];
                    Materia.HSSemanales = (int)drMaterias["hs_semanales"];
                    Materia.HSTotales = (int)drMaterias["hs_totales"];
                    Materia.IDPlan = (int)drMaterias["id_plan"];
                    Materia.Plan = (string)drMaterias["desc_plan"];
                    Materia.IDEspecialidad = (int)drMaterias["id_especialidad"];
                    Materia.Especialidad = (string)drMaterias["desc_especialidad"];

                    Materias.Add(Materia);
                }
                drMaterias.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Materias", Ex);
                throw ExcepcionManejada;
            }finally{
                this.CloseConnection();
            }
            return Materias;
        }

        public Business.Entities.Materia GetOne(int ID)
        {
            Materia oEntity = new Materia();

            try
            {

                this.OpenConnection();

                SqlCommand cmdMaterias = new SqlCommand("SELECT M.*, P.*, E.* FROM materias AS M LEFT JOIN planes AS P ON P.id_plan = M.id_plan LEFT JOIN especialidades AS E ON P.id_especialidad = E.id_especialidad WHERE M.id_materia=@id", sqlConn);
                cmdMaterias.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();
                if (drMaterias.Read())
                {
                    oEntity.ID = (int)drMaterias["id_materia"];
                    oEntity.Descripcion = (string)drMaterias["desc_materia"];
                    oEntity.HSSemanales = (int)drMaterias["hs_semanales"];
                    oEntity.HSTotales = (int)drMaterias["hs_totales"];
                    oEntity.IDPlan = (int)drMaterias["id_plan"];
                    oEntity.Plan = (string)drMaterias["desc_plan"];
                    oEntity.IDEspecialidad = (int)drMaterias["id_especialidad"];
                    oEntity.Especialidad = (string)drMaterias["desc_especialidad"];

                }
                drMaterias.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar la materia", Ex);
                throw ExcepcionManejada;
            }finally{
                this.CloseConnection();
            }
            return oEntity;
        }

        public List<Materia> GetSome(int IDPlan)
        {
            List<Materia> Materias = new List<Materia>();

            try
            {

                this.OpenConnection();

                SqlCommand cmdMaterias = new SqlCommand("SELECT M.id_materia, M.desc_materia, M.hs_semanales, M.hs_totales, M.id_plan, P.desc_plan, E.id_especialidad, E.desc_especialidad FROM materias AS M LEFT OUTER JOIN planes AS P ON P.id_plan = M.id_plan LEFT JOIN especialidades E ON E.id_especialidad = P.id_especialidad WHERE M.id_plan = @id_plan", sqlConn);
                cmdMaterias.Parameters.Add("@id_plan", SqlDbType.Int).Value = IDPlan;
                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();
                while (drMaterias.Read())
                {
                    Materia Materia = new Materia();

                    Materia.ID = (int)drMaterias["id_materia"];
                    Materia.Descripcion = (string)drMaterias["desc_materia"];
                    Materia.HSSemanales = (int)drMaterias["hs_semanales"];
                    Materia.HSTotales = (int)drMaterias["hs_totales"];
                    Materia.IDPlan = (int)drMaterias["id_plan"];
                    Materia.Plan = (string)drMaterias["desc_plan"];
                    Materia.IDEspecialidad = (int)drMaterias["id_especialidad"];
                    Materia.Especialidad = (string)drMaterias["desc_especialidad"];

                    Materias.Add(Materia);
                }
                drMaterias.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Materias", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return Materias;
        }

        public void Delete(int ID)
        {
            try{
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("DELETE materias WHERE id_materia=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();
            } catch(Exception Ex) {
                Exception ExcepcionManejada = new Exception("Error al eliminar el Materia:" + Ex.ToString(), Ex);
            } finally {
                this.CloseConnection();
            }
        }

        public void Save(Materia Materia)
        {

            if (Materia.State == BusinessEntity.States.New)
            {
                this.Insert(Materia);
            }
            else if (Materia.State == BusinessEntity.States.Deleted)
            {
                this.Delete(Materia.ID);
            }
            else if (Materia.State == BusinessEntity.States.Modified)
            {
                this.Update(Materia);
            }
            Materia.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Materia Materia)
        {
            try{
                this.OpenConnection();
             
                SqlCommand cmdSave = new SqlCommand("UPDATE materias SET desc_materia = @desc, id_plan = @id_plan, hs_semanales = @hs_semanales, hs_totales = @hs_totales WHERE id_materia = @id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = Materia.ID;
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = Materia.Descripcion;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = Materia.IDPlan;
                cmdSave.Parameters.Add("@hs_semanales", SqlDbType.Int).Value = Materia.HSSemanales;
                cmdSave.Parameters.Add("@hs_totales", SqlDbType.Int).Value = Materia.HSTotales;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del Materia", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Materia Materia)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT INTO materias (desc_materia,id_plan,hs_semanales,hs_totales) " +
                    "VALUES (@desc,@id_plan,@hs_semanales,@hs_totales) " +
                    "SELECT @@identity", sqlConn);
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = Materia.Descripcion;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = Materia.IDPlan;
                cmdSave.Parameters.Add("@hs_semanales", SqlDbType.Int).Value = Materia.HSSemanales;
                cmdSave.Parameters.Add("@hs_totales", SqlDbType.Int).Value = Materia.HSTotales;
                Materia.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
              
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear el Materia" + Ex, Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
    
    }
}
