using Calculator.BusinessLogic.Controller;

namespace Calculator.BusinessLogic.Model
{
    public class LogMessage
    {
        public string Data;
        public string? currentTime;
        public LogMessage(OperationController opController){
            double tmpRes = 0;
            opController.Calculate(ref tmpRes);
            if (opController.currentOperation != Operations.Factorial){
                Data = $"Опрерация: {opController.firstOperand} {Convert.ToChar(opController.currentOperation)} {opController.secondOperand} = {tmpRes}";
            }
            else{
                Data = $"Опрерация: {opController.firstOperand}{Convert.ToChar(opController.currentOperation)} = {tmpRes}";
            }
            currentTime = DateTime.Now.ToString();
        }
        public string ReturnMessage(){
            return $"{Data} | {currentTime}";
        }
    }
}