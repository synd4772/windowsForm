using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KolmRakendust.MathQuiz.Logic
{
    public static class MathQuizUtils
    {
        public static bool CheckTheAnswer(MathExample example, decimal answer)
        {
            if (example.Calculate() == answer)
            {
                return true;
            }
            return false;
        }
        public static MathExample GenerateRandomExample()
        {
            Random random = new Random();
            OperatorType randOperator;
            int y, x;
            Array values = Enum.GetValues(typeof(OperatorType));
            randOperator = (OperatorType)values.GetValue(random.Next(values.Length));

            y = random.Next(1, 10);
            x = random.Next(1, 15);
            MathExample mathEx = new MathExample(x, y, new MathOperator(randOperator));
            return mathEx;
        }
        public static MathExample GenerateRandomExample(int start, int end)
        {
            Random random = new Random();
            OperatorType randOperator;
            int y, x;
            Array values = Enum.GetValues(typeof(OperatorType));
            randOperator = (OperatorType)values.GetValue(random.Next(values.Length));

            y = random.Next(start, end);
            x = random.Next(start, end);
            MathExample mathEx = new MathExample(y, x, new MathOperator(randOperator));
            return mathEx;
        }
    }
}
