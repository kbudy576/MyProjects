using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public abstract class GradeTracker : IGradeTracker
    {
        public abstract void AddGrade(float grade);
        public abstract GradeStatistics ComputeStatistics();
        public abstract void WriteGrades(TextWriter destination);

        public string Name
        {
            get  // Gets are used for computation
            {
                return _name;
            }
            set  // Sets are usd for validation
            {

                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty");

                }
                if (_name != value && NameChanged != null)
                {
                    NameChangedEventArgs args = new NameChangedEventArgs(); // Events revisited
                    args.ExistingName = _name;
                    args.NewName = value;

                    NameChanged(this, args); //pass along myself then arguments
                }

                _name = value;
            }

        }

        public event NameChangedDelegate NameChanged;

        protected string _name;
       
    }
}
