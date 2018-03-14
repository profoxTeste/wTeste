using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

namespace WChat
{
    static class Program  {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var db = new Contexto();
            //db.Database.CreateIfNotExists();
            var a1 = new Assunto() { Id = "Ass1", Nome = "Assunto 1" };
            var a2 = new Assunto() { Id = "Ass2", Nome = "Assunto 2" };

            db.Assunto.Add(a1);
            db.Assunto.Add(a2);

        }
    }
}
