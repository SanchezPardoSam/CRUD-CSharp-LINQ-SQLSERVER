using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace Wpf_linQ_CRUD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dataContext;
        public MainWindow()
        {
            InitializeComponent();

            string miConexcion = ConfigurationManager.
                ConnectionStrings["Wpf_linQ_CRUD.Properties.Settings.CRUDlinQSQL"].
                ConnectionString;

            dataContext = new DataClasses1DataContext(miConexcion);

            //InsertarEmpresa();
            //InsertarEmpleado();
            //InsertarCargo();
            //InsertarCargoEmpleado();
            //ActualizarEmpleado();
            DeleteEmpleado();
        }

        public void InsertarEmpresa()
        {
            dataContext.ExecuteCommand("delete from Empresa");

            Empresa platzi = new Empresa();
            platzi.Nombre = "Platzi";
            dataContext.Empresa.InsertOnSubmit(platzi);

            Empresa google = new Empresa();
            google.Nombre = "Google";
            dataContext.Empresa.InsertOnSubmit(google);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empresa;
        }

        public void InsertarEmpleado()
        {
            Empresa platzi = dataContext.Empresa.First(em => em.Nombre.Equals("Platzi"));

            Empresa google = dataContext.Empresa.First(em => em.Nombre.Equals("Google"));

            List<Empleado> listEmpleado = new List<Empleado>();

            listEmpleado.Add(new Empleado { Nombre = "Sam", EmpresaId = platzi.Id });
            listEmpleado.Add(new Empleado { Nombre = "Andy", EmpresaId = google.Id });
            listEmpleado.Add(new Empleado { Nombre = "Fiorella", EmpresaId = platzi.Id });
            listEmpleado.Add(new Empleado { Nombre = "Antonella", EmpresaId = google.Id });
            listEmpleado.Add(new Empleado { Nombre = "Rody", EmpresaId = platzi.Id });
            listEmpleado.Add(new Empleado { Nombre = "Lusi", EmpresaId = google.Id });

            dataContext.Empleado.InsertAllOnSubmit(listEmpleado);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleado;
        }
        public void InsertarCargo()
        {
            List<Cargo> listCargo = new List<Cargo>();
            listCargo.Add(new Cargo { NombreCargo = "SEO" });
            listCargo.Add(new Cargo { NombreCargo = "Director" });
            listCargo.Add(new Cargo { NombreCargo = "Jefe de area" });
            listCargo.Add(new Cargo { NombreCargo = "Jefe de desarrollo" });
            listCargo.Add(new Cargo { NombreCargo = "Desarrollador" });
            listCargo.Add(new Cargo { NombreCargo = "DIseñador" });

            dataContext.Cargo.InsertAllOnSubmit(listCargo);
            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Cargo;
        }
        public void InsertarCargoEmpleado()
        {
            Empleado emp = dataContext.Empleado.First(empl => empl.Nombre.Equals("Sam"));

            Cargo cargo = dataContext.Cargo.First(carg => carg.NombreCargo.Equals("SEO"));

            CargoEmpleado cargoEmpl = new CargoEmpleado { EmpleadoId = emp.Id, CargoId = cargo.Id };

            dataContext.CargoEmpleado.InsertOnSubmit(cargoEmpl);
            dataContext.SubmitChanges();
            Principal.ItemsSource = dataContext.CargoEmpleado;
        }

        public void ActualizarEmpleado()
        {
            Empleado emp = dataContext.Empleado.First(empl => empl.Nombre.Equals("Lusi"));

            emp.Nombre = "Luis";

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleado;
        }
        public void DeleteEmpleado()
        {
            Empleado emp = dataContext.Empleado.First(empl => empl.Nombre.Equals("Luis"));

            dataContext.Empleado.DeleteOnSubmit(emp);
            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleado;
        }
    }
}
