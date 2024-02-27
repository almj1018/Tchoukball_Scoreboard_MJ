using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class MatchHistoryViewModel : ViewModelBase
    {
        private HomeViewModel _model;

        public MatchHistoryViewModel(HomeViewModel model)
        {
            _model = model;
        }

        public ObservableCollection<ScoreboardItemViewModel> MatchList
        {
            get { return _model.MatchList; }
        }

        public HomeViewModel? ScoreboardItem 
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
