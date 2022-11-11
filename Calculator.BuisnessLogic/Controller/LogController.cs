using Calculator.BusinessLogic.Model;

namespace Calculator.BusinessLogic.Controller
{
    static public class LogController
    {
        static private int countLogs = 0;
        static public void WriteLogInFile(OperationController opController){
            LogMessage tmpLog = new LogMessage(opController);
            countLogs++;
            using (StreamWriter sw = new StreamWriter(new FileStream("log.txt", FileMode.Append))){
                sw.Write($"{countLogs} | {tmpLog.ReturnMessage()} \n");
            }
        }
    }
    public enum Operations
    {
        Plus = '+',
        Minus = '-',
        Mult = '*',
        Divide = '/',
        Exp = '^',
        Factorial = '!'
    }

    public enum ToDo{
        Exiting,
        Initing,
        Operating,
        Logging,
        Reseting
    }
    public class ProcessController
    {
        public ToDo currentProcess;
        public string? filePath;
        public ProcessController(){
            currentProcess = ToDo.Initing;
            filePath = "log.txt";
        }
    }
    
    public class OperationController{
        public double firstOperand;
        public double secondOperand;
        public Operations currentOperation;
        static int Factorial(int a){
            for (int i = a - 1; i > 0; i--){
                a *= i;
            }
            return a;
        }
        public bool Calculate(ref double result){
            switch (this.currentOperation){
                case Operations.Plus:
                    result = firstOperand + secondOperand;
                    return true;
                case Operations.Minus:
                    result = firstOperand - secondOperand;
                    return true;
                case Operations.Mult:
                    result = firstOperand * secondOperand;
                    return true;
                case Operations.Divide:
                    if (secondOperand == 0){
                        return false;
                    }
                    else{
                        result = firstOperand / secondOperand;
                        return true;
                    }
                case Operations.Exp:
                    result = Math.Pow(firstOperand, secondOperand);
                    return true;
                case Operations.Factorial:
                    if ((int) this.firstOperand >= 0){
                        result = Factorial((int)firstOperand);
                        return true;
                    }
                    else{
                        return false;
                    }
            }
            return false;
        }
    }
    static public class OperatorCheck{
        static List<char> list = new List<char> {'+', '-', '*', '/', '^', '!'};
        static public bool CharInList(char inValue){
            bool res = false;
            foreach (var item in list)
            {
                if (inValue == item){
                    res = true;
                }
            }
            return res;
        }
    }
}