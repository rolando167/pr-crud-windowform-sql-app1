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

namespace crud_sql_entityFramework.Presentacion
{
    public partial class FrmTabla : MetroFramework.Forms.MetroForm
    {
        public int? id;
        cliente ocliente = null;
        public FrmTabla(int? id=null)
        {
            InitializeComponent();

            this.id = id;
            if (id != null)
                cargarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        #region HELPER
        private void Guardar()
        {
            using (HOMEEntities db = new HOMEEntities())
            {
                DateTime now = DateTime.Now;

                if (id == null)
                    ocliente = new cliente();
           
                ocliente.nombre = txtNombre.Text;
                ocliente.correo = txtCorreo.Text;
                ocliente.fecha_nacimiento = dateFechaNacimiento.Value;

                if (id == null)
                {
                    ocliente.created_at = now;
                    db.cliente.Add(ocliente);
                }
                else
                {
                    ocliente.updated_at = now;
                    db.Entry(ocliente).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                this.Close();
                MessageBox.Show("Funcion realizada!!");
            }
        }

        private void cargarDatos()
        {
            using (HOMEEntities db = new HOMEEntities())
            {
                ocliente = db.cliente.Find(id);
                txtNombre.Text = ocliente.nombre;
                txtCorreo.Text = ocliente.correo;
                dateFechaNacimiento.Value = (DateTime)ocliente.fecha_nacimiento;

            }
        }
        #endregion
    }
}
