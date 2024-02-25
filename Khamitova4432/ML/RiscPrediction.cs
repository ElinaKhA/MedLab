using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khamitova4432.ML
{
    public class RiscPrediction
    {
        dynamic riskModel;

        public RiscPrediction(string pythonScriptPath)
        {
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(@"C:\diplomLastV");
                dynamic module = Py.Import("makeforestmodel");
                riskModel = module.RiskModel();
            }
        }

        public double PredictRisk(double[] newData)
        {
            using (Py.GIL())
            {
                dynamic np = Py.Import("numpy");
                dynamic newDataArray = np.array(newData);
                dynamic result = riskModel.predict(newDataArray);
                return (double)result;
            }
        }
    }
}
