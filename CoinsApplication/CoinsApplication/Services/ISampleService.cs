using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsApplication.Services
{
    public interface ISampleService
    {
        void SampleMethod();
    }

    public class SampleService : ISampleService
    {
        public SampleService()
        {
            
        }

        public void SampleMethod()
        {
            Debug.WriteLine("Sample method called");
        }
    }
}
