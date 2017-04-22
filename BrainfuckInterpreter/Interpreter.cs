using System;
using System.Collections.Generic;
using System.IO;

namespace BrainfuckInterpreter
{
    class Interpreter
    {
        private string m_Title = "Brainfuck interpreter by Stan Q. Graafmans, 2017";
        public string Title { get { return m_Title; } }

        private string m_File;

        private byte[] m_Code;
        private byte[] m_Cells;

        private ushort m_Pointer = 0;
        private ushort m_MemorySize = 0;

        public Interpreter(ushort memorySize)
        {
            m_MemorySize = memorySize;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(m_Title);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void Init()
        {
            m_Cells = new byte[m_MemorySize];

            int length = m_Cells.Length;
            for (int i = 0; i < length; ++i)
            {
                m_Cells[i] = 0;
            }

            m_Pointer = 0;
        }

        public void Interpret(string file)
        {
            Init();

            Console.WriteLine();

            ReadCode(file);

            Stack<int> loopStack = new Stack<int>();

            int length = m_Code.Length;
            for (int i = 0; i < length; ++i)
            {
                if (loopStack.Count > 0)
                {
                    if (m_Code[i] != 91 && m_Code[i] != 93 && loopStack.Peek() < 0) continue;
                }

                switch (m_Code[i])
                {
                    case 60:
                        m_Pointer--;
                        continue;
                    case 62:
                        m_Pointer++;
                        continue;
                    case 44:
                        m_Cells[m_Pointer] = (byte) Console.ReadKey().KeyChar;
                        continue;
                    case 46:
                        Console.Write((char) m_Cells[m_Pointer]);
                        continue;
                    case 43:
                        m_Cells[m_Pointer]++;
                        continue;
                    case 45:
                        m_Cells[m_Pointer]--;
                        continue;
                    case 91:
                        if (m_Cells[m_Pointer] > 0) loopStack.Push(i + 1);
                        else loopStack.Push(-1);
                        continue;
                    case 93:
                        if (loopStack.Count > 0)
                        {
                            if (loopStack.Peek() < 0)
                            {
                                loopStack.Pop();
                                continue;
                            }

                            if (m_Cells[m_Pointer] > 0)
                            {
                                i = loopStack.Peek() - 1;
                            }
                            else
                            {
                                loopStack.Pop();
                            }
                        }

                        continue;
                }
            }
        }

        private void ReadCode(string file)
        {
            m_File = file;

            m_Code = File.ReadAllBytes(@file);
        }
    }
}
