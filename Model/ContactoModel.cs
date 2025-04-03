using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;


namespace llamada.Model
{
   public  class ContactoModel
    {
        public string Nombre {get;set;}

        public Command<string> llamar_numero_comando { get; set; }
        public string imagen { get; set; }

        public string telefono { get;set; }




        public void alo()
        {

            if (PhoneDialer.Default.IsSupported)
            {
                PhoneDialer.Default.Open(telefono);
            }
        }
        public ContactoModel()
        {

           // llamar_numero_comando = new Command(llamar);

        }

    }
    
    }
