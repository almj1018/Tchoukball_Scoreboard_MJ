using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class KeyboardShortcutsViewModel : ViewModelBase
    {
        private KeyboardSettingsItemViewModel _model;

        public KeyboardShortcutsViewModel(KeyboardSettingsItemViewModel model)
        {
            _model = model;
        }

        public KeyboardSettingsItemViewModel? KeyboardSettings
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

    }
}
