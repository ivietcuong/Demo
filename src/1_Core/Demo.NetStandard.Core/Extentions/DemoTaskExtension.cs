using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Demo.NetStandard.Core.Extentions
{
    public static class DemoTaskExtension
    {
        public static async void Run(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e); 
                throw;
            }
        }
    }
}
