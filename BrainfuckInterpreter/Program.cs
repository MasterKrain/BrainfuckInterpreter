using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    class Program
    {
        private static Interpreter m_Interpreter;

        static void Main(string[] args)
        {
            m_Interpreter = new Interpreter(30000);
            Console.Title = m_Interpreter.Title;
            m_Interpreter.Interpret(args[0]);
            Console.ReadKey();
        }
    }
}
