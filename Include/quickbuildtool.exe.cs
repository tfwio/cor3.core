using System;

namespace SimpleTask
{
    public class SetEnvironmentVariable : ITask
    {
        private IBuildEngine engine;              
        public IBuildEngine BuildEngine
        {
            get { return engine; }
            set { engine = value; }
        }       

        private ITaskHost host;
        public ITaskHost HostObject
        {
            get { return host; }
            set { host = value; }
        }

        private string name;
       
        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string varValue;

        [Required]
        public string Value
        {
            get { return varValue; }
            set { varValue = value; }
        }

        public bool Execute()
        {                       
            System.Environment.SetEnvironmentVariable(name, varValue);
            string message = string.Format("Environment Variable {0} set to {1}", name, varValue);
            BuildMessageEventArgs args = new BuildMessageEventArgs(
                message, string.Empty, "SetEnvironmentVariable", MessageImportance.Normal);
            engine.LogMessageEvent(args);
          
            return true;
        }
    }
}