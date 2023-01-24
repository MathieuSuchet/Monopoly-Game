using System;

namespace MonopolyLib.Logique.Events
{
    public class PartieResumeArgs : EventArgs
    {
        public string Resume;

        public PartieResumeArgs(string s)
        {
            Resume = s;
        }
    }
}