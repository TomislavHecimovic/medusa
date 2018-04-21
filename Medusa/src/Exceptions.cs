using System;

namespace Medusa
{
    abstract class Exceptions
    {
        public class MsiAfterburnerNotInstalled : Exception
        {
            public MsiAfterburnerNotInstalled(string message) : base(message)
            {
            }
        }

        public class MsiAfterburnerNotStarted : Exception
        {
            private string _exeFullPath;
            public string ExeFullPath { get => _exeFullPath; }

            public MsiAfterburnerNotStarted(string message, string exeFullPath) : base(message)
            {
                _exeFullPath = exeFullPath;
            }
        }
    }
}
