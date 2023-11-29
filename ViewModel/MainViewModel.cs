using CollegeNavigatorPC.View;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CollegeNavigatorPC.Model;

namespace CollegeNavigatorPC.ViewModel
{
    internal class MainViewModel : ViewModedBase
    {
        private Page Map = new MapPage();
        private Page Audiences = new AudiencesPage();
        private Page Exit = new ExitPage();
        private Page _CurPage = new MapPage();

        public Page CurPage
        {
            get => _CurPage;
            set => Set(ref _CurPage, value);
        }
        public ICommand OpenMapPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Map);
            }
        }
        public ICommand OpenAudiencesPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Audiences);
            }
        }
        public ICommand OpenExitPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Exit);
            }
        }
    }
}
