using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flights_GUI.Commands;

namespace Flights_GUI.ViewModel
{
    internal class UpdatePasswordVM : ViewModelBase
    {
        #region constructor
        public UpdatePasswordVM()
        {
            UpdatePswrdCommand = new UpdatePasswordCmnd(this);
        }
        #endregion

        #region properties


        #endregion

        public ICommand UpdatePswrdCommand { get; set; }
    }
}
