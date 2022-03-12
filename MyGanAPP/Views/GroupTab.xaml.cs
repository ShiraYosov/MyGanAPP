using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupTab : TabbedPage
    {
        public GroupTab()
        {
            this.Children.Add(new Page());
            InitializeComponent();
        }
    }
}