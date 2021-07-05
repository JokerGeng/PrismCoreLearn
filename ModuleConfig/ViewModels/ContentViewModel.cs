using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleConfig.ViewModels
{
    public class ContentViewModel:BindableBase
    {
        IModuleManager _moduleManager;
        public ContentViewModel(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
            content = "OtherContentViewModel";
            Pub = new DelegateCommand(PubCommand);
            PubStr = new DelegateCommand<string>(PubCommandStr);
        }

        private string content;

        public DelegateCommand Pub { get; set; }
        public DelegateCommand<string> PubStr { get; set; }

        public string Content
        {
            get { return content; }
            set
            {
                SetProperty(ref content, value);
            }
        }

        private void PubCommand()
        {
            Content = "ClickChanged";
            Console.WriteLine("PubCommand");
        }

        private void PubCommandStr(string str)
        {
            Content = "ContentViewModel";
            Console.WriteLine(str);
        }
    }
}
