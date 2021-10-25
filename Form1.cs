using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using crud_sql_entityFramework.Models;

namespace crud_sql_entityFramework
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "Bienvenidos !");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        #region HELPER
        private void Refrescar()
        {
            using (HOMEEntities db = new HOMEEntities())
            {
                var lista = from d in db.cliente
                            select d;
                dataGridViewClientes.DataSource = lista.ToList();
            }
        }

        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridViewClientes.Rows[dataGridViewClientes.CurrentRow.Index].Cells[0].Value.ToString());

            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Presentacion.FrmTabla oFrmTabla = new Presentacion.FrmTabla();
            // oFrmTabla.FormBorderStyle = FormBorderStyle.FixedSingle; // No al usar metroFramework UI
            oFrmTabla.ShowDialog();
            Refrescar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if(id!=null)
            {
                Presentacion.FrmTabla ofrmTabla = new Presentacion.FrmTabla(id);
                ofrmTabla.ShowDialog();
                Refrescar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                using (HOMEEntities db = new HOMEEntities())
                {
                    cliente ocliente = db.cliente.Find(id);
                    db.cliente.Remove(ocliente);

                    db.SaveChanges();
                    MessageBox.Show("Registro Eliminadoic!!");
                }
                Refrescar();
            }
        }
    }
}
