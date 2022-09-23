using System;
using System.Collections.Generic;
using System.CommandLine;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace RealitSystem_CLI.Commands
{
    internal class RealitCommand
    {
        
        private List<string> failures = new List<string>();
        private List<Action> modificationToPerform = new List<Action>();

        protected void AddFailureMessage(string message) => failures.Add(message);

        protected void AddModification(Action action) => modificationToPerform.Add(action);

        public RealitReturnCode Apply()
        {
            if (failures.Count > 0)
            {
                string message = string.Join("\n", failures);
                return new RealitReturnCode(ReturnStatus.Failure, message);
            }

            try
            {
                foreach (var action in modificationToPerform)
                    action.Invoke();

                return new RealitReturnCode(ReturnStatus.Success, "Changes successfuly registered. Perform 'Realit build' to generate the scene");
            }

            catch (Exception e)
            {
                return new RealitReturnCode(ReturnStatus.Failure, e.Message);
            }
        }
    }
}
