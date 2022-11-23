using NUnit.Framework;
using Calculator.BusinessLogic.Controller;
using System;

namespace CalcTests1;


public class Tests
{
    
    public OperationController testOp = new OperationController();
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(data))]
    public void ValidTest(double Op1, double Op2, Operations currentOp, double expected)
    {
        OperationController testOp = new OperationController();
        testOp.firstOperand = Op1;
        testOp.secondOperand = Op2;
        testOp.currentOperation = currentOp;
        double actualRes = 0;
        testOp.Calculate(ref actualRes);
        Assert.AreEqual(expected, actualRes);
    }
    static object[] data =
    {
        new object[] { 12, 3, Operations.Plus, 15 },
        new object[] { 12, 2, Operations.Minus, 10 },
        new object[] { 12, 4, Operations.Mult, 48 },
        new object[] { 12, 4, Operations.Divide, 3 },
        new object[] { 5, 4, Operations.Factorial, 120 },
        new object[] { 3, 4, Operations.Exp, 81 }
    };

    static object[] negativeData = {
        new object[] {5, 0, Operations.Divide}
    };


    [TestCaseSource(nameof(negativeData))]
    public void InvalidTest(double Op1, double Op2, Operations currentOp){
        OperationController testOp = new OperationController();
        testOp.firstOperand = Op1;
        testOp.secondOperand = Op2;
        testOp.currentOperation = currentOp;
        double actualRes = 0;
        Assert.False(testOp.Calculate(ref actualRes));
    }
}