using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_GUI.ViewModel;

namespace Flights_GUI.Commands
{
    internal class UpdatePasswordCmnd : CommandBase
    {
        private UpdatePasswordVM updateVM;
        public UpdatePasswordCmnd(UpdatePasswordVM updateVM)
        {
            this.updateVM = updateVM;
        }

        public override void Execute(object paramater)
        {
            throw new NotImplementedException();
        }
    }
}
