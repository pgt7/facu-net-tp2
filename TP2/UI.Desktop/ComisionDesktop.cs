﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class ComisionDesktop : ApplicationForm
    {
        private Comision _Comision;

        public Comision Comision
        {
            get { return _Comision; }
            set { _Comision = value; }
        }

        public ComisionDesktop()
        {
            InitializeComponent();
            this.loadCmb();
        }
        public ComisionDesktop(ModoForm modo):this(){
            this.Modo = modo;
        }
        private void loadCmb()
        {
            this.cmbPlanes.DataSource = this.getPlanes();
            this.cmbPlanes.DisplayMember = "Descripcion";
            this.cmbPlanes.ValueMember = "ID";
        }
        public ComisionDesktop(int ID, ModoForm modo):this()
        {
           
            this.Modo = modo;
            ComisionLogic logic = new ComisionLogic();
            this.Comision = logic.GetOne(ID);
            MapearDeDatos();
           
        }
         public override void MapearDeDatos()
        {
            this.txtID.Text = this.Comision.ID.ToString();

            this.txtDescripcion.Text = Comision.Descripcion;
            this.cmbPlanes.SelectedValue = Comision.IDPlan;
            this.txtAnio.Text = Comision.AnioEspecialidad.ToString();

            string txtAceptar = "Aceptar";

            if (Modo.Equals(ModoForm.Alta) || Modo.Equals(ModoForm.Modificacion)) txtAceptar = "Guardar";
            this.btnAceptar.Text = txtAceptar;
        }
        

        private void ComisionDesktop_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                this.GuardarCambios();
                this.Close();
            }
        }
        public override void GuardarCambios()
        {
            this.MapearADatos();
            ComisionLogic uLogic = new ComisionLogic();
            uLogic.Save(this.Comision);
        }
        public override void MapearADatos()
        {
            if (Modo.Equals(ModoForm.Alta))
            {
                this.Comision = new Comision();
                this.Comision.State = BusinessEntity.States.New;
            }
            else if (Modo.Equals(ModoForm.Modificacion))
            {
                this.Comision.ID = Int32.Parse(txtID.Text);
                this.Comision.State = BusinessEntity.States.Modified;
            }

            this.Comision.Descripcion = this.txtDescripcion.Text;
            this.Comision.AnioEspecialidad = Convert.ToInt32(this.txtAnio.Text);
            this.Comision.IDPlan = (int)this.cmbPlanes.SelectedValue;

        }

        public override bool Validar()
        {
            return true;
        }
    }
}