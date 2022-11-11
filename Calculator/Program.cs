using System;
using System.IO;
using Calculator.BusinessLogic.Controller;
using Calculator.BusinessLogic.Model;

namespace TheBigOne
{


    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Запущен калькулятор...\n");
            Console.Clear();
            using (StreamWriter sw = new StreamWriter(new FileStream("log.txt", FileMode.Create))){
                sw.Write("\tЛОГИ:\n");
            }
            ProcessController procController = new ProcessController();
            OperationController opController = new OperationController();
            while (Convert.ToBoolean(procController.currentProcess)){
                switch (procController.currentProcess)
                {
                    case ToDo.Initing:
                        Console.WriteLine("Введите оператор");
                        
                        try{
                            char tmp = Convert.ToChar(Console.ReadLine() ?? "e");
                            if (OperatorCheck.CharInList(tmp)){
                                opController.currentOperation = (Operations) tmp;
                                procController.currentProcess = ToDo.Operating;
                            }
                            else{
                                throw new Exception();
                            }
                        }
                        catch (Exception){
                            Console.WriteLine("Некорректный ввод оператора! Попробуйте еще раз");
                            procController.currentProcess = ToDo.Reseting;
                        }
                        break;
                    case ToDo.Operating:
                        double result = 0;
                        if (opController.currentOperation == Operations.Factorial && procController.currentProcess == ToDo.Operating){
                            Console.WriteLine("Введите операнд");
                            if (!Double.TryParse(Console.ReadLine(), out opController.firstOperand)){
                                procController.currentProcess = ToDo.Reseting;
                            };
                        }
                        else{
                            Console.WriteLine("Введите операнд 1");
                            if (!Double.TryParse(Console.ReadLine(), out opController.firstOperand)){
                                procController.currentProcess = ToDo.Reseting;
                            };
                            Console.WriteLine("Введите операнд 2");
                            if (!Double.TryParse(Console.ReadLine(), out opController.secondOperand)){
                                procController.currentProcess = ToDo.Reseting;
                            };
                        }
                        if (opController.Calculate(ref result) && procController.currentProcess == ToDo.Operating){
                            Console.WriteLine($"Результат операции: {result}");
                            procController.currentProcess = ToDo.Logging;
                        }
                        else{
                            Console.WriteLine("Некорректный ввод операнда! Попробуйте еще раз");
                        }
                        break;
                    case ToDo.Logging:
                        LogController.WriteLogInFile(opController);
                        procController.currentProcess = ToDo.Reseting;
                        break;
                    case ToDo.Reseting:
                        Console.WriteLine("Введите 'r' , если хотите продолжить, или 'e' для выхода");
                        try {
                            char tmp = Convert.ToChar(Console.ReadLine() ?? "");
                            if (tmp == 'r'){
                                procController.currentProcess = ToDo.Initing;
                            }
                            else{
                                procController.currentProcess = ToDo.Exiting;
                            }
                        }
                        catch(Exception){
                            Console.WriteLine("Некорректный ввод! Попробуйте еще раз");
                        }
                        break;
                }
            }
        } 
    }
}