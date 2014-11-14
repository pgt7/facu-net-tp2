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
    public partial class MateriaDesktop : ApplicationForm
    {
        private Materia _Materia;

        public Materia Materia
        {
            get { return _Materia; }
            set { _Materia = value; }
        }
        public MateriaDesktop()
        {
            InitializeComponent();
            this.loadCmb();
        }
          public MateriaDesktop(ModoForm modo):this(){
            this.Modo = modo;
        }

          public MateriaDesktop(int ID, ModoForm modo):this()
        {
           
            this.Modo = modo;
            MateriaLogic logic = new MateriaLogic();
            this.Materia = logic.GetOne(ID);
            MapearDeDatos();
           
        }
          public override void MapearDeDatos()
          {
              this.txtID.Text = this.Materia.ID.ToString();

              this.txtMateria.Text = Materia.Descripcion;
              this.cmbPlanes.SelectedValue = Materia.IDPlan;
              this.txtHsSemanales.Text = Materia.HSSemanales.ToString();
              this.txtHsTotales.Text = Materia.HSTotales.ToString();

              string txtAceptar = "Aceptar";

              if (Modo.Equals(ModoForm.Alta) || Modo.Equals(ModoForm.Modificacion)) txtAceptar = "Guardar";
              this.btnAceptar.Text = txtAceptar;
          }

          private void loadCmb()
          {
              this.cmbPlanes.DataSource = this.getPlanes();
              this.cmbPlanes.DisplayMember = "Descripcion";
              this.cmbPlanes.ValueMember = "ID";
          }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                this.GuardarCambios();
                this.Close();
            }
        }

        public override bool Validar()
        {
            return true;
        }
        public override void GuardarCambios()
        {
            this.MapearADatos();
            MateriaLogic uLogic = new MateriaLogic();
            uLogic.Save(this.Materia);
        }
        public override void MapearADatos()
        {
            if (Modo.Equals(ModoForm.Alta))
            {
                this.Materia = new Materia();
                this.Materia.State = BusinessEntity.States.New;
            }
            else if (Modo.Equals(ModoForm.Modificacion))
            {
                this.Materia.ID = Int32.Parse(txtID.Text);
                this.Materia.State = BusinessEntity.States.Modified;
            }

            this.Materia.Descripcion = this.txtMateria.Text;
            this.Materia.HSSemanales = Convert.ToInt32(this.txtHsSemanales.Text);
            this.Materia.HSTotales = Convert.ToInt32(this.txtHsTotales.Text);
       
            this.Materia.IDPlan = (int)this.cmbPlanes.SelectedValue;

        }

        private void MateriaDesktop_Load(object sender, EventArgs e)
        {

        }
    }
}