﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EntidadesCompartidas;
using Logica;
using System.Drawing;

namespace Presentacion
{
    public partial class am_nacionales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            botones_inicio();
            if (!IsPostBack)
            {
                Session["Nacional"] = null;
                cargar_secciones();
                cargar_periodistas();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

        }
        protected void gvPeriodistasSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                lblMsj.Text = ex.Message;
                lblMsj.ForeColor = Color.Red;
                //this.Response.Write("error al seleccionar!" + ex);
            }
        }

        protected void buscar()
        {
            try
            {
                Nacional noticia = null;
                Noticia n = FabricaLogica.getLogicaNoticias().BuscarNoticia(txtCodigo.Text);

                if (n != null && n.TipoNoticia != "Nacional")
                {
                    limpiar();
                    throw new Exception("La noticia no es Nacional");
                }

                noticia = (Nacional)n;

                if (txtCodigo.Text == string.Empty)
                    throw new Exception("Debe ingresar un codigo");

                if (noticia == null)
                {
                    btnGuardar.Enabled = true;
                    lblMsj.Text = "No se encontró la noticia. Puede agregarla.";
                    lblMsj.ForeColor = Color.DarkOrange;
                }
                else
                {
                    btnModificar.Enabled = true;

                    txtCodigo.Text = noticia.Codigo;
                    txtTitulo.Text = noticia.Titulo;
                    txtCuerpo.Text = noticia.Cuerpo;
                    ddlImportancia.SelectedItem.Text = noticia.Importancia.ToString();
                    ddlSecciones.SelectedValue = noticia.Seccion.Codigo_secc;
                    gvPeriodistasSeleccion.DataSource = noticia.Periodistas;
                    gvPeriodistasSeleccion.DataBind();
                    Session["Nacional"] = noticia;

                    lblMsj.Text = "Noticia Encontrada";
                    lblMsj.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblMsj.Text = ex.Message;
                lblMsj.ForeColor = Color.Red;
            }
        }
        protected void guardar()
        {
            try
            {
                Usuario user = (Usuario)Session["user"];
                DateTime date = Convert.ToDateTime(txtfecha.Text);
                string code = txtCodigo.Text;
                string title = txtTitulo.Text;
                string body = txtCuerpo.Text;
                Seccion secc = FabricaLogica.getLogicaSecciones().BuscarSeccion(ddlSecciones.SelectedValue);
                int imp = Convert.ToInt32(ddlImportancia.SelectedValue);
                List<Periodista> ptas = null;
                foreach (DataGridItem item in gvPeriodistasElegidos.Rows)
                {
                    Periodista p = FabricaLogica.getLogicaPeriodistas().BuscarPeriodistaActivo(item.Cells[0].ToString());
                    ptas.Add(p);
                }

                Nacional noticia = new Nacional(secc, code, date, title, body, imp, ptas, user);

                if (noticia != null)
                {
                    FabricaLogica.getLogicaNoticias().AgregarNoticia(noticia);

                    lblMsj.Text = "Noticia Nacional Agregada con Exito";
                    lblMsj.ForeColor = Color.Green;

                    limpiar();
                }
                else
                {
                    lblMsj.Text = "No se pudo agregar la Noticia Nacional";
                    lblMsj.ForeColor = Color.DarkOrange;
                }

                txtCodigo.Focus();

            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;
                lblMsj.Text = ex.Message;
                lblMsj.ForeColor = Color.Red;
            }
        }
        protected void modificar()
        {
            try
            {
                lblMsj.Text = "";
                Usuario user = (Usuario)Session["user"];
                Nacional noticia = (Nacional)Session["Nacional"];

                if (noticia != null)
                {
                    noticia.Fecha = Convert.ToDateTime(txtfecha.Text);
                    noticia.Titulo = txtTitulo.Text.Trim();
                    noticia.Cuerpo = txtCuerpo.Text;              
                    noticia.Importancia = Convert.ToInt32(ddlImportancia.SelectedValue);
                    noticia.Seccion =  FabricaLogica.getLogicaSecciones().BuscarSeccion(ddlSecciones.SelectedValue);

                    FabricaLogica.getLogicaNoticias().ModificarNoticia(noticia);
                    lblMsj.Text = "Noticia Internacional Modificada con Exito";
                    lblMsj.ForeColor = Color.Green;
                }
                else
                {
                    lblMsj.Text = "No se pudo modificar la Noticia Internacional";
                    lblMsj.ForeColor = Color.DarkOrange;
                }

                txtCodigo.Focus();

            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;
                lblMsj.Text = ex.Message;
                lblMsj.ForeColor = Color.Red;
            }
        }

        private void limpiar()
        {
            txtCodigo.Text = "";
            txtTitulo.Text = "";
            txtCuerpo.Text = "";
            //fecha.SelectedDate.Date.ti = "00010101";
            gvPeriodistasElegidos = null;
            //ddlImportancia.
            //ddlSecciones.
        }
        private void botones_inicio()
        {
            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;
            btnLimpiar.Enabled = true;
        }

        protected void cargar_secciones()
        {
            ddlSecciones.DataSource = Session["secciones"];
            ddlSecciones.DataTextField = "Nombre_secc";
            ddlSecciones.DataValueField = "Codigo_secc";
            ddlSecciones.DataBind();
        }
        protected void cargar_periodistas()
        {
            gvPeriodistasSeleccion.DataSource = (List<Periodista>)Session["periodistas_todos"];
            gvPeriodistasSeleccion.DataBind();
        }

        
    }
}