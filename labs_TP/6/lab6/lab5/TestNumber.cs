using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    class TestNumber
    {
		public string num;

		public TestNumber(string n)
		{
			num = n;
		}

		public bool TestNum()
		{
			return (CheckChar() && CheckIntOrFloat());
		}

		//все ли символы числа
		private bool CheckChar()
		{
			if (num.Length == 0)
			{
				MessageBox.Show("Пустота не лучшее решение.");
				return false;
			}

			//проверяем являются ли все символы числами для положительного
			if (num[0] != '-')
			{
				for (int i = 0; i < num.Length; i++)
					if (num[i] < ',' || num[i] == '/' || num[i] == '.' || num[i] == '-' || num[i] > '9')
					{
						MessageBox.Show("А может лучше ввести цифры?");
						return false;
					}
			}
			//проверяем являются ли все символы числами для отрицательного
			else
			{
				for (int i = 1; i < num.Length; i++)
				{
					if (num[i] < ',' || num[i] == '/' || num[i] == '.' || num[i] == '-' || num[i] > '9')
					{
						MessageBox.Show("А может лучше ввести цифры?");
						return false;
					}
				}
			}

			return true;
		}

		//в зависимости от того, с кем работаем, в тот метод и попадаем
		private bool CheckIntOrFloat()
		{
			int count = 0;

			if (num[0] != '-')
			{
				if (num[0] == ',' || num[num.Length - 1] == ',')
				{
					MessageBox.Show("У дробного не может быть в начале или в конце запятая. Либо в начале 0, либо пишите целое число.");
					return false;
				}
			}
			else
			{
				if (num[1] == ',' || num[num.Length - 1] == ',')
				{
					MessageBox.Show("У дробного не может быть в начале или в конце запятая. Либо в начале 0, либо пишите целое число.");
					return false;
				}
			}

			for (int i = 1; i < num.Length; i++)
			{
				if (num[i] == ',')
				{
					count++;
				}
			}

			switch (count)
			{
				case 0:
					return CheckInt();
				case 1:
					return CheckFloat();
				default:
					MessageBox.Show("У дробного не может быть больше одной запятой.");
					return false;
			}

		}

		//проверка для int
		private bool CheckInt()
		{
			if (num.Length < 10 || (num.Length < 11 && num[0] == '-')) // если число меньше нужного кол-ва символов, то оно уже точно подхоидмт
				return true;
			else
			{
				if (num[0] != '-') //проверяем больше ли максимального, учитывая длину
					if (num[0] < '3' && num[1] < '2' && num[2] < '5' && num[3] < '8' && num[4] < '5' && num[5] < '9' && num[6] < '4' && num[7] < '7' && num[8] < '5' && num[9] < '8' && num.Length <= 10)
						return true;
					else
					{
						MessageBox.Show("Выход за диапазон типа int (-2 147 483 648 / 2 147 483 647). Слишком большое число.");
						return false;
					}
				else //проверяем меньше ли минимального, учитывая длину
				{
					if (num[1] < '3' && num[2] < '2' && num[3] < '5' && num[4] < '8' && num[5] < '5' && num[6] < '9' && num[7] < '4' && num[8] < '7' && num[9] < '5' && num[10] < '9' && num.Length <= 11)
						return true;
					else
					{
						MessageBox.Show("Выход за диапазон типа int (-2 147 483 648 / 2 147 483 647). Слишком маленькое число.");
						return false;
					}
				}
			}
		}

		//проверка для float
		private bool CheckFloat()
		{
			int count = 0;

			for (int i = 1; i < num.Length - 1; i++)
				if (num[i] == ',')
				{
					count = i;
					break;
				}

			if (num[0] != '-') //проверяем больше ли максимального, учитывая длину
				if (count > 10)
				{
					MessageBox.Show("Выход за диапазон типа int (-2 147 483 648.0 / 2 147 483 647.0). Слишком большое число.");
					return false;
				}
				else
				{
					if (count < 10)
						return true;
					if (num[0] < '3' && num[1] < '2' && num[2] < '5' && num[3] < '8' && num[4] < '5' && num[5] < '9' && num[6] < '4' && num[7] < '7' && num[8] < '5' && num[9] < '8')
					{
						if (num[10] == ',')
						{
							if (num[0] == '2' && num[1] == '1' && num[2] == '4' && num[3] == '7' && num[4] == '4' && num[5] == '8' && num[6] == '3' && num[7] == '6' && num[8] == '4' && num[9] == '7')
							{
								for (int i = 11; i < num.Length; i++)
									if (num[i] != '0')
									{
										MessageBox.Show("Выход за диапазон типа int (-2 147 483 648.0 / 2 147 483 647.0). Слишком большое число.");
										return false;
									}
								return true;
							}
							else 
								return true;
						}
						else
						{
							MessageBox.Show("Выход за диапазон типа int (-2 147 483 648.0 / 2 147 483 647.0). Слишком большое число.");
							return false;
						}
					}
					else
					{
						MessageBox.Show("Выход за диапазон типа int (-2 147 483 648.0 / 2 147 483 647.0). Слишком большое число.");
						return false;
					}
				}				
			else //проверяем меньше ли минимального, учитывая длину
			{
				if (count > 11)
				{
					MessageBox.Show("Выход за диапазон типа int (-2 147 483 648.0 / 2 147 483 647.0). Слишком большое число.");
					return false;
				}
				else
				{
					if (count < 11)
						return true;
					if (num[1] < '3' && num[2] < '2' && num[3] < '5' && num[4] < '8' && num[5] < '5' && num[6] < '9' && num[7] < '4' && num[8] < '7' && num[9] < '5' && num[10] < '8')
					{
						if (num[11] == ',')
						{
							if (num[1] == '2' && num[2] == '1' && num[3] == '4' && num[4] == '7' && num[5] == '4' && num[6] == '8' && num[7] == '3' && num[8] == '6' && num[9] == '4' && num[10] == '7')
							{
								for (int i = 12; i < num.Length; i++)
									if (num[i] != 0)
									{
										MessageBox.Show("Выход за диапазон типа int (-2 147 483 648.0 / 2 147 483 647.0). Слишком большое число.");
										return false;
									}
								return true;	
							}
							else
								return true;
						}
						else
						{
							MessageBox.Show("Выход за диапазон типа int (-2 147 483 648.0 / 2 147 483 647.0). Слишком большое число.");
							return false;
						}
					}
					else
					{
						MessageBox.Show("Выход за диапазон типа int (-2 147 483 648 / 2 147 483 647). Слишком маленькое число.");
						return false;
					}
				}
			}
		}

	}
}