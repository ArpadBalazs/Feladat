using System.Globalization;
using System.Runtime.InteropServices;

namespace ConsoleApp80
{
    internal class Program
    {
        public enum Kapu
        {
            AND,OR,NOT,NAND,NOR,XOR,XNOR,
        }
        public class Variable
        {
            public Variable(string name, string data)
            {
                Name = name;
                Data = data;
                SetDataValue();
            }

            public string Name { get; set; }
            public string Data { get; set; }

            public void SetDataValue()
            {
                string temp = "";
                double num = 0;
                for (int i = 0; i < Data.Length; i+=2)
                {
                    num = double.Parse(Data[i] +"."+ Data[i+1],CultureInfo.InvariantCulture);
                    if(num <= 0.8&&num>=0) {

                        temp += 0;
                    }
                    else if (num <=5&&num>=2.7)
                    {
                        temp += 1;
                    }
                    else
                    {
                        temp += "E";
                    }
                }
                this.Data = temp;
            }
        }
        static void Main(string[] args)
        {
            string[] data = Console.ReadLine().Split(" ");
            Variable[] variables = new Variable[50];
            int rcounter = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Length == 1)
                {
                    rcounter++;
                }
            }
            bool stop = false;
            string dataline = "";
            int counter = 0;
            string[] arr;
            while(rcounter > 0)
            {
                dataline=Console.ReadLine();
                for (int i = 0; i < data.Length; i++)
                {
                    arr = dataline.Split(" ");
                    if (data[i] == arr[0])
                    {
                        stop = false;
                        for (int j = 0; j < variables.Length&& !stop; j++)
                        {
                            if (variables[j] == null)
                            {
                                stop=true;
                                rcounter--;
                            }
                            else
                            {
                                if (variables[j].Name==arr[0])
                                {
                                    variables[counter] = new Variable(arr[0], arr[1]);
                                    counter++;
                                    rcounter--;
                                    stop = true;
                                }
                            }
                        }
                    }
                }
            }
            string tempdata = "";
            string output = GetData(data[data.Length - 1], variables);
            for (int i = data.Length - 2; i >= 0; i--)
            {
                if (data[i].Length == 1)
                {
                    tempdata = GetData(data[i],variables);
                }else if (data[i].ToUpper().Equals("NOT"))
                {
                    tempdata = Logic(Kapu.NOT, tempdata);
                }
                else
                {
                    output = Logic((Kapu)Enum.Parse(typeof(Kapu), data[i]), tempdata, output);
                }
            }
        }

        private static string GetData(string name, Variable[] variables)
        {
            for (int j = 0; j < variables.Length; j++)
            {
                if (variables[j].Name.Equals(name))
                {
                    return variables[j].Data;
                    j=variables.Length;
                }
            }
            return "";
        }


        private static string Logic(Kapu kapu,string a,string b="")
        {
            string output="";
            if (kapu == Kapu.NOT)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].ToString().Equals("E"))
                    {
                        output += "E";
                    }else if (a[i].ToString().Equals("1"))
                    {
                        output+="0";
                    }
                    else
                    {
                        output += "1";
                    }
                }
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].ToString().Equals("E") || b[i].ToString().Equals("E"))
                    {
                        output += "E";
                    }
                    else
                    {
                        switch (kapu)
                        {
                            case Kapu.AND:
                                if (a[i].ToString().Equals("1") && b[i].ToString().Equals("1"))
                                {
                                    output += "1";
                                }
                                else
                                {
                                    output += "0";
                                }
                                break;
                            case Kapu.OR:
                                if (a[i].ToString().Equals("1") || b[i].ToString().Equals("1"))
                                {
                                    output += "1";
                                }
                                else
                                {
                                    output += "0";
                                }
                                break;
                            case Kapu.NAND:
                                if (a[i].ToString().Equals("0") || b[i].ToString().Equals("0"))
                                {
                                    output += "1";
                                }
                                else
                                {
                                    output += "0";
                                }
                                break;
                            case Kapu.NOR:
                                if (a[i].ToString().Equals("1") || b[i].ToString().Equals("1"))
                                {
                                    output += "0";
                                }
                                else
                                {
                                    output += "1";
                                }

                                break;
                            case Kapu.XOR:
                                if (!a[i].ToString().Equals(b[i].ToString()))
                                {
                                    output += "1";
                                }
                                else
                                {
                                    output += "0";
                                }
                                break;
                            case Kapu.XNOR:
                                if (a[i].ToString().Equals(b[i].ToString()))
                                {
                                    output += "1";
                                }
                                else
                                {
                                    output += "0";
                                }
                                break;
                        }
                    }
                }
            }
            return output;
        }
    }
}