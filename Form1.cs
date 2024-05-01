namespace Caculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "9";
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "(";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += ")";
        }

        private void btn_cong_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "+";
        }

        private void btn_tru_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "-";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtDisplay.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDisplay.ReadOnly = true;
        }

        private void btn_ce_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1);
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "0";
        }

        public class TreeNode
        {
            public string Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode(string value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        public class ShuntingYard
        {
            private string input;

            public ShuntingYard(string expression)
            {
                input = expression;
            }

            private bool isDigit(string s)
            {
                foreach (char c in s)
                {
                    if (!Char.IsDigit(c))
                        return false;
                }

                return true;
            }

            // Thứ tự ưu tiên biểu thức
            private int Precedence(string op)
            {
                switch (op)
                {
                    case "+":
                    case "-":
                        return 1;
                    case "*":
                    case "/":
                        return 2;
                    default:
                        return 0;
                }
            }

            // Chuyển sang biểu thức postfix
            private List<string> ConvertToPostfix()
            {
                Dictionary<int, string> postfix = new Dictionary<int, string>();

                Stack<string> operators = new Stack<string>();

                int postfixIndex = -1;

                for (int i = 0; i < input.Length; i++)
                {
                    if (Char.IsDigit(input[i])) // tokenizer
                    {
                        if (i == 0 || (i > 0 && !Char.IsDigit(input[i - 1])))
                        {
                            postfixIndex++;
                            postfix.Add(postfixIndex, input[i].ToString());
                        }
                        else
                        {
                            postfix[postfixIndex] = postfix[postfixIndex] + input[i].ToString();
                        }
                    }
                    else if (input[i] == '(')
                    {
                        operators.Push(input[i].ToString());
                    }
                    else if (input[i] == ')')
                    {
                        while (operators.Count > 0 && operators.Peek() != "(")
                        {
                            postfixIndex++;
                            postfix.Add(postfixIndex, operators.Pop());
                        }
                        operators.Pop(); // Pop '('
                    }
                    else
                    {
                        while (operators.Count > 0 && Precedence(input[i].ToString()) <= Precedence(operators.Peek()))
                        {
                            postfixIndex++;
                            postfix.Add(postfixIndex, operators.Pop());
                        }
                        operators.Push(input[i].ToString());
                    }
                }

                while (operators.Count > 0)
                {
                    postfixIndex++;
                    postfix.Add(postfixIndex, operators.Pop());
                }

                return postfix.Values.ToList();
            }

            private int Evaluate(TreeNode node)
            {
                if (isDigit(node.Value))
                {
                    return int.Parse(node.Value);
                }

                int leftValue = Evaluate(node.Left);
                int rightValue = Evaluate(node.Right);

                switch (node.Value)
                {
                    case "+":
                        return leftValue + rightValue;
                    case "-":
                        return leftValue - rightValue;
                    case "*":
                        return leftValue * rightValue;
                    case "/":
                        return leftValue / rightValue;
                    default:
                        throw new ArgumentException("Invalid operator: " + node.Value);
                }
            }

            public int EvaluateExpression()
            {
                TreeNode root = BuildTree();
                return Evaluate(root);
            }

            public TreeNode BuildTree()
            {
                List<string> postfix = ConvertToPostfix();
                Stack<TreeNode> stack = new Stack<TreeNode>();


                try
                {
                    foreach (string c in postfix)
                    {
                        if (isDigit(c))
                        {
                            stack.Push(new TreeNode(c));
                        }
                        else
                        {
                            if (stack.Count < 2)
                            {
                                throw new InvalidOperationException("Not enough operands to build the tree.");
                            }

                            TreeNode node = new TreeNode(c);
                            node.Right = stack.Pop();
                            node.Left = stack.Pop();
                            stack.Push(node);
                        }
                    }

                    if (stack.Count != 1)
                    {
                        throw new InvalidOperationException("Invalid expression. Tree construction failed.");
                    }

                    return stack.Pop();
                }
                catch (Exception ex)
                {
                    return null; // Trả về null nếu có lỗi
                }
            }
        }
        private void btnEqual_Click(object sender, EventArgs e)
        {
            string expression = txtDisplay.Text;
            ShuntingYard shuntingYard = new ShuntingYard(expression);
            TreeNode root = shuntingYard.BuildTree();
            if (root == null)
            {
                txtDisplay.Text = "ERROR";
            }
            else
            {
                int result = shuntingYard.EvaluateExpression();
                txtDisplay.Text = result.ToString();

            }
        }

        private void btn_nhan_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "*";
        }

        private void btn_chia_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "/";
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
