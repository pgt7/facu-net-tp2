﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Logic;
using Business.Entities;

namespace UI.Web
{
    public class BaseABM : System.Web.UI.Page
    {
        public List<Especialidad> lstEsp;
        public List<Plan> lstPlan;
        public List<Comision> lstCom;
        public List<Materia> lstMat;

        public enum FormModes { Alta, Baja, Modificacion }
        public FormModes FormMode
        {
            get { return (FormModes)this.ViewState["FormMode"]; }
            set { this.ViewState["FormMode"] = value; }
        }

        protected int SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] != null) return (int)this.ViewState["SelectedID"];
                else return 0;
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }

        protected bool IsEntitySelected
        {
            get { return (this.SelectedID != 0); }
        }

        public List<Especialidad> getEspecialidades()
        {
            EspecialidadLogic esp = new EspecialidadLogic();
            this.lstEsp = esp.GetAll();
            return this.lstEsp;
        }

        public List<Plan> getPlanes(int IDEspecialidad)
        {
            PlanLogic esp = new PlanLogic();
            this.lstPlan = esp.GetAll(IDEspecialidad);
            return this.lstPlan;
        }

        public string getEspecialidadById(int id)
        {
            this.getEspecialidades();
            string nombre = this.lstEsp.Find(o => o.ID == id).Descripcion;
            return nombre;
        }


        public List<Comision> getComisiones(int IDPlan)
        {
            ComisionLogic com = new ComisionLogic();
            this.lstCom = com.GetSome(IDPlan);
            return this.lstCom;
        }

        public List<Materia> getMaterias(int IDPlan)
        {
            MateriaLogic mat = new MateriaLogic();
            this.lstMat = mat.GetSome(IDPlan);
            return this.lstMat;
        }
    

        public virtual bool Validar() { return false; }

    }
}