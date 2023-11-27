namespace aspnetcore
{
    public class Calculator
    {
        public long Sum(int x, int y) { return x + (long)y; }
        public int Sum(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return 0;
            }

            string[] numbers = str.Split(',');
            int sum = 0;
            string negativeNumbers = "";
            string inValidNumber = "";

            foreach (string number in numbers)
            {
                if(int.TryParse(number.Trim(), out int value))
                {
                    if (value < 0)
                    {
                        negativeNumbers += number + ", ";
                    }

                    sum += value;
                } else
                {
                    inValidNumber += number;
                    throw new Exception($"{inValidNumber.TrimStart(' ')} không phải là số");
                }
            }

            if (!string.IsNullOrEmpty(negativeNumbers))
            {
                throw new Exception($"Không chấp nhận toán hạng âm: {negativeNumbers.TrimEnd(',', ' ')}");
            }

            return sum;
        }
    }
}
