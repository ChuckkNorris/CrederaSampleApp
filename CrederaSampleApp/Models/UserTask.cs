using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrederaSampleApp.Models {
    public class UserTask : BaseModel {

        // TITLE
        private string _Title;
        public string Title {
            get { return _Title; }
            set { SetField(ref _Title, value); }
        }

        // DESCRIPTION
        private string _Description;
        public string Description {
            get { return _Description; }
            set { SetField(ref _Description, value); }
        }

        // DUE DATE
        private DateTime _DueDate = DateTime.Today;
        public DateTime DueDate {
            get { return _DueDate; }
            set { SetField(ref _DueDate, value); }
        }

    }
}
